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
        //�Է¿� ���� �¿�� �̵�
        if (!isMoving) 
        { 
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(moveHorizontal, 0, 0);

            //��ġ ������Ʈ
            transform.position += dir * pMs * Time.deltaTime;
        }
    }

    private void Move(Vector3 direction)
    {
        isMoving = true; // �̵� �� ���·� ����

        // ���� ��ġ���� �̵��� ��ġ ���
        Vector3 targetPosition = transform.position + direction * pMs;

        // ��ġ�� �����̵�
        transform.position = targetPosition;

        isMoving = false; // �̵� �Ϸ� ���·� ����
    }
}
