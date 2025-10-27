using System.Collections;
using System.Collections.Generic;
using UnityEditor; // Unity ������ ���� ��� (�� �ڵ忡���� ������ ����)
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾��� �Է� ������ �����ϴ� ���� (x: �¿�, y: ����)
    public Vector2 inputPlayerVec;

    // �÷��̾� �̵� �ӵ�
    public float speed;

    // Rigidbody2D ������Ʈ�� ������ ����
    Rigidbody2D PlayerRigid;
    // SpriteRenderer ������Ʈ�� ������ ����
    SpriteRenderer PlayerSprite;
    // Animator ������Ʈ�� ������ ����
    Animator PlayerAni;
    void Awake()
    {
        // ���� ���� ������Ʈ�� �پ� �ִ� Rigidbody2D ������Ʈ�� ������ ���� (�ʱ�ȭ �����ִ� ��)
        PlayerRigid = GetComponent<Rigidbody2D>();

        // ���� ���� ������Ʈ�� �پ� �ִ� SpriteRenderer ������Ʈ�� ������ ���� (�ʱ�ȭ �����ִ� ��)
        PlayerSprite = GetComponent<SpriteRenderer>();

        // ���� ���� ������Ʈ�� �پ� �ִ� Animator ������Ʈ�� ������ ���� (�ʱ�ȭ �����ִ� ��)
        PlayerAni = GetComponent<Animator>();
    }

    void Update()
    {
        // Ű���� �Է��� �޾� x��� y�� ���� ���� ����
        // GetAxisRaw�� -1, 0, 1 �� �ϳ��� ���� ��ȯ (��� ����)
        inputPlayerVec.x = Input.GetAxisRaw("Horizontal"); // �¿� ����Ű �Ǵ� A/D Ű
        inputPlayerVec.y = Input.GetAxisRaw("Vertical");   // ���� ����Ű �Ǵ� W/S Ű

        // ���� inputPlayerVec�� x���� 0�� �ƴ϶� 1�̰ų� -1�� ��
        if (inputPlayerVec.x != 0)
        {
            // x���� 0���� ���� -1�� ��, �ø�(x���� �������� �¿����)�� �ض�
            PlayerSprite.flipX = inputPlayerVec.x < 0;
        }

        // PlayerAni�� Set�� ������ �� Speed �Ķ���Ϳ� ������ ���� Float���� ��������
        // ���ڰ��� �Ķ������ �̸��� ���� �ϰ� �ӵ����� inputPlayerVec�� x���̳� y�� �ϳ��� �ִ°� �ƴ϶�
        // ��� �������� �������� ���°� ���� �� �ְ� inputPlayerVec �� ��ü�� ������ �����ؼ� �־��ش�.
        PlayerAni.SetFloat("Speed", inputPlayerVec.magnitude);

    }

    // ���� ������ �ʿ��� ��� FixedUpdate���� ó�� (�����Ӱ� �����ϰ� ������ �ð� �������� �����)
    private void FixedUpdate()
    {
        // x�� y������ 1��ŭ ������ �� �밢���� 1��ŭ �����̴°� �ƴ϶� �밢�� �ӵ��� �����ֱ� ���� normalized ���.
        // �ӵ��� �ð�(Time.fixedDeltaTime)�� ���� ���� �̵� �Ÿ� ���
        Vector2 nextVec = inputPlayerVec.normalized * speed * Time.fixedDeltaTime;

        // Rigidbody2D�� �̿��� ���� ��ġ���� ���� �������� �̵�
        PlayerRigid.MovePosition(PlayerRigid.position + nextVec);
    }

}
