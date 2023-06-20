using UnityEngine;
using Player;

namespace Camera
{
    public class TargetFollowMovement : MonoBehaviour
    {
        public Transform target;
        public Transform car;
        public float mouseSensitivity;
        public float rotationPower = 3f;
        public float rotationLerp = 0.5f;
        public GameObject recticle;

        public GameObject driveCamera;
        public GameObject aimCamerara;

        public static bool isAiming;

        //private float xRotationCamera = 0f;
        private Quaternion nextRotation;

        // Start is called before the first frame update
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (SwitchPlayer.isAiming)
                {
                    target.rotation = car.rotation;
                }               
                SwitchPlayer.isAiming = !SwitchPlayer.isAiming;
            }
        }

        // Fixed Update is called once per specific rate
        private void FixedUpdate()
        {
            aimCamerara.SetActive(SwitchPlayer.isAiming);
            driveCamera.SetActive(!SwitchPlayer.isAiming);

            if (SwitchPlayer.isAiming)
            {
                var mouseHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                var mouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                // xRotationCamera -= mouseVertical;
                // xRotationCamera = Mathf.Clamp(xRotationCamera, -90f, 90f);

                // transform.localRotation = Quaternion.Euler(xRotationCamera, -90f, 90f);
                // Target.Rotate(Vector3.up * mouseHorizontal);

                target.transform.rotation *= Quaternion.AngleAxis(mouseHorizontal, Vector3.up);
                target.transform.rotation *= Quaternion.AngleAxis(mouseVertical, Vector3.left);

                var angles = target.transform.localEulerAngles;
                angles.z = 0;

                var angle = target.transform.localEulerAngles.x;

                //Clamp the Up/Down rotation
                if (angle > 180 && angle < 345)
                    angles.x = 345;
                else if (angle < 180 && angle > 40) angles.x = 40;

                target.transform.localEulerAngles = angles;

                nextRotation = Quaternion.Lerp(target.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);
            }
        }
    }
}