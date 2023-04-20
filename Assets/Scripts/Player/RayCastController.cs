using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastController : MonoBehaviour
{
  
    //raycast from mouse position

    public Camera cam;
    public GameObject UI;
    

    void Start()
    {
        UI.SetActive(false);
    }	

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Interactable")
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    UI.SetActive(true);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Interactable")
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.white;
                    UI.SetActive(false);
                }
            }
        }
    }   
}
