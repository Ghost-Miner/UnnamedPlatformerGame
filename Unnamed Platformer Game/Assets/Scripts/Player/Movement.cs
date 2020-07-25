using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    bool facingRight = true;

    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWater;
    public float jumpForce;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallJumping;
    public float wallJumpTime;
    public float xWallForce;
    public float yWallForce;
    bool wallSliding;
    public float wallSlidingSpeed;
    public float maxFallSpeed;
    float fallSpeed;
    bool lethal;
    bool isInWater;

    public ParticleSystem dirtParticles;

    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isGrounded)
		{
            fallSpeed = Math.Abs(rb.velocity.y);

            if (fallSpeed > maxFallSpeed)
			{
                lethal = true;
			} else
			{
                lethal = false;
			}
		}

        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if (input > 0 && facingRight == false)
        {
            Flip();
        }
        else if (input < 0 && facingRight == true)
        {
            Flip();
        }

        if (input != 0)
		{
            anim.SetBool("isWalking", true);
            //dirtParticles.Play();
		} else
		{
            anim.SetBool("isWalking", false);
            //dirtParticles.Stop();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        isInWater = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsWater);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true || Input.GetKeyDown(KeyCode.Space) && isInWater == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isGrounded && isInWater == false && lethal)
		{
            SceneManager.LoadScene("Game");
		}

        if (isTouchingFront && !isGrounded && input != 0)
        {
            wallSliding = true;
            anim.SetBool("sliding", true);
        }
        else
        {
			wallSliding = false;
            anim.SetBool("sliding", false);
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -input, yWallForce);
        }


    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
}
