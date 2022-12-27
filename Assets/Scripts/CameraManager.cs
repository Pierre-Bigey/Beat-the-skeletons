using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private InputManager inputManager;

    private Transform targetTransform; //The object the camera will follow
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float cameraFollowSpeed = 0.2f;
    private float lookAngle; //Camera looking up and down
    private float pivotAngle; //Camera looking left and right

    [SerializeField] float cameraLookSpeed = 2;
    [SerializeField] float cameraPivotSpeed = 2;
    [SerializeField] float minimumPivotAngle = -35;
    [SerializeField] float maximumPivotAngle = 35;

    private Vector3 cameraFollowVelocity = Vector3.zero;

    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;


        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
}
