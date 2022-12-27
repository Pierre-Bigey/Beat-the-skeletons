using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHand : MonoBehaviour
{
    private CapsuleCollider colliderHand;
    [SerializeField] private bool isPlayerHand;

    private void Awake()
    {
        colliderHand = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !isPlayerHand)
        {
            colliderHand.enabled = false;
        }
        if(other.gameObject.CompareTag("Skeleton") && isPlayerHand)
        {
            //Debug.Log("Player hand touched something !");
            colliderHand.enabled = false;
        }
    }
}
