using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float gravity;

    private float horizontalInput;
    private float verticalInput;
    private CharacterController controller;
    private Vector3 movementVector;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        bool jump = Input.GetKeyDown(KeyCode.Space);

        MovePlayer(jump);
        //AddGravity();
    }

    void MovePlayer(bool jump)
    {
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1); // w/o this, speed would be greater on diagonal 

        Vector3 inputDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.TransformDirection(inputDirection);

        if (jump)
        {
            movementVector.y = jumpStrength;
        }
        else
        {
            //Add gravity
            movementVector.y -= gravity;
        }

        movementVector = new Vector3(inputVector.x, movementVector.y, inputVector.y);
        
        controller.Move(movementVector * Time.deltaTime);

    }

    //void AddGravity()
    //{
    //    float gravity = Physics.gravity * gravityScale;
    //    controller.Move(movementVector * Time.deltaTime);
    //}
}
