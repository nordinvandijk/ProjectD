using System;
using CesiumForUnity;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Attributes.Player;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Unity.Mathematics;
using static TerrainManager;

public class RayCastController : MonoBehaviour
{
    //raycast from mouse position

    public Camera cam;
    //public GameObject UI;
    //public TextMeshProUGUI text;
    private bool isTargeted = false;
    private GameObject Hit = null;

    // The GameObject with the UI to enable / disable depending on
    // whether metadata has been picked.
    public GameObject metadataPanel;

    // The text to display the metadata properties.
    public Text metadataText;

    void Start()
    {
        //UI.SetActive(false);

        // Fix the cursor to the center of the screen and hide it.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (metadataPanel != null)
        {
            metadataPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTargeted == false)
        {
            metadataText.text = "";
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.TransformDirection(Vector3.forward),
            out hit,
            Mathf.Infinity))
            {
                CesiumGeoreference georeference = hit.transform.GetComponentInParent<CesiumGeoreference>();
                double3 unityPosition = new double3(hit.transform.position);
                double3 ecef = georeference.TransformUnityPositionToEarthCenteredEarthFixed(unityPosition);
                double3 lonLatHeight = CesiumWgs84Ellipsoid.EarthCenteredEarthFixedToLongitudeLatitudeHeight(ecef);
                double longitude = lonLatHeight.x;
                double latitude = lonLatHeight.y;

                var selectedBuilding = buildings.Find(b => Math.Round(b.Lon, 9, MidpointRounding.ToEven)  == Math.Round(longitude, 9, MidpointRounding.ToEven)
                                                           && Math.Round(b.Lat, 9, MidpointRounding.ToEven) == Math.Round(latitude, 9, MidpointRounding.ToEven));

                if (selectedBuilding is not null)
                {
                    metadataText.text += $"Name: {selectedBuilding.Name}\nDescription: {selectedBuilding.Description}\nLongitude: {selectedBuilding.Lon}\nLatitude: {selectedBuilding.Lat}\n\n";
                }

                CesiumMetadata metadata = hit.transform.GetComponentInParent<CesiumMetadata>();
                if (metadata != null)
                {
                    CesiumFeature[] features = metadata.GetFeatures(hit.transform, hit.triangleIndex);
                    // List out each metadata property in the UI.
                    foreach (var feature in features)
                    {
                        foreach (var propertyName in feature.properties)
                        {
                            string propertyValue = feature.GetString(propertyName, "null");
                            if (propertyValue != "null" && propertyValue != "")
                            {
                                metadataText.text += "<b>" + propertyName + "</b>" + ": "
                                                     + propertyValue + "\n";
                            }
                        }
                    }
                }
            }

        }

        if (metadataPanel != null)
        {
            metadataPanel.SetActive(metadataText.text.Length > 0);
        }

        // if (Input.GetMouseButtonDown(1) && Hit != null)
        // {
        //     text.text = "";
        //     Hit.GetComponent<Renderer>().material.color = Color.white;
        //     isTargeted = false;
        //     UI.SetActive(false);
        // }
    }
}