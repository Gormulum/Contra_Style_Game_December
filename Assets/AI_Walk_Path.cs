using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Walk_Path : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > pointB.position.x)
        {
            speed *= -1f;
        }

        if (this.transform.position.x < pointA.position.x)
        {
            speed *= -1f;
        }

        this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    private void FixedUpdate()
    {
        
    }
        

}
