using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastController : MonoBehaviour
{
  
    //raycast from mouse position

    public Camera cam;
    public GameObject UI;
    public bool isTargeted; 
    private GameObject Hit = null;
  
    void Start()
    {
        UI.SetActive(false);
    }	

    void Update()
    {

            if (Input.GetMouseButtonDown(0) && isTargeted == false)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Interactable")
                    {
                        Hit = hit.collider.gameObject;
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        //change ui text to object name, width height and coordinates
                        UI.SetActive(true);
                        UI.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Name: " + hit.collider.gameObject.name + "\n" + "Width: " + hit.collider.gameObject.transform.localScale.x + "\n" + "Height: " + hit.collider.gameObject.transform.localScale.y + "\n" + "Coordinates: " + hit.collider.gameObject.transform.position;
                        isTargeted = true;
                        
                    }
                }
            
            }
            if (Input.GetMouseButtonDown(1) && Hit != null)
            {
                Hit.GetComponent<Renderer>().material.color = Color.white;
                isTargeted = false;
                UI.SetActive(false);
            }
        }
} 