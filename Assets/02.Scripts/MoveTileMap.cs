using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 스크립트는 플레이어가 특정 구역을 벗어났을 때 타일맵을 움직이게 함
public class MoveTileMap : MonoBehaviour
{
    // 플레이어가 이 오브젝트의 트리거 영역을 벗어났을 때 실행되는 함수
    void OnTriggerExit2D(Collider2D collision)
    {
        // 만약 충돌한 오브젝트의 태그가 "Area"가 아니면 아무것도 하지 않고 종료 ! -> 아니면
        if (!collision.CompareTag("Area"))
            return;

        // 플레이어의 현재 위치를 가져옴 (게임매니저 싱글톤을 이용하여 플레이어를 받아옴)
        Vector3 playerPosition = GameManager.instance.playerController.transform.position;

        // 현재 타일맵의 위치를 가져옴
        Vector3 mapPosition = transform.position;

        // 플레이어와 타일맵 사이의 거리 차이를 계산
        float movePositionX = Mathf.Abs(playerPosition.x - mapPosition.x);
        float movePositionY = Mathf.Abs(playerPosition.y - mapPosition.y);

        // 플레이어가 움직인 방향을 가져옴
        Vector3 playerDir = GameManager.instance.playerController.inputPlayerVec;

        // 플레이어가 왼쪽으로 움직였으면 -1, 오른쪽이면 1
        float dirx = playerDir.x < 0 ? -1 : 1;

        // 플레이어가 아래로 움직였으면 -1, 위쪽이면 1
        float diry = playerDir.y < 0 ? -1 : 1;

        // 이 오브젝트의 태그에 따라 다르게 행동
        switch (transform.tag)
        {
            // 태그가 "Ground"일 경우
            case "Ground":
                // X축 차이가 더 크면 좌우로 움직임
                if (movePositionX > movePositionY)
                {
                    // 플레이어가 움직인 방향으로 타일맵을 40만큼 이동
                    transform.Translate(Vector3.right * dirx * 40);
                }
                // Y축 차이가 더 크면 위아래로 움직임
                else if (movePositionX < movePositionY)
                {
                    // 플레이어가 움직인 방향으로 타일맵을 40만큼 이동
                    transform.Translate(Vector3.up * diry * 40);
                }
                break;
        }
    }
}
