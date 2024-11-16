using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class PotionPickup : MonoBehaviour
{

    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    bool walkedThrough;
    private void Start()
    {
        //anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        
    }

    private void Update()
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }


}
