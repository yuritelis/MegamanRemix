using UnityEngine;
using UnityEngine.UI;

public class HpUiControl : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    private static HpUiControl _instance;

    public static HpUiControl instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HpUiControl>();
                if (_instance == null)
                {
                    Debug.LogError("Não há um objeto HpUiControl na cena.");
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        // DontDestroyOnLoad(gameObject); 
    }

    public void SetMaxHearts(int maxHp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < maxHp;
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdatedHearts(int atualHp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < atualHp)
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