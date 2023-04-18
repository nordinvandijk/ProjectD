using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * 10;
        float moveVertical = Input.GetAxis("Vertical") * 10;
        float moveUp = Input.GetAxis("Jump") * 5;

        

        Vector3 vel = rb.velocity;
        vel.x = moveHorizontal;
        vel.z = moveVertical;
        vel.y = moveUp;
        rb.velocity = vel;

    }
}