using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInputController inputController;
    public Vector2 inputDirection;
    [Header("移动速度")]
    public float speed;
    [Header("跳跃力度")]
    public float jumpForce;
    [Header("二段跳力度")]
    public float secondJumpForce;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PhysicsCheck physicsCheck;
    private PlayerStatus playerStatus;
    private PlayerAttack playerAttack;
    private void Awake()
    {
        inputController = new PlayerInputController();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        inputController.Gameplay.Jump.started += Jump;
        physicsCheck = GetComponent<PhysicsCheck>();
        playerStatus = GetComponent<PlayerStatus>();
        playerAttack = GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        inputDirection = inputController.Gameplay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        if(!playerStatus.isKnockedBack)
            Move();
    }
    private void OnEnable()
    {
        inputController.Enable();
    }
    private void OnDisable()
    {
        inputController.Disable();
    }
    public void Move()
    {
        if(playerAttack.isAttack)
        {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime * 0.27f, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        }
        if(inputDirection.x > 0)
        {
            sr.flipX = false;
        }
        else if (inputDirection.x < 0)
        {
            sr.flipX = true;
        }
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        if ((physicsCheck.isGround == true && physicsCheck.isAir == false) || (physicsCheck.isAir == true && physicsCheck.canDoubleJump == true))
        {
            if (physicsCheck.isGround == true && physicsCheck.isAir == false)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
            if (physicsCheck.isAir == true && physicsCheck.canDoubleJump == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(transform.up * secondJumpForce, ForceMode2D.Impulse);
                physicsCheck.canDoubleJump = false;
            }

        }
    }
}
