using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class CarController : MonoBehaviour
    {
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

        private float horizontalInput;
        private float verticalInput;

        public Rigidbody rigidbody;

        private bool isBreaking;
        private float speed = 0f;
        private float steerAngle;

        private AudioSource _audioSource;

        public float pitchFromCar = 0f;

        public AudioClip skidClip;
        private float skidThreshhold = 0f;


        private void Awake() 
        {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null) 
            {
                Debug.LogError("Missing Audio Source");
            }
        }

        private void FixedUpdate()
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }

        private void Update()
        {
           EngineSound(speed);
        }

        private void GetInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            speed = rigidbody.velocity.magnitude * 3.6f;
            isBreaking = Input.GetKey(KeyCode.Space) || speed >= 5 && verticalInput < 0;
        }

        private void HandleSteering()
        {
            steerAngle += horizontalInput;
            if (horizontalInput == 0)
            {
                if (steerAngle > 0) steerAngle -= steerDecay;
                if (steerAngle < 0) steerAngle += steerDecay;
            }

            steerAngle = Math.Clamp(steerAngle, -maxSteeringAngle, maxSteeringAngle);
            frontLeftWheelCollider.steerAngle = steerAngle;
            frontRightWheelCollider.steerAngle = steerAngle;
        }

        private void HandleMotor()
        {
            frontLeftWheelCollider.motorTorque = Math.Clamp(verticalInput * motorForce, -200, 1050);
            frontRightWheelCollider.motorTorque = Math.Clamp(verticalInput * motorForce, -200, 1050);

            var brakeTorque = isBreaking ? brakeForce : 0f;
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

        private void EngineSound(float speed) 
        {
            pitchFromCar = speed / 50f;
            if (speed < 0f) {
                _audioSource.pitch = 0.2f;
            }

            if (speed > 0f && speed < 260) 
            {
                _audioSource.pitch = 0.2f + pitchFromCar;
            }

            if (speed > 260) {
                _audioSource.pitch = 0;
            }
        }
    }
}