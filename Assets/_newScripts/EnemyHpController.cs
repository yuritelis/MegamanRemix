using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpController : MonoBehaviour
{
    [SerializeField] private int hpMax = 4;
    [SerializeField] ParticleSystem smoke;
    [SerializeField] ParticleSystem explosion;

    int hpAtual;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        hpAtual = hpMax;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (!isInvulnerable)
        {
            hpAtual--;

            StartCoroutine(Blink(0.2f));

            if (hpAtual <= (hpMax / 2) && hpAtual > 0)
            {
                CreateAndPlayParticle(smoke);
            }
            else if (hpAtual <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        CreateAndPlayParticle(explosion);
        spriteRenderer.enabled = false;
        Destroy(gameObject, 0.8f);
    }

    void CreateAndPlayParticle(ParticleSystem particlePrefab)
    {
        if (particlePrefab)
        {
            GameObject particleInstance = Instantiate(particlePrefab.gameObject, transform.position, Quaternion.identity);
            particleInstance.transform.parent = transform;
            particleInstance.GetComponent<ParticleSystem>().Play();

            Destroy(particleInstance, 2f);
        }
    }

    IEnumerator Blink(float duration)
    {
        isInvulnerable = true;
        float blinkInterval = 0.1f;
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkInterval);
        }
        spriteRenderer.color = Color.white;
        isInvulnerable = false;
    }
}