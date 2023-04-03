using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparrowMovement : MonoBehaviour
{
  //  [SerializeField] private Camera theCamera;
    [SerializeField] private float moveSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody body;
    private Animator animator;
    [SerializeField] private SparrowJump jumpScript;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);   
        inputVector = Vector2.ClampMagnitude(inputVector, 1); // w/o this, speed would be greater on diagonal 

        Vector3 movementRelativeToCamera = ConvertToCameraSpace(inputVector);

        body.velocity = new Vector3(movementRelativeToCamera.x * moveSpeed, body.velocity.y, movementRelativeToCamera.z * moveSpeed);
        // Orient transform's forward vector with movement direction
        transform.LookAt(transform.position + new Vector3(movementRelativeToCamera.x, 0, movementRelativeToCamera.z));
    }


    Vector3 ConvertToCameraSpace(Vector2 playerInput)
    {
        // Make input vector into a Vector3 (input y will become z)
        Vector3 vectorToRotate = new Vector3(playerInput.x, 0, playerInput.y);

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Ignore y components
        cameraForward.y = 0;
        cameraRight.y = 0;
        
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        // Rotate the x and z vectors using cam directional vectors
        Vector3 cameraForwardZProduct = vectorToRotate.z * cameraForward;
        Vector3 cameraRightXProduct = vectorToRotate.x * cameraRight;

        Vector3 vectorRotatedToCameraSpace = cameraForwardZProduct + cameraRightXProduct;

        return vectorRotatedToCameraSpace;
    }

    void UpdateAnimation()
    {
        if (verticalInput != 0 || horizontalInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
