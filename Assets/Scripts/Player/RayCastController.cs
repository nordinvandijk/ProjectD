using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastController : MonoBehaviour
{
  
    public GameObject UI;

    void Start()
    {
        UI.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.green);
            //Change the color of the object that is hit and clicked
            if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.tag == "Interactable")
            {
                hit.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
                //log the coordinates of the object that is hit and display textbox by the object that is hit and clicked for the user to read the information of the object
                Debug.Log("Hit at " + hit.point);
                UI.SetActive(true);
                UI.transform.position = hit.transform.position;
            }
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1000, Color.white);
            Debug.Log("Did not Hit");
            UI.SetActive(false);
            
        }
    }
 
    //draw the ray in the scene view for debugging purposes backwords
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 1000);
    }
    
}
