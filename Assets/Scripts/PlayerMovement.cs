using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public PlayerInputActions playerInput;

    public float playerSpeed = 5f;
    public float airSpeed = 2f;
    public float jumpForce = 0f;
    public float maxVelocity;
    public float minVelocity;



    public float baseMaxVelocity;
    public float baseMinVelocity;
    public float dashMaxVelocity;
    public float dashMinVelocity;


    Vector2 moveDirection = Vector2.zero;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;


    InputAction move;
    InputAction fire;
    InputAction jump;
    InputAction dash;

    public Transform cameraTarget;

    [SerializeField] private Cooldown dashCooldown;
    [SerializeField] private Cooldown dashDuration;


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

        dash = playerInput.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
    }
    private void OnDisable()
    {
        //playerInput.Disable();
        move.Disable();
        fire.Disable();
        jump.Disable();
        dash.Disable();
    }

    //update functions
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        if (dashCooldown.IsCoolingDown == false)
        {
            maxVelocity = baseMaxVelocity;
            minVelocity = baseMinVelocity;
        }

        if (dashCooldown.IsCoolingDown == false)
        {
            playerSpeed = 3;
            playerInput.Enable();
        }

        if (isGrounded() == true)
        {
            rb.drag = 6f;
        }
        else
        {
            rb.drag = 0.4f;
        }
    }

    private void FixedUpdate()
    {
        
        Vector3 v = rb.velocity;
        v.x = Mathf.Clamp(rb.velocity.x, minVelocity, maxVelocity);
        rb.velocity = v;
        
        rb.AddForce(new Vector2(moveDirection.x * playerSpeed, 0), ForceMode2D.Impulse);

    }


    //input actions
    void Fire(InputAction.CallbackContext context)
    {
        
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded() == true)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            
        }
        
        
    }

    void Dash(InputAction.CallbackContext context)
    {
        if (dashDuration.IsCoolingDown == true)
        {
            return;
        }
        if (dashCooldown.IsCoolingDown == true)
        {
            return;
        }
        
        



        maxVelocity = dashMaxVelocity;
        minVelocity = dashMinVelocity;
        playerSpeed = 12;
        //playerInput.Disable();

        dashDuration.StartCooldown();
        dashCooldown.StartCooldown();
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
