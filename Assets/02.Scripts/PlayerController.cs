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
    SpriteRenderer PlayerSprite;
    void Awake()
    {
        // ���� ���� ������Ʈ�� �پ� �ִ� Rigidbody2D ������Ʈ�� ������ ����
        PlayerRigid = GetComponent<Rigidbody2D>();
        PlayerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Ű���� �Է��� �޾� x��� y�� ���� ���� ����
        // GetAxisRaw�� -1, 0, 1 �� �ϳ��� ���� ��ȯ (��� ����)
        inputPlayerVec.x = Input.GetAxisRaw("Horizontal"); // �¿� ����Ű �Ǵ� A/D Ű
        inputPlayerVec.y = Input.GetAxisRaw("Vertical");   // ���� ����Ű �Ǵ� W/S Ű

        if (inputPlayerVec.x != 0)
        {
            PlayerSprite.flipX = inputPlayerVec.x < 0; // �����̸� ������
        }

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
