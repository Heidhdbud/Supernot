    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Movement")]
    public float moveSpeed;
    private float xInput;
    private float zInput;
    private Vector3 direction;
    private Rigidbody rb;
    [SerializeField] private Transform orientation;

    [Header("SlowTime")]
    [SerializeField] private float slowSpeedMultiplier;
    [SerializeField] private float slowCooldown;
    private float slowCountCooldown;
    [SerializeField] private float timeInSlow;
    [SerializeField] private float slowScale = 0.2f;
    private bool isSlow;

    [Header("Dashing")]
    public float dashPower;
    [SerializeField] private float dashCooldown;
    private float dashCountCooldown;
    private bool dashing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        dashCountCooldown += Time.deltaTime;
        slowCountCooldown += Time.deltaTime;
        
        if (dashCountCooldown >= dashCooldown / 4) // stop dashing 1/4th of the way into the cooldown
        {
            dashing = false;
        }

        Dash();
        if (Input.GetKeyDown(KeyCode.Space) && !isSlow && slowCountCooldown >= slowCooldown)
        {
            StartCoroutine(SlowTime());
        }
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
        rb.AddForce(Vector3.down * 9.8f, ForceMode.Force);

        Vector3 XZVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (XZVelocity.magnitude > moveSpeed) // if it's more than max speed
        {
            // Clamp the speed to be maximum speed
            XZVelocity = dashing ? XZVelocity.normalized * (dashPower + moveSpeed) : XZVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(XZVelocity.x, rb.velocity.y, XZVelocity.z);
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCountCooldown >= dashCooldown)
        {
            dashCountCooldown = 0f;
            dashing = true;
            if (direction != Vector3.zero)
            {
                rb.AddForce(direction.normalized * dashPower * 10f, ForceMode.Impulse);
            }
            else // if player isnt holding direction, dash forward
            {
                rb.AddForce(orientation.forward * dashPower * 10f, ForceMode.Impulse);
            }
        }
    }

    private IEnumerator SlowTime()
    {
        isSlow = true;
        Debug.Log("Start slow");
        Time.timeScale = slowScale;
        moveSpeed *= slowSpeedMultiplier;
        
        yield return new WaitForSecondsRealtime(timeInSlow);
        
        Time.timeScale = 1f;
        isSlow = false;
        Debug.Log("Stop slow");   
        slowCountCooldown = 0f;
        moveSpeed /= slowSpeedMultiplier;
    }
}
