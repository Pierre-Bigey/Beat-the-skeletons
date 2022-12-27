using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonLocomotion : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private GameObject player;
    private PlayerManager playerManager;
    private SkeletonManager skeletonManager;

    [SerializeField] private float movementSpeed = 0.8f;
    [SerializeField] private float rotationSpeed = 15;

    

    // Start is called before the first frame update
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        skeletonManager = GetComponent<SkeletonManager>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
    }

    public float GetMoveAmount()
    {
        float moveAmount = navAgent.velocity.magnitude* movementSpeed;
        
        return moveAmount;
    }

    public void HandleAllMovement()
    {
        if (playerManager != null)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private void HandleMovement()
    {
        if(!playerManager.isDead)
        {
            navAgent.SetDestination(player.transform.position);
        }
    }

    private void HandleRotation()
    {
        Vector3 targetDirection;
        if (!skeletonManager.isAttacking && !playerManager.isDead)
        {
            targetDirection = navAgent.velocity;
        }
        else
        {
            targetDirection = player.transform.position - transform.position;
        }
        
        targetDirection.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Quaternion skeletonRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = skeletonRotation;
    }
}
