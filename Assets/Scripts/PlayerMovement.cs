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
    

    Vector2 moveDirection = Vector2.zero;

    InputAction move;
    InputAction fire;
    InputAction jump;

    // Start is called before the first frame update

    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * playerSpeed, rb.velocity.y);
    }

    void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fired!");
    }
    void Move(InputAction.CallbackContext context)
    {
        Debug.Log("moved");
    }

    void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }
}
