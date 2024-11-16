using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float airMovementBoundLength = 1;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int maxJumps = 2;
    private int jumpCount = 0;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private ParticleSystem dustParticle;

    private Vector2 velocity;
    private Vector2 playerPos;
    private bool isGrounded;
    private bool isJump = false;
    float moveInput;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        PlayerTrigger.OnGroundExit += ChangeGroundState;
        PlayerTrigger.OnGroundEnter += ChangeGroundState;
        PlayerTrigger.OnGroundEnter += ResetJumpCount;
        GravityController.OnGravityChange += GravityChanged;
    }
    private void OnDisable()
    {
        PlayerTrigger.OnGroundExit -= ChangeGroundState;
        PlayerTrigger.OnGroundEnter -= ChangeGroundState;
        PlayerTrigger.OnGroundEnter -= ResetJumpCount;
        GravityController.OnGravityChange -= GravityChanged;

    }
    private void FixedUpdate()
    {
        // trigger anim
        if (moveInput != 0)
            animator.SetBool("isRunning", true);

        else
        {
            animator.SetBool("isRunning", false);
            dustParticle.Stop();
        }

        // flip rotation
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;


        if (isGrounded || isJump)
        {
            transform.Translate(new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0));
            if (!dustParticle.isPlaying && !isJump)
                dustParticle.Play();
        }
        else
        {
            transform.Translate(new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0));
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, playerPos.x - airMovementBoundLength, playerPos.x + airMovementBoundLength), transform.position.y, 0);
        }


    }
    private void ChangeGroundState(bool isGround)
    {
        Debug.Log("Is Grounded : " + isGround);
        isGrounded = isGround;
        if (!isGround)
            playerPos = transform.position;
        else
        {
            if (animator.GetBool("isJumping"))
            {
                animator.SetBool("isJumping", false);
            }
            if (animator.GetBool("isFalling"))
            {
                animator.SetBool("isFalling", false);
            }
        }
    }
    private void Jump()
    {
        if (isGrounded)
        {
            animator.SetBool("isJumping", true);
            isJump = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force
            isGrounded = false; // Temporarily set to false until collision checks
            dustParticle.Stop();
            jumpCount++;
        }

    }
    private void ResetJumpCount(bool isGround)
    {
        if (isGround)
        {
            isJump = false;
            jumpCount = 0;
        }
    }
    private void GravityChanged(bool isFlipped)
    {
        jumpForce *= -1;
        animator.SetBool("isFalling", true);
    }
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }
    }

}
