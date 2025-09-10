using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHpController : MonoBehaviour
{
    [Header("Health Settings")]
    public int hpAtual = 4;
    public int hpMax = 4;

    public HpUiControl hpUI;
    private SpriteRenderer spriteRenderer;

    int danoMissil = 2;
    int danoInimigo = 1;

    void Start()
    {
        hpUI.SetMaxHearts(hpMax);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(hpAtual <= 0)
        {
            LevelManager.instance.LowDamage();
            Start();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage(danoInimigo);
        }
        else if (collision.collider.CompareTag("Damage"))
        {
            TakeDamage(danoMissil);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int dano)
    {
        hpAtual -= dano;
        Debug.Log($"[{gameObject.name}] Dano: {dano}, HP restante: {hpAtual}");

        hpUI.UpdatedHearts(hpAtual);

        StartCoroutine(FlashRed());
        StartCoroutine(BlinkSprite());
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator BlinkSprite()
    {
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
    }
}