using UnityEngine;

namespace UI
{
    public class Speedometer : MonoBehaviour
    {
        public Rigidbody rb;

        public float maxSpeed;

        public float minSpeedArrowAngle;
        public float maxSpeedArrowAngle;

        [Header("UI")]
        // public Text speedLabel; // The label that displays the speed;
        public RectTransform arrow; // The arrow in the speedometer

        private float speed;

        private void Update()
        {
            // 3.6f to convert in kilometers
            // ** The speed must be clamped by the car controller **
            speed = rb.velocity.magnitude * 3.6f;

            // if (speedLabel != null)
            //     speedLabel.text = ((int)speed) + " km/h";
            if (arrow != null)
                arrow.localEulerAngles =
                    new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
        }
    }
}