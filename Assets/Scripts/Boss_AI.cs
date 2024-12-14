using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_AI : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject player;

    public Transform pointA;
    public Transform pointB;
    
    SpriteRenderer spriteRenderer;
    Animator animator;
    CapsuleCollider2D collider;

    public Transform boneProjectile;

    int health = 50;
    public float speed;

    int animationID;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();

        InvokeRepeating("Projectile", 0, 2);
    }


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

        if (health <= 0)
        {
            Death();
        }

       

        this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        

        
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
        CancelInvoke("Projectile");
        speed = 0f;
        collider.enabled = false;
    }

    void Projectile()
    {
        Transform bulletTransform = Instantiate(boneProjectile, this.transform.position, Quaternion.identity);

        Vector3 shootDir = ((this.transform.position + new Vector3(0, 0.5f, 0)) - player.transform.position).normalized;
        bulletTransform.GetComponent<Bone>().Setup(shootDir);
    }

    


    
}
