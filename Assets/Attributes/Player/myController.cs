using System.Net.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public MeshRenderer frontLeftWheelMesh;
    public MeshRenderer frontRightWheelMesh;
    public MeshRenderer rearLeftWheelMesh;
    public MeshRenderer rearRightWheelMesh;

    public float maxSteeringAngle = 30f;
    public float motorForce = 1050f;
    public float brakeForce = 2000f;
    public float steerDecay = 1f;

    public Rigidbody rigidbody;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle += horizontalInput;
        if (horizontalInput == 0) {
            if (steerAngle > 0) {
                steerAngle -= steerDecay;
            }
            if (steerAngle < 0) {
                steerAngle += steerDecay;
            }
        }
        steerAngle = Math.Clamp(steerAngle, -maxSteeringAngle, maxSteeringAngle);
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = Math.Clamp(verticalInput * motorForce, -200, 1050);
        frontRightWheelCollider.motorTorque = Math.Clamp(verticalInput * motorForce, -200, 1050);

        float brakeTorque = isBreaking ? brakeForce : 0f;
        // Debug.Log(brakeTorque);

        frontLeftWheelCollider.brakeTorque = brakeTorque;
        frontRightWheelCollider.brakeTorque = brakeTorque;
        rearLeftWheelCollider.brakeTorque = brakeTorque;
        rearRightWheelCollider.brakeTorque = brakeTorque;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelMesh);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelMesh);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelMesh);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelMesh);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, MeshRenderer mesh)
    {

        // Get wheel collider state
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);

        // Set wheel transform state
        mesh.transform.rotation = rot;
        mesh.transform.position = pos;
    }

}