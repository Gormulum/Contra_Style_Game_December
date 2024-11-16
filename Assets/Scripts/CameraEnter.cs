using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class CameraEnter : MonoBehaviour
{

    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    public Transform cameraTarget;
    bool cameraLock;
    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter.Invoke();
        cameraLock = true;
    }

    private void Update()
    {
        if (cameraLock == true)
        {
            cameraTarget.position = new Vector3(this.transform.position.x, this.transform.position.y -3, this.transform.position.z);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit.Invoke();
        cameraLock = false;
    }


}
