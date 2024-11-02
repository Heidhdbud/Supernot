    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public float moveSpeed;
    private bool canSprint;

    private float xInput;
    private float zInput;
    private Vector3 direction;
    private Rigidbody rb;

    public Transform orientation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        GetInput();
        Move();
    }

    private void GetInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        direction = orientation.forward * zInput + orientation.right * xInput;
        rb.AddForce(direction.normalized * moveSpeed * 10f, ForceMode.Force);

        Vector3 XZVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (XZVelocity.magnitude > moveSpeed) // if it's more than max speed
        {
            // Clamp the speed to be maximum speed
            XZVelocity = XZVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(XZVelocity.x, rb.velocity.y, XZVelocity.z);
        }
    }
}
