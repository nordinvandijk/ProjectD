using UnityEngine;

namespace Player
{
    public class MovePlayer : MonoBehaviour
    {
        private Rigidbody rb;

        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update()
        {
            var moveHorizontal = Input.GetAxis("Horizontal") * 10;
            var moveVertical = Input.GetAxis("Vertical") * 10;
            var moveUp = Input.GetAxis("Jump") * 5;


            var vel = rb.velocity;
            vel.x = moveHorizontal;
            vel.z = moveVertical;
            vel.y = moveUp;
            rb.velocity = vel;
        }
    }
}