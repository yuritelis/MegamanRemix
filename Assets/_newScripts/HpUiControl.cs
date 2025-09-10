using UnityEngine;
using UnityEngine.UI;

public class HpUiControl : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    public void SetMaxHearts(int hpMax)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < hpMax;
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdatedHearts(int hpAtual)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hpAtual)
            {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
                hearts[i].enabled = false;
            }
        }
    }
}