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

    void Update()
    {
        GetMouseInput();
        RotateCam();
    }

    public void GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.unscaledDeltaTime * senX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.unscaledDeltaTime * senY;
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
