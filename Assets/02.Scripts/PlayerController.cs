using System.Collections;
using System.Collections.Generic;
using UnityEditor; // Unity 에디터 관련 기능 (이 코드에서는 사용되지 않음)
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 입력 방향을 저장하는 변수 (x: 좌우, y: 상하)
    public Vector2 inputPlayerVec;

    // 플레이어 이동 속도
    public float speed;

    public Scanner scanner;

    // Rigidbody2D 컴포넌트를 저장할 변수
    Rigidbody2D PlayerRigid;
    // SpriteRenderer 컴포넌트를 저장할 변수
    SpriteRenderer PlayerSprite;
    // Animator 컴포넌트를 저장할 변수
    Animator PlayerAni;
    void Awake()
    {
        // 현재 게임 오브젝트에 붙어 있는 Rigidbody2D 컴포넌트를 가져와 저장 (초기화 시켜주는 것)
        PlayerRigid = GetComponent<Rigidbody2D>();

        // 현재 게임 오브젝트에 붙어 있는 SpriteRenderer 컴포넌트를 가져와 저장 (초기화 시켜주는 것)
        PlayerSprite = GetComponent<SpriteRenderer>();

        // 현재 게임 오브젝트에 붙어 있는 Animator 컴포넌트를 가져와 저장 (초기화 시켜주는 것)
        PlayerAni = GetComponent<Animator>();

        scanner = GetComponent<Scanner>();
    }

    void Update()
    {
        // 키보드 입력을 받아 x축과 y축 방향 값을 저장
        // GetAxisRaw는 -1, 0, 1 중 하나의 값을 반환 (즉시 반응)
        inputPlayerVec.x = Input.GetAxisRaw("Horizontal"); // 좌우 방향키 또는 A/D 키
        inputPlayerVec.y = Input.GetAxisRaw("Vertical");   // 상하 방향키 또는 W/S 키

        // 만약 inputPlayerVec의 x축이 0이 아니라 1이거나 -1일 때
        if (inputPlayerVec.x != 0)
        {
            // x축이 0보다 작은 -1일 때, 플립(x축을 기준으로 좌우반전)을 해라
            PlayerSprite.flipX = inputPlayerVec.x < 0;
        }

        // PlayerAni에 Set을 가져올 때 Speed 파라미터와 종류가 같은 Float으로 가져오고
        // 인자값을 파라미터의 이름과 같게 하고 속도값을 inputPlayerVec의 x값이나 y값 하나로 주는게 아니라
        // 어느 방향으로 움직여도 상태가 변할 수 있게 inputPlayerVec 그 자체를 값으로 설정해서 넣어준다.
        PlayerAni.SetFloat("Speed", inputPlayerVec.magnitude);

    }

    // 물리 연산이 필요한 경우 FixedUpdate에서 처리 (프레임과 무관하게 일정한 시간 간격으로 실행됨)
    void FixedUpdate()
    {
        // x축 y축으로 1만큼 움직일 때 대각선은 1만큼 움직이는게 아니라서 대각선 속도를 맞춰주기 위해 normalized 사용.
        // 속도와 시간(Time.fixedDeltaTime)을 곱해 실제 이동 거리 계산
        Vector2 nextVec = inputPlayerVec.normalized * speed * Time.fixedDeltaTime;

        // Rigidbody2D를 이용해 현재 위치에서 계산된 방향으로 이동
        PlayerRigid.MovePosition(PlayerRigid.position + nextVec);
    }

}
