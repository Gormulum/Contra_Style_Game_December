using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    PlayerHealth playerHealth;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(0, 10, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerHealth.HealthIncrease();
        Destroy(gameObject);
    }

    
}
