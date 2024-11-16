using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float airMovementBoundLength = 1;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 velocity;
    private Vector2 playerPos;
    private bool isGrounded;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        PlayerTrigger.OnGroundExit += ChangeGroundState;
        PlayerTrigger.OnGroundEnter += ChangeGroundState;
    }
    private void OnDisable()
    {
        PlayerTrigger.OnGroundExit -= ChangeGroundState;
        PlayerTrigger.OnGroundEnter -= ChangeGroundState;
    }
    private void FixedUpdate()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");

        // trigger anim
        if (moveInput != 0)
            animator.SetBool("isRunning", true);

        else
            animator.SetBool("isRunning", false);

        // flip rotation
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if(moveInput < 0)
            spriteRenderer.flipX = true;

        if(isGrounded)
            transform.Translate(new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0));
        else
        {
            transform.Translate(new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0));
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, playerPos.x - airMovementBoundLength, playerPos.x + airMovementBoundLength), transform.position.y, 0);
        }
    }
    private void ChangeGroundState(bool isGround)
    {
        isGrounded = isGround;
        playerPos= transform.position;
    }
}
