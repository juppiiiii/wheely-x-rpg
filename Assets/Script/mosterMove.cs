using UnityEngine;

public class mosterMove : MonoBehaviour
{
    public float mMs = 0.00001f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();

    
    }

    void move()
    {
        Vector3 dir = new Vector3(0, 0, -1);
        transform.position +=  dir * mMs * Time.deltaTime ;
    }
}
