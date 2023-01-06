using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;

    public float speed = 5f;
    public float jumpForce = 10f;
    private float gravity = 20f;
    private float vericalVelocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }
    void Move()
    {
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, 
            Input.GetAxis(Axis.VERTICAL));

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;

        ApplyGravity();

        characterController.Move(moveDirection);
    }

    void ApplyGravity()
    {
        vericalVelocity -= gravity * Time.deltaTime;
        DoJump();
        moveDirection.y = vericalVelocity * Time.deltaTime;
    }
    void DoJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vericalVelocity = jumpForce;
        }
    }
}
