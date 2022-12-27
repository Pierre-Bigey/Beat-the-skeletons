using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    private BoxCollider shieldCollider;
    private PlayerManager playerManager;

    private void Awake()
    {
        shieldCollider = GetComponent<BoxCollider>();
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        

    }

}
