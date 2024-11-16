using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float bulletSpeed;

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void Move()
    {
        
    }
}
