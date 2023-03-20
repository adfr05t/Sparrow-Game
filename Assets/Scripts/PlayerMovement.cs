using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float rotationSpeed;
    private float yRotation;
    private Rigidbody body;
    private Animator animator;


    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool movingForward = Input.GetKey(KeyCode.Space);
        float horizontalInput = Input.GetAxis("Horizontal");
        RotateSparrow(horizontalInput);
        SetAnimation(movingForward, horizontalInput);

        if (movingForward)
        {
            MoveSparrowForward();
        }
    }

    void RotateSparrow(float horizontalInput)
    {
        yRotation += horizontalInput * rotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, yRotation, 0);
        body.MoveRotation(rotation);
    }

    void MoveSparrowForward()
    {
        body.velocity = transform.forward * forwardSpeed;
    }

    void SetAnimation(bool movingForward, float horizontalInput)
    {
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
