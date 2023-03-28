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
    public float motorForce = 1000f;
    public float brakeForce = 0f;

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
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        float brakeTorque = isBreaking ? brakeForce : 0f;
        Debug.Log(brakeTorque);

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