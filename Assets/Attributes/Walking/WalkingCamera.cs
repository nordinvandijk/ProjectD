using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        //get mouse input 
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        //clamping the x rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        //rotate the camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);

    }
}
