using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnScript : MonoBehaviour
{

    public GameObject levelTheme;
    public GameObject bossTheme;
    public GameObject ambience;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            levelTheme.SetActive(false);
            bossTheme.SetActive(true);
            ambience.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            levelTheme.SetActive(true);
            bossTheme.SetActive(false);
                ambience.SetActive(true);
        }
    }
}
