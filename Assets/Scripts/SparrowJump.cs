using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparrowJump : MonoBehaviour
{
    [SerializeField] private float jumpStrength;
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

        grounded = Physics.CheckSphere(groundCheckTransform.position, 0.1f, groundLayer);

        if (jump && grounded)
        {
            PlayerJump();
        }

        UpdateAnimation();
    }

    void PlayerJump()
    {
        body.AddForce(new Vector3(0, 1 * jumpStrength, 0), ForceMode.Impulse);
    }

    void UpdateAnimation()
    {
        if (grounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
    }
}
