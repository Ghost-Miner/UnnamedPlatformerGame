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
	public float raycastToGroundLength;
	public int smooth = 10;
	Quaternion rot;

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
	bool crouch = false;

    public ParticleSystem dirtParticles;

    Animator anim;

    public GameObject DeathScreen;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        var xCheck = PlayerPrefs.GetFloat("x", 0);
        var yCheck = PlayerPrefs.GetFloat("y", 0);
        gameObject.transform.position = new Vector3(xCheck, yCheck, 0);
		
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

        if (Input.GetButtonDown("Jump") && (isGrounded == true || isInWater == true))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (isGrounded && !isInWater && lethal)
		{
            Dead();
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

        if (Input.GetButtonDown("Jump") && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -input, yWallForce);
        }
		
		//search>crouch
		if (Input.GetButtonDown("Crouch"))
		{
			crouch = !crouch;
			anim.SetBool("crouch", crouch);
		}
		
		transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * smooth);              


    }
	
	void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject boxCollider = collision.otherCollider.gameObject;
		string boxColliderName = boxCollider.name;
		
		GameObject otherCollider = collision.gameObject;
		string otherColliderName = boxCollider.name;
		bool inWater = otherColliderName == "water";
		
		CollisionEvent(boxColliderName, true, inWater);
		
    }
	
	void OnCollisionExit2D(Collision2D collision)
    {
        GameObject boxCollider = collision.otherCollider.gameObject;
		string boxColliderName = boxCollider.name;
		
		GameObject otherCollider = collision.gameObject;
		bool inWater = otherCollider.name == "water";
		
		CollisionEvent(boxColliderName, false, inWater);
		
    }
	
	void CollisionEvent(string name, bool mode, bool inWater)
	{
		isInWater = inWater && mode;
		switch(name)
		{
			case "Bottom":
				isGrounded = mode;
				
				break;
			case "Right":
				isTouchingFront = mode;
				break;
		}
	}
	
	void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.gameObject.name == "Ground")
		{
			Vector2 pos = groundCheck.position;
			int temp = 0;
			Vector2 tempVec = new Vector2(0f,0f);
			for (float x = -0.4f; x <=0.4; x+= 0.1f)
			{
				Vector2 offset = new Vector2(groundCheck.right.x,groundCheck.right.y)*x;
				RaycastHit2D hit = Physics2D.Raycast(pos + offset, - Vector2.up, raycastToGroundLength, whatIsGround);
				Debug.DrawRay(pos + offset, - Vector2.up *raycastToGroundLength, Color.green);
				if(hit){
					temp++;
					tempVec += hit.normal;
				}
			}
			rot = Quaternion.FromToRotation(transform.up, tempVec/temp) * transform.rotation;
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

    public void Dead()
    {
        DeathScreen.SetActive(true);
    }
}
