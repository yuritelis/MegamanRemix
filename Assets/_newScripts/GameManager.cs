using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public CameraFollow mycamera;

    [Header("Respawn Settings")]
    public GameObject playerPrefab;
    public GameObject respawnPoint;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(playerPrefab, respawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("O jogador j� existe na cena.");
        }
    }

    public void RespawnPlayer()
    {
        StartCoroutine(DoRespawn(1.0f));
    }

    private IEnumerator DoRespawn(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (playerPrefab != null && respawnPoint != null)
        {
            GameObject playerInstance = Instantiate(playerPrefab, respawnPoint.transform.position, Quaternion.identity);

            yield return null;

            if (mycamera != null)
            {
                mycamera.SetPlayer(playerInstance);
            }
            else
            {
                Debug.LogError("A c�mera n�o est� atribu�da no GameManager!");
            }
        }
        else
        {
            Debug.LogError("Player prefab ou respawn point n�o est�o atribu�dos no GameManager!");
        }
    }
}