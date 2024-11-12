using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    

    private Vector3 shootDir;
    SpriteRenderer spriteRenderer;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float moveSpeed = -40f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;

        if (shootDir.x < 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
