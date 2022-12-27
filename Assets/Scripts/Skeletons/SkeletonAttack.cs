using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;
    private AnimatorManager animatorManager;
    private SkeletonManager skeletonManager;

    [SerializeField] private GameObject rightWrist;
    private CapsuleCollider wristCollider;

    [SerializeField] private float rotationSpeed = 15;
    [SerializeField] private float attackRate = 1;
    private float lastTimeAttacked;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerManager = player.GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        wristCollider = rightWrist.GetComponent<CapsuleCollider>();
        skeletonManager = GetComponent<SkeletonManager>();

        lastTimeAttacked = Time.time;
        SetWristCollider(false);
    }

    public void HandleRotation()
    {
        Vector3 targetDirection = player.transform.position - transform.position;
        targetDirection.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Quaternion skeletonRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = skeletonRotation;
    }
    public void Attack()
    {
        SetWristCollider(true);
        skeletonManager.isAttacking = true;
        animatorManager.PlayTargetAnimation("Attack");
    }

    public void HandleAttack()
    {
        HandleRotation();
        if(Time.time - lastTimeAttacked > attackRate && !playerManager.isDead)
        {
            lastTimeAttacked = Time.time;

            Attack();
        }
    }

    public void SetWristCollider(bool value)
    {
        wristCollider.enabled = value;
    }
}
