using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 1f;
    bool isFacingRight = true;
    float jumpForce = 5f;
    bool isGrounded;

    bool jump, doublejump, jumpagain;
    float jumptime, jumptimeside;
    float xmov;

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
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        /*if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("isJumping", !isGrounded);
        }*/

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
        //anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (jumptimeside < 0.1f)
            rb.AddForce(new Vector2(xmov * 20 / (rb.linearVelocity.magnitude + 1), 0));

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit)
        {
            anim.SetFloat("yVelocity", hit.distance);
            if (jumptimeside < 0.1)
                JumpRoutine(hit);
        }

        RaycastHit2D hitright;

        hitright = Physics2D.Raycast(transform.position + Vector3.up * 0.5f, transform.right, 1);
        if (hitright)
        {
            if (hitright.distance < 0.3f && hit.distance > 0.5f)
            {
                JumpRoutineSide(hitright);
            }
            Debug.DrawLine(hitright.point, transform.position + Vector3.up * 0.5f);
        }
    }

    private void JumpRoutine(RaycastHit2D hit)
    {
        if (hit.distance < 0.1f)
        {
            jumptime = 1;
        }

        if (jump)
        {
            jumptime = Mathf.Lerp(jumptime, 0, Time.fixedDeltaTime * 10);
            rb.AddForce(Vector2.up * jumptime, ForceMode2D.Impulse);
            if (rb.linearVelocity.y < 0)
            {
                jumpagain = false;
            }
        }

    }

    private void JumpRoutineSide(RaycastHit2D hitside)
    {
        if (hitside.distance < 0.3f)
        {
            jumptimeside = 6;
        }

        if (doublejump)
        {
            // PhisicalReverser();
            jumptimeside = Mathf.Lerp(jumptimeside, 0, Time.fixedDeltaTime * 10);
            rb.AddForce((hitside.normal + Vector2.up) * jumptimeside, ForceMode2D.Impulse);
        }
    }


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
