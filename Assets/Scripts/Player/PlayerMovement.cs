using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 velocity;
    private bool isGrounded;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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


        transform.Translate(new Vector3(moveInput * moveSpeed * Time.deltaTime, 0, 0));
    }
}
