using FMOD.Studio;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float crouchSpeedMultiplier = 0.5f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float lowJumpMultiplier = 3f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Reference to the sprite
    public bool isGrounded;
    private bool isCrouching;
    private float horizontalInput;
    private float lastGroundedTime;
    private float lastJumpPressedTime;
    private bool facingRight = true; // Track facing direction

    private EventInstance headBumpInstance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck is not assigned! Please assign a Transform under the player.");
        }

        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isCrouching = Input.GetKey(KeyCode.LeftControl);
        isGrounded = IsGrounded();

        if (isGrounded) lastGroundedTime = Time.time;
        if (Input.GetButtonDown("Jump")) lastJumpPressedTime = Time.time;

        if ((Time.time - lastJumpPressedTime <= jumpBufferTime) && (Time.time - lastGroundedTime <= coyoteTime))
        {
            Jump();
            lastJumpPressedTime = -1f;
        }

        FlipCharacter(); // Flip sprite when changing direction
    }

    private void FixedUpdate()
    {
        Move();
        ApplyBetterJumpingPhysics();
    }

    private void Move()
    {
        float targetSpeed = (isCrouching ? moveSpeed * crouchSpeedMultiplier : moveSpeed) * horizontalInput;
        float speedDifference = targetSpeed - rb.linearVelocity.x;

        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = speedDifference * accelRate * Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x + movement, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void ApplyBetterJumpingPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private void FlipCharacter()
    {
        if (horizontalInput > 0 && !facingRight)
        {
            facingRight = true;
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0 && facingRight)
        {
            facingRight = false;
            spriteRenderer.flipX = true;
        }
    }


 
}
