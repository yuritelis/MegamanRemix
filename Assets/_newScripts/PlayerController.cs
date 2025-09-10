using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 1f;
    float jumpForce = 5f;

    bool isFacingRight = true;
    bool isGrounded;
    bool doubleJump;

    [SerializeField] GameObject particleObject;

    Rigidbody2D rb;
    Animator anim;
    ParticleSystem fire;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fire = particleObject.GetComponent<ParticleSystem>();

        anim.SetBool("isShooting", false);
    }

    void Update()
    {
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 0.5f, Color.red);

        isGrounded = hit;

        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();
        //GroundCheck();

        if(Input.GetButtonDown("Jump") && isGrounded || Input.GetButtonDown("Jump") && doubleJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetBool("isJumping", !isGrounded);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("isShooting", true);
            fire.Emit(1);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("isShooting", false);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2 (horizontalInput * moveSpeed, rb.linearVelocity.y);
        anim.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    /*private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
    }*/

    void FlipSprite()
    {
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Ground"));
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        anim.SetBool("isJumping", !isGrounded);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damage") || collision.collider.CompareTag("Enemy"))
        {
            LevelManager.instance.LowDamage();
        }
    }
}