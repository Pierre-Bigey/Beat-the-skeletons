using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonManager : MonoBehaviour
{
    private SkeletonLocomotion skeletonLocomotion;
    private AnimatorManager animatorManager;
    private SkeletonAttack skeletonAttack;
    private PlayerManager playerManager;
    private SkeletonAudio skeletonAudio;

    [Header("General Settings")]
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject disapearParticle;
    [SerializeField] private int killingPoints = 10;
    [SerializeField] private int hitingPoints = 1;

    private bool isDead = false;
    private bool isHit;
    public bool isAttacking { get; set; }

    [Header("Health settings")]
    //[SerializeField] private float recuperationTime = 0.5f;
    [SerializeField] private float initialHealth = 5;
    private float health;
    private float lastTimeHit;

    [Header("Attack settings")]
    [SerializeField] private float attackDistance = 0.1f;

    private float moveAmount;

    // Start is called before the first frame update
    void Awake()
    {
        isDead = false;
        isAttacking = false;

        skeletonLocomotion = GetComponent<SkeletonLocomotion>();
        animatorManager = GetComponent<AnimatorManager>();
        skeletonAttack = GetComponent<SkeletonAttack>();
        skeletonAudio = GetComponent<SkeletonAudio>();

        health = initialHealth;
        lastTimeHit = Time.time;
    }

    private void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if(moveAmount <= attackDistance &&!isAttacking &&!isHit )
            {
                //Debug.Log("Attacking !!!");
                //isAttacking = true;
                if (playerManager.isDead)
                {
                    HandleMotionAnimation();

                }
                else
                {
                    skeletonAttack.HandleAttack();
                }
                
            }
            else
            {
                HandleMotionAnimation();
            } 
        }
    }

    private void HandleMotionAnimation()
    {
        moveAmount = Mathf.Clamp01(Mathf.Abs(skeletonLocomotion.GetMoveAmount()));
        //Debug.Log("skeleton move amount : " + moveAmount);
        animatorManager.UpdateAnimatorValues(0, moveAmount);
        //Debug.Log("Vertical value for Skeleton : " + moveAmount);
    }

    private void FixedUpdate()
    {
        if (!isDead && !isHit )
        {
            skeletonLocomotion.HandleAllMovement();
        }
        
    }

    private void LateUpdate()
    {
        checkDeath();
    }

    private void checkDeath()
    {
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("Skeleton Die");
        isDead = true;
        animatorManager.PlayTargetAnimation("Death");
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<CapsuleCollider>().enabled = false;
        playerManager.addPoints(killingPoints);
        skeletonAudio.PlayDeathSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered by " + other.gameObject.name);
        if (other.gameObject.CompareTag("Player_Hand"))
        {
            Hit(other.transform.position);
        }
    }

    private void Hit(Vector3 position)
    {
        if (!isHit && !isDead)
        {
            isHit = true;
            Instantiate(hitParticle, position, hitParticle.transform.rotation);
            lastTimeHit = Time.time;
            //Debug.Log("Skeleton hit");
            animatorManager.PlayTargetAnimation("GetHit");
            health -= 1;
            playerManager.addPoints(hitingPoints);
            skeletonAudio.PlayHitSound();
        }
    }

    public void resetHit()
    {
        isHit = false;
    }

    public void setIsAttacking(bool value)
    {
        isAttacking = false;
    }

    public void DestroySkeleton()
    {
        var particles = Instantiate(disapearParticle, this.transform.position, disapearParticle.transform.rotation);
        Destroy(particles, 4);
        Destroy(this.gameObject);
    }
}
