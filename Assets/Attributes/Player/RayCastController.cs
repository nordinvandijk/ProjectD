 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayCastController : MonoBehaviour
{
  
    //raycast from mouse position

    public Camera cam;
    public GameObject UI;
    public TextMeshProUGUI text;
    private bool isTargeted = false; 
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
                        text.text = "Name: " + Hit.name + "\n" + "Position: " + Hit.transform.position + "\n" + "Height: " + Hit.transform.localScale.y + "\n" + "Width: " + Hit.transform.localScale.x;
                        UI.SetActive(true);
                        isTargeted = true; 
                    }
                }
            
            }
            if (Input.GetMouseButtonDown(1) && Hit != null)
            {
                text.text = "";
                Hit.GetComponent<Renderer>().material.color = Color.white;
                isTargeted = false;
                UI.SetActive(false);
            }
        }
} 