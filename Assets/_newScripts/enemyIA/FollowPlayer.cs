using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject target;
    Rigidbody2D rb;
    SpriteRenderer sprite;

    private bool isFacingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 dif = target.transform.position - transform.position;
            rb.AddForce(dif);
        }

        if(rb.linearVelocityX > 0.01f)
        {
            Debug.Log("Going direita");
            sprite.flipX = false;
        }
        else if (rb.linearVelocityX < -0.01f)
        {
            Debug.Log("Going esquerda");
            sprite.flipX= true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
            Debug.Log("achou player");
        }
    }

    void FlipSprite()
    {
        Vector3 ls = transform.localScale;
        ls.x *= -1f;
        transform.localScale = ls;
    }
}