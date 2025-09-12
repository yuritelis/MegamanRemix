using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHpController : MonoBehaviour
{
    [Header("Health Settings")]
    public int hpAtual;
    public int hpMax = 4;

    private SpriteRenderer spriteRenderer;

    int danoMissil = 2;
    int danoInimigo = 1;

    private bool isInvulnerable = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hpAtual = hpMax;

        if (HpUiControl.instance != null)
        {
            HpUiControl.instance.SetMaxHearts(hpMax);
        }
        else
        {
            Debug.LogError("HpUiControl.instance é null. A UI de HP não pode ser inicializada.");
        }
    }

    private void Update()
    {
        if (hpAtual <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isInvulnerable)
        {
            return;
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage(danoInimigo);
        }
        else if (collision.collider.CompareTag("Damage"))
        {
            TakeDamage(danoMissil);
        }
    }

    public void TakeDamage(int dano)
    {
        hpAtual -= dano;

        if (HpUiControl.instance != null)
        {
            HpUiControl.instance.UpdatedHearts(hpAtual);
        }
        else
        {
            Debug.LogError("HpUiControl.instance é null. A UI de HP não pode ser atualizada.");
        }

        StartCoroutine(BlinkSprite());
    }

    private IEnumerator BlinkSprite()
    {
        isInvulnerable = true;

        float duration = 0.6f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        isInvulnerable = false;
    }

    void Die()
    {
        GameManager.instance.RespawnPlayer();
        Destroy(gameObject);
    }
}