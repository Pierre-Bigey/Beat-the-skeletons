using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private AnimatorManager animatorManager;
    private PlayerAction playerAction;
    private PlayerManager playerManager;

    private MainManager mainManager;

    public Vector2 mouvementInput { get; private set; }
    public Vector2 cameraInput { get; private set; }

    public float cameraInputX { get; private set; }
    public float cameraInputY { get; private set; }

    public float verticalInput { get; private set; }
    public float horizontalInput { get; private set; }

    public bool punchInput { get; private set; }
    public bool blockInput { get; private set; }

    private float moveAmount;

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Mouvement.performed += ctx => 
                mouvementInput = ctx.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += ctx =>
                cameraInput = ctx.ReadValue<Vector2>();


            playerControls.PlayerActions.Punch.performed += ctx =>
                punchInput = true;

            playerControls.PlayerActions.Block.performed += ctx =>
                blockInput = true;
            playerControls.PlayerActions.Block.canceled += ctx =>
                releaseBlock();

        }
        playerControls.Enable();
    }

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerAction = GetComponent<PlayerAction>();
        playerManager = GetComponent<PlayerManager>();
        mainManager = GameObject.FindObjectOfType<MainManager>();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleMouvementInput()
    {
        verticalInput = mouvementInput.y;
        horizontalInput = mouvementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    private void HandleMouvementWhileBlocking()
    {
        verticalInput = 0;
        horizontalInput = 0;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
        
        animatorManager.UpdateAnimatorValues(0, 0);
    }

    public void HandleAllInputs()
    {
        if (mainManager.isPaused || playerManager.isDead || playerManager.isHit)
        {
            //HandleMouvementWhileBlocking();
            punchInput = false;
            blockInput = false;
            cameraInputX = 0;
            cameraInputY = 0;
        }
        else
        {
            if (!playerManager.isDead)
            {
                if (!blockInput)
                {
                    HandleMouvementInput();
                }
                else
                {
                    HandleMouvementWhileBlocking();
                }
        
                HandleActionInput();
            }
            else
            {
                HandleMouvementWhileBlocking();
            }
            

        }
        
    }

    private void releaseBlock()
    {
        playerAction.updateBlocking(false);
        blockInput = false;
    }

    private void HandleActionInput()
    {
        if (blockInput)
        {
            //blockInput = false;
            playerAction.HandleBLocking();
            //Debug.Log("Block button pressed\n");
        }
        else if (punchInput)
        {
            //punchInput = false;
            //Debug.Log("Punch button pressed\n");
            playerAction.HandlePunching();
        }
        punchInput = false;
    }


}
