using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public PlayerInputActions playerInput;

    public float playerSpeed = 5f;
    public float jumpForce = 0f;
    public float maxVelocity;
    public float minVelocity;


    Vector2 moveDirection = Vector2.zero;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;


    InputAction move;
    InputAction fire;
    InputAction jump;

    public Transform cameraTarget;



    //initalization
    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    //enabled and disabled input actions
    private void OnEnable()
    {
        //playerInput.Enable();

        
        move = playerInput.Player.Move;
        move.Enable();

        fire = playerInput.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
        
        jump = playerInput.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }
    private void OnDisable()
    {
        //playerInput.Disable();
        move.Disable();
        fire.Enable();
        jump.Disable();
    }

    //update functions
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(moveDirection.x * playerSpeed, 0), ForceMode2D.Impulse);


        Vector3 v = rb.velocity;
        v.x = Mathf.Clamp(rb.velocity.x, minVelocity, maxVelocity);
        rb.velocity = v;
    }


    //input actions
    void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired!");
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded() == true)
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }
        
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
