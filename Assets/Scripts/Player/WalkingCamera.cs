using UnityEngine;

namespace Player
{
    public class WalkingCamera : MonoBehaviour
    {
        public float sensX;
        public float sensY;

        public Transform orientation;

        private float xRotation;
        private float yRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            //get mouse input 
            var mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;

            //clamping the x rotation
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //rotate the camera and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}