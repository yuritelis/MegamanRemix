using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunicaoIa : MonoBehaviour
{
    public ParticleSystem explosion;

    void Start()
    {
        StartCoroutine(ActivateCollider());

        Destroy(gameObject, 4);
        GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
    }

    private IEnumerator ActivateCollider()
    {
        yield return null;

        GetComponent<Collider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            explosion.Play();
            Destroy(gameObject, 0.6f);
        }
    }
}