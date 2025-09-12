using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject target;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    private bool isFacingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 dif = target.transform.position - transform.position;

            rb.AddForce(dif);
        }

        if (rb.linearVelocityX > 0.01f)
        {
            sprite.flipX = false;
        }

        else if (rb.linearVelocityX < -0.01f)
        {
            sprite.flipX = true;
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
        }
    }
}