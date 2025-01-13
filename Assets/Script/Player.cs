using UnityEngine;

public class Player : MonoBehaviour
{
    public float pMs = 6.0f; //PlayerMoveSpeed
    private bool isMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //입력에 따라 좌우로 이동
        if (!isMoving) 
        { 
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(moveHorizontal, 0, 0);

            //위치 업데이트
            transform.position += dir * pMs * Time.deltaTime;
        }
    }

    private void Move(Vector3 direction)
    {
        isMoving = true; // 이동 중 상태로 설정

        // 현재 위치에서 이동할 위치 계산
        Vector3 targetPosition = transform.position + direction * pMs;

        // 위치를 순간이동
        transform.position = targetPosition;

        isMoving = false; // 이동 완료 상태로 설정
    }
}
