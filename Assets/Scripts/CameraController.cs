using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float senX;
    public float senY;

    public Transform playerOrientation;

    private float xrotation;
    private float yrotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;   
    }

    void Update()
    {
        GetMouseInput();
        RotateCam();
    }

    public void GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * senY;
        yrotation += mouseX;
        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f); //clamp up down camera rotation
    }

    public void RotateCam()
    {
        transform.rotation = Quaternion.Euler(xrotation, yrotation, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yrotation, 0);
    }
}
