using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    [Header("Ground Check")]
    public float PlayerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform Orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public float weight;
    float Countdown;

    // Start is called before the first frame update
    float adjustedMoveSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void jump()
    {
        if(Input.GetKeyDown("space") && grounded)
        {
            rb.AddForce(Vector3.up * 500f, ForceMode.Force);
        }


    }
    void dash()
    {
        if(Input.GetKeyDown("e") && grounded && (Countdown <= 0))
        {
            rb.AddForce(Orientation.forward * 3000f, ForceMode.Force);
            Countdown += 3f;
        }else if(Countdown > 0)
        {
            Countdown -= 1 * Time.deltaTime;
        }

    }
    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }
    void MovePlayer()
    {
        adjustedMoveSpeed = moveSpeed * (3 * ( 180f / weight));
        // Calculate move direction
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * adjustedMoveSpeed * 1000f * Time.deltaTime, ForceMode.Force);
    }
    // Update is called once per frame



    void Update()
    {
        dash();
        jump();
        grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();
        MovePlayer();

        if(grounded)
        {
            rb.drag = groundDrag + 3 - (( weight / 180 ));
        }
        else
        {
            rb.drag = 3;
        }

    }
}
