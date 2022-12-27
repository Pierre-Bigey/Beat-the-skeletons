using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    private InputManager inputManager;

    private Vector3 moveDirection;
    private Transform cameraObject;

    private Rigidbody playerRb;

    [SerializeField] private float movementSpeed = 7;
    [SerializeField] private float rotationSpeed = 15;

   

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMouvement()
    {
        
        HandleMovement();
        
        
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = new Vector3(cameraObject.forward.x, 0f, cameraObject.forward.z) * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        Vector3 movementVelocity = moveDirection * movementSpeed;
        playerRb.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;



        targetDirection =targetDirection + cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        
        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.position - cameraObject.position;
            targetDirection.y = 0;
            targetDirection.Normalize();
        }
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

}
