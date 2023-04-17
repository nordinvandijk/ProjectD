using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollowMovement : MonoBehaviour
{
    public Transform target;
    public Transform car;
    public float mouseSensitivity;
    private float xRotationCamera = 0f;
    private Quaternion nextRotation;
    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public GameObject driveCamera;
    public GameObject aimCamerara;
    private bool isAiming = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isAiming)
            {
                target.rotation = car.rotation;
            }
            isAiming = !isAiming;
        }

        aimCamerara.SetActive(isAiming);
        driveCamera.SetActive(!isAiming);


        if (isAiming)
        {
            float mouseHorizontal = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // xRotationCamera -= mouseVertical;
            // xRotationCamera = Mathf.Clamp(xRotationCamera, -90f, 90f);

            //transform.localRotation = Quaternion.Euler(xRotationCamera, -90f, 90f);
            //Target.Rotate(Vector3.up * mouseHorizontal);

            target.transform.rotation *= Quaternion.AngleAxis(mouseHorizontal, Vector3.up);
            target.transform.rotation *= Quaternion.AngleAxis(mouseVertical, Vector3.right);

            var angles = target.transform.localEulerAngles;
            angles.z = 0;

            var angle = target.transform.localEulerAngles.x;

            //Clamp the Up/Down rotation
            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if (angle < 180 && angle > 40)
            {
                angles.x = 40;
            }


            target.transform.localEulerAngles = angles;

            nextRotation = Quaternion.Lerp(target.transform.rotation, nextRotation, Time.deltaTime * rotationLerp);
        }
    }
}
