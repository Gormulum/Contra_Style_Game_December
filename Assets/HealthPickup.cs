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

        Invoke("Force", 0.2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void Force()
    {
        rb.AddForce(0, 10, 0);
    }

    
}
