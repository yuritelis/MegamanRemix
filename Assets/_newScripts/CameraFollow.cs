using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    Rigidbody2D rb;
    [SerializeField]
    float advanceFactor = 2;

    void LateUpdate()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                SetPlayer(player);
            }
        }

        if (target != null)
        {
            if (rb)
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(target.transform.position.x + rb.linearVelocity.x * advanceFactor,
                    target.transform.position.y,
                    transform.position.z), Time.smoothDeltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(target.transform.position.x,
                    target.transform.position.y,
                    transform.position.z), Time.smoothDeltaTime);
            }
        }
    }

    /// <summary>
    /// Seta o jogador na camera e tenta obter o Rigidbody2D.
    /// </summary>
    /// <param name="tgt">jogador</param>
    public void SetPlayer(GameObject tgt)
    {
        target = tgt;
        if (target != null)
        {
            rb = target.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogWarning("O jogador não tem um Rigidbody2D. A câmera não usará o fator de avanço.");
            }
        }
        else
        {
            Debug.LogWarning("SetPlayer foi chamado com um alvo nulo.");
        }
    }
}