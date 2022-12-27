using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private PlayerLocomotion playerLocomotion;
    private PlayerAction playerAction;
    private CameraManager cameraManager;
    private AnimatorManager animatorManager;
    private MainUIHandler mainUIHandler;
    private PlayerAudio playerAudio;

    [SerializeField] private GameObject rightWrist;
    private CapsuleCollider rightWristCollider;
    private BoxCollider shieldCollider;
    private CapsuleCollider bodyCollider;
    private GameManager GM;

    [SerializeField] private GameObject hitParticle;
    [Header("Health settings")]
    [SerializeField] private float recuperationTime = 0.2f;
    [SerializeField] private float initialHealth = 5;
    public float health { get; private set; }
    private float lastTimeHit;
    public bool isHit { get; private set; }
    public bool isDead { get; private set; }

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerAction = GetComponent<PlayerAction>();
        animatorManager = GetComponent<AnimatorManager>();
        playerAudio = GetComponent<PlayerAudio>();

        rightWristCollider = rightWrist.GetComponent<CapsuleCollider>();
        SetWristCollider(false);



        
        shieldCollider = GetComponent<BoxCollider>();
        bodyCollider = GetComponent<CapsuleCollider>();

        health = initialHealth;
        lastTimeHit = Time.time;
        isHit = false;
        isDead = false;

        
    }

    private void Start()
    {
        //Debug.Log("\n-------------------------------\nPLAYER MANAGER START !!!!\n------------------------------\n");
        GM = GameManager.Instance;
        cameraManager = FindObjectOfType<CameraManager>();
        mainUIHandler = GameObject.FindObjectOfType<MainUIHandler>();
        resetScore();
        SetCursorEnabled(false);
    }

    private void Update()
    {
        if (health <= 0 && !isDead)
        {
            Die();
        }
        
        inputManager.HandleAllInputs();
        
        
    }

    private void FixedUpdate()
    {
        if (!isHit && !isDead)
        {
            playerLocomotion.HandleAllMouvement();
        }
        
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();

        //playerAction.isPunching = animator.GetBool("isPunching");
    }

    private void Die()
    {
        //Debug.Log("Player Die");
        SetCursorEnabled(true);
        isDead = true;
        playerAudio.PlayDeathSound();
        animatorManager.PlayTargetAnimation("Death");
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<CapsuleCollider>().enabled = false;
        mainUIHandler.updateDeathMenu(true);
    }

    public void SetWristCollider(bool value)
    {
        rightWristCollider.enabled = value;
    }

    public void updateBlocking(bool value)
    {
        
        if (shieldCollider == null)
        {
            shieldCollider = GetComponent<BoxCollider>();
        }
        if(bodyCollider == null)
        {
            bodyCollider = GetComponent<CapsuleCollider>();
        }
        bodyCollider.enabled = !value;
        shieldCollider.enabled = value;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Skeleton_Hand") && !playerAction.isBlocking)
        {
            
            Hit(other.transform.position);
        }
    }

    private void Hit(Vector3 position)
    {
        if (Time.time - lastTimeHit > recuperationTime && !isDead && !isHit)
        {
            //Debug.Log("Player hit !");
            isHit = true;
            playerAudio.PlayHitSound();
            Instantiate(hitParticle, position, hitParticle.transform.rotation);
            lastTimeHit = Time.time;
            //Debug.Log("Skeleton hit");
            animatorManager.PlayTargetAnimation("GetHit");
            health -= 1;

            mainUIHandler.UpdateHealthBar();
        }
    }

    public void addPoints(int value)
    {
        GM.SetCurrentScore(GM.currentScore + value);
        mainUIHandler.UpdateScoreText();
    }

    public void resetScore()
    {
        GM.SetCurrentScore(0);
        mainUIHandler.UpdateScoreText();
    }
    public void resetIsHit()
    {
        isHit = false;
    }

    private void SetCursorEnabled(bool value)
    {
        Debug.Log("in PlayerManager, setting cursor as : " + value);
        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.visible = value;
    }
}
