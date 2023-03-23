using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    private bool jumping;
    private bool grounded;
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
        bool jump = Input.GetKeyDown(KeyCode.Space);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        grounded = Physics.CheckSphere(groundCheckTransform.position, 0.1f, groundLayer);

        SetAnimation(horizontalInput, verticalInput);

        if (jump && grounded)
        {
            jumping = true;
            Jump();
        }

    }


    void Jump()
    {
        body.velocity = new Vector3(body.velocity.x, 1 * jumpStrength, body.velocity.z);
        StartCoroutine(JumpCooldown());

        // 1.       body.AddForce(new Vector3(0, 1 * jumpStrength, 0), ForceMode.Impulse);
    }

    void MoveSparrow()
    {
        // body.velocity = (transform.forward * forwardSpeed);
        //     body.AddForce(transform.forward * forwardSpeed, ForceMode.Acceleration);
  //      body.MovePosition(transform.position + transform.forward * Time.deltaTime * forwardSpeed); // should be in Fixed. Still messes with jump

        //if (body.velocity.x > maxSpeed)
        //{
        //    body.velocity = new Vector3(maxSpeed, body.velocity.y, body.velocity.z);
        //}
        //else if (body.velocity.z > maxSpeed)
        //{
        //    body.velocity = new Vector3(body.velocity.x, body.velocity.y, maxSpeed);
        //}



    }

    void SetAnimation(float verticalInput, float horizontalInput)
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

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(1f);
        jumping = false;
    }
}
