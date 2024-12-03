using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    Vector3 targetPosition = new Vector3(0, 1, -10);

    public PlayerMovement playerMovement;


    bool locked = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (locked == false)
        {
            
            if (playerMovement.collidingWith == "cameraLockOn")
            {
                targetPosition = playerMovement.colliderPosition + new Vector3(0, 0, -10);
            }
            else
            {
                targetPosition = target.TransformPoint(new Vector3(0, 0.1f, -10));
            }


            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        if (playerMovement.collidingWith == "cameraDetach" )
        {
            targetPosition = playerMovement.colliderPosition + new Vector3(0, 0, -10);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            locked = true;       
        }



        

    }

}
