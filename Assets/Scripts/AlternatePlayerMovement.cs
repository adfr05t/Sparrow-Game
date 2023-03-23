using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatePlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpStrength;
    private float yRotation;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody body;
    private Animator animator;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool jump = Input.GetKeyDown(KeyCode.M);
        bool movingForward = Input.GetKey(KeyCode.Space);
        float horizontalInput = Input.GetAxis("Horizontal");
        
        bool grounded = Physics.CheckSphere(groundCheckTransform.position, 0.1f, groundLayer);

        RotateSparrow(horizontalInput);
        UpdateAnimation(grounded, movingForward, horizontalInput);

        if (jump && grounded)
        {
            Jump();
        }
        if (movingForward)
        {
            MoveSparrowForward();
        }
    }

    void RotateSparrow(float horizontalInput)
    {
        // Update yRotation according to input
        yRotation += horizontalInput * rotationSpeed * Time.deltaTime;
        // Convert rotation values to a quaternion to use with rb.MoveRotation()
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        body.MoveRotation(rotation);
    }

    void Jump()
    {
        body.AddForce(new Vector3(0, 1 * jumpStrength, 0), ForceMode.Impulse);
    }

    void MoveSparrowForward()
    {
        // Strictly, should prob be in FixedUpdate, since setting rb.velocity
        body.velocity = new Vector3(transform.forward.x * forwardSpeed, body.velocity.y, transform.forward.z * forwardSpeed); 
    }

    void UpdateAnimation(bool grounded, bool movingForward, float horizontalInput)
    {
        if (grounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        if (movingForward || horizontalInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
