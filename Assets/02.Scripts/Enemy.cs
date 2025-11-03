using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D enemyRigid;
    SpriteRenderer enemySprite;
    // Start is called before the first frame update
    void Awake()
    {
        enemyRigid = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLive)
            return;

        Vector2 dirVec = target.position - enemyRigid.position;
        Vector2 moveVec = dirVec.normalized * speed * Time.deltaTime;
        enemyRigid.MovePosition(enemyRigid.position + moveVec);
        enemyRigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (isLive)
            return;

        enemySprite.flipX = target.position.x < enemyRigid.position.x;
    }
}
