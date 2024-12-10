using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Health_Damage : MonoBehaviour
{
    int health = 3;
    public GameObject healthItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            healthItem = new GameObject();
            healthItem.transform.position = this.transform.position;

            
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            health--;
        }
    }
}
