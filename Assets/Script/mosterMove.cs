using UnityEngine;

public class mosterMove : MonoBehaviour
{
    public float mMs = 0.00001f;
    float walktime = 0f;
    float stopwalk = 2.1f;


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
        
        if (walktime < stopwalk)
        {
            Vector3 dir = new Vector3(0, 0, -1);
            transform.position += dir * mMs * Time.deltaTime;
            walktime += Time.deltaTime;
        }
        else 
        {
            Vector3 dir = new Vector3(0, 0, 0);
            transform.position += dir * mMs * Time.deltaTime;
        }

    }
}
