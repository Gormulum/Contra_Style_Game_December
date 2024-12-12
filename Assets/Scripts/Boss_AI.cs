using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject player;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Rigidbody2D rb;

    int health = 10;
    float speed;

    int animationID;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("Randomiser", 2, 5);
    }


    void Update()
    {
        if (this.transform.position.x > player.transform.position.x)
        {
            speed *= -1f;
            spriteRenderer.flipX = false;
        }

        if (health <= 0)
        {
            Death();
        }

        if (animationID == 1)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }

        if (animationID == 2)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Debug.Log(health);
            health--;
        }
    }

    void Death()
    {
        animator.SetBool("Death", true);
    }

    void Randomiser()
    {
        animationID = Random.Range(0, 2);
    }
}
