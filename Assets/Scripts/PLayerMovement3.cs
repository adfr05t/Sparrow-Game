using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerMovement3 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody body;
    private Animator animator;
    [SerializeField] private Jump jumpScript;

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
        body.velocity = new Vector3(inputVector.x * moveSpeed, body.velocity.y, inputVector.y * moveSpeed);
        // Orient transform's forward vector with movement direction
        transform.LookAt(transform.position + new Vector3(horizontalInput, 0, verticalInput));
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
