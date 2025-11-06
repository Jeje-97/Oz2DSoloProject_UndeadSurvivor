using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 에너미를 제어하는 클래스
public class Enemy : MonoBehaviour
{
    // 에너미의 이동 속도
    public float speed;

    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] aniCon;

    // 추적할 대상
    public Rigidbody2D target;

    // 에너미가 살아있는 상태인지
    bool isLive;

    // 에너미 Rigidbody2D 컴포넌트
    Rigidbody2D enemyRigid;

    Collider2D collider2d;

    Animator animator;

    // 에너미 SpriteRenderer 컴포넌트
    SpriteRenderer enemySprite;

    WaitForFixedUpdate wait;

    // 게임 시작 전에 컴포넌트를 가져오는 초기 설정
    void Awake()
    {
        // Rigidbody2D 컴포넌트 가져오기
        enemyRigid = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        // SpriteRenderer 컴포넌트 가져오기
        enemySprite = GetComponent<SpriteRenderer>();

        wait = new WaitForFixedUpdate();

        collider2d = GetComponent<Collider2D>();
    }

    // 물리 연산이 필요한 경우 매 프레임마다 호출됨
    void FixedUpdate()
    {
        // 적이 살아있으면 움직이지 않음
        if (isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        // 대상과 에너미 사이의 방향 벡터 계산
        Vector2 dirVec = target.position - enemyRigid.position;

        // 방향 벡터를 정규화하고 속도와 시간에 따라 이동 벡터 계산
        Vector2 moveVec = dirVec.normalized * speed * Time.deltaTime;

        // 에너미를 새로운 위치로 이동
        enemyRigid.MovePosition(enemyRigid.position + moveVec);

        // 이동 후 속도 초기화 (불필요한 물리 효과 방지)
        enemyRigid.velocity = Vector2.zero;
    }

    // 렌더링 직전에 호출되어 이미지 방향을 조정
    private void LateUpdate()
    {
        // 적이 살아있으면 이미지 방향 변경하지 않음
        if (isLive)
            return;

        // 대상이 왼쪽에 있으면 이미지 좌우 반전
        enemySprite.flipX = target.position.x < enemyRigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.playerController.GetComponent<Rigidbody2D>();
        isLive = false;
        collider2d.enabled = true;
        enemyRigid.simulated = true;
        enemySprite.sortingOrder = 2;
        animator.SetBool("Dead", false);
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = aniCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            collider2d.enabled = false;
            enemyRigid.simulated = false;
            enemySprite.sortingOrder = 1;
            animator.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.playerController.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        enemyRigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }
}
