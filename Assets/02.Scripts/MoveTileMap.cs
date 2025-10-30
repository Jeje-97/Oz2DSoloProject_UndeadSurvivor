using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ũ��Ʈ�� �÷��̾ Ư�� ������ ����� �� Ÿ�ϸ��� �����̰� ��
public class MoveTileMap : MonoBehaviour
{
    // �÷��̾ �� ������Ʈ�� Ʈ���� ������ ����� �� ����Ǵ� �Լ�
    void OnTriggerExit2D(Collider2D collision)
    {
        // ���� �浹�� ������Ʈ�� �±װ� "Area"�� �ƴϸ� �ƹ��͵� ���� �ʰ� ���� ! -> �ƴϸ�
        if (!collision.CompareTag("Area"))
            return;

        // �÷��̾��� ���� ��ġ�� ������ (���ӸŴ��� �̱����� �̿��Ͽ� �÷��̾ �޾ƿ�)
        Vector3 playerPosition = GameManager.instance.playerController.transform.position;

        // ���� Ÿ�ϸ��� ��ġ�� ������
        Vector3 mapPosition = transform.position;

        // �÷��̾�� Ÿ�ϸ� ������ �Ÿ� ���̸� ���
        float movePositionX = Mathf.Abs(playerPosition.x - mapPosition.x);
        float movePositionY = Mathf.Abs(playerPosition.y - mapPosition.y);

        // �÷��̾ ������ ������ ������
        Vector3 playerDir = GameManager.instance.playerController.inputPlayerVec;

        // �÷��̾ �������� ���������� -1, �������̸� 1
        float dirx = playerDir.x < 0 ? -1 : 1;

        // �÷��̾ �Ʒ��� ���������� -1, �����̸� 1
        float diry = playerDir.y < 0 ? -1 : 1;

        // �� ������Ʈ�� �±׿� ���� �ٸ��� �ൿ
        switch (transform.tag)
        {
            // �±װ� "Ground"�� ���
            case "Ground":
                // X�� ���̰� �� ũ�� �¿�� ������
                if (movePositionX > movePositionY)
                {
                    // �÷��̾ ������ �������� Ÿ�ϸ��� 40��ŭ �̵�
                    transform.Translate(Vector3.right * dirx * 40);
                }
                // Y�� ���̰� �� ũ�� ���Ʒ��� ������
                else if (movePositionX < movePositionY)
                {
                    // �÷��̾ ������ �������� Ÿ�ϸ��� 40��ŭ �̵�
                    transform.Translate(Vector3.up * diry * 40);
                }
                break;
        }
    }
}
