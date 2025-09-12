using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metralhadora : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private GameObject missileReference;

    [SerializeField]
    private GameObject firepoint;

    [SerializeField]
    private SpriteRenderer cannonSpriteRenderer;

    [SerializeField]
    int state = 0;

    float cooldown = 0;

    private Vector3 initialFirepointLocalPosition;

    private void Start()
    {
        initialFirepointLocalPosition = firepoint.transform.localPosition;
    }

    private void Update()
    {
        if (cannonSpriteRenderer.flipX)
        {
            firepoint.transform.localPosition = new Vector3(-initialFirepointLocalPosition.x, initialFirepointLocalPosition.y, initialFirepointLocalPosition.z);
        }
        else
        {
            firepoint.transform.localPosition = initialFirepointLocalPosition;
        }

        switch (state)
        {
            case 0:
                Idle();
                break;
            case 1:
                Aim();
                break;
        }
    }

    void Idle()
    {

    }

    void Aim()
    {
        if (cooldown <= 0)
        {
            Quaternion missileRotation = firepoint.transform.rotation;

            if (cannonSpriteRenderer.flipX)
            {
                missileRotation = Quaternion.Euler(0, 0, firepoint.transform.rotation.eulerAngles.z + 180);
            }

            Instantiate(missileReference, firepoint.transform.position, missileRotation);
            cooldown = 1;
        }
        cooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
            state = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            state = 0;
        }
    }
}