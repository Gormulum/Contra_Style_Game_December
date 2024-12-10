using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 6;

    public PotionChangeImage healthIcon;
    public PlayerMovement playerMovement;

    bool canTakeDamage = true;

    public Transform respawnLocaton;

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = 6;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && canTakeDamage == true)
        {
            InvokeRepeating("HealthDeplete", 0.0f, 1);
            
        }

        if (collision.tag == "Death")
        {
            Death();
        }
    }

    void Death()
    {
        animator.SetBool("Died", true);
        canTakeDamage = false;
        Invoke("PlayerRespawn", 3);
        playerMovement.enabled = false;
    }

    void PlayerRespawn()
    {
        
        health = 6;
        animator.SetBool("Hurt", false);
        animator.SetBool("Died", false);
        playerMovement.enabled = true;
        healthIcon.Reset();
        rb.position = respawnLocaton.position;
        canTakeDamage = true;
        CancelInvoke("HealthDeplete");
        CancelInvoke("PlayerRespawn");

    }

    private void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }

    void HealthDeplete()
    {
        health--;
        healthIcon.ChangeImageDown();
        animator.SetBool("Hurt", true);
    }

    public void HealthIncrease()
    {
        health++;
        healthIcon.ChangeImageUp();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" )
        {
            
            CancelInvoke("HealthDeplete");
            animator.SetBool("Hurt", false);
        }
    }
}
