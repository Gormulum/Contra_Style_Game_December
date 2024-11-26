using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public PlayerInputActions playerInput;


    public float playerSpeed = 5f;
    public float airSpeed = 2f;
    public float jumpForce = 0f;
    public float maxVelocity;
    public float minVelocity;

    bool grounded;

    public float baseMaxVelocity;
    public float baseMinVelocity;
    public float dashMaxVelocity;
    public float dashMinVelocity;


    Vector2 moveDirection = Vector2.zero;
    Vector2 mousePosition = Vector2.zero;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    [SerializeField] private Transform bullet;
    [SerializeField] private Transform gunEndPoint;
    

    InputAction move;
    InputAction fire;
    InputAction jump;
    InputAction dash;

    public Transform cameraTarget;

    [SerializeField] private Cooldown dashCooldown;
    [SerializeField] private Cooldown dashDuration;
    [SerializeField] private Cooldown bulletCooldown;

    public string collidingWith = "";
    public Vector3 colliderPosition = Vector3.zero;

    

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

        if (moveDirection.x == 1)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDirection.x == -1)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

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


        cameraTarget.position = rb.position;
    }

    private void FixedUpdate()
    {
        
        Vector3 v = rb.velocity;
        v.x = Mathf.Clamp(rb.velocity.x, minVelocity, maxVelocity);
        rb.velocity = v;
        
        rb.AddForce(new Vector2(moveDirection.x * playerSpeed, 0), ForceMode2D.Impulse);


        
    }


    //input actions

    //Projectile Firing
    void Fire(InputAction.CallbackContext context)
    {
        if (bulletCooldown.IsCoolingDown == true)
        {
            return;
        }

        Transform bulletTransform = Instantiate(bullet, gunEndPoint.position, Quaternion.identity);

        Vector3 shootDir = ((this.transform.position + new Vector3(0, 1, 0)) - gunEndPoint.position).normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);

        bulletCooldown.StartCooldown();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CameraLockOn")
        {
            collidingWith = "cameraLockOn";
            colliderPosition = other.transform.position;
        }
        else if (other.tag == "CameraDetach")
        {
            
            collidingWith = "cameraDetach";
            colliderPosition = other.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CameraLockOn")
        {
            collidingWith = "";
            colliderPosition = this.transform.position;
        }

        if (collision.tag == "CameraDetach")
        {
            collidingWith = ""; 
        }
    }
}
    