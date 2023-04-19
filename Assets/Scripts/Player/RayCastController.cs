using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastController : MonoBehaviour
{
  
    //Unity script to draw a raycast in the scene view
    //Attach this script to a gameobject in the scene
    //Press play and move the mouse around to see the raycast
    //if raycast hits something, it will turn that object red


    // Update is called once per frame
    void Update()
    {
        //create a raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //draw the raycast in the scene view
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        //if raycast hits something
        if (Physics.Raycast(ray, out hit, 100))
        {
            //turn the object red
            hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
