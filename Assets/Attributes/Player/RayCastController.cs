using System;
using CesiumForUnity;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static TerrainManager;

public class RayCastController : MonoBehaviour
{
    public enum ActiveDataPanel
    {
        None,
        MetadataPanel,
        ExtraDataPanel
    }

    public Camera cam;
    public GameObject dataScreen;
    public GameObject bigDataScreen;

    // The text to display the metadata properties.
    public Text title;
    public Text description;
    public Text titleExtraDetails;
    public Text descriptionExtraDetails;
    public Text bagData;
    public RawImage imageDataScreen;
    public RawImage imageBDS;
    public Texture defaultImage;
    private readonly bool isTargeted = false;

    private ActiveDataPanel activeDataPanel;
    private bool buildingIsSelected;
    private GameObject Hit = null;

    private void Start()
    {
        // Fix the cursor to the center of the screen and hide it.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (dataScreen != null) dataScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTargeted == false)
        {
            activeDataPanel = ActiveDataPanel.MetadataPanel;
            title.text = "No title available";
            description.text = "";
            titleExtraDetails.text = "No title available";
            descriptionExtraDetails.text = "";
            bagData.text = "";
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(
                    Camera.main.transform.position,
                    Camera.main.transform.TransformDirection(Vector3.forward),
                    out hit,
                    Mathf.Infinity))
            {
                var georeference = hit.transform.GetComponentInParent<CesiumGeoreference>();
                var unityPosition = new double3(hit.transform.position);
                var ecef = georeference.TransformUnityPositionToEarthCenteredEarthFixed(unityPosition);
                var lonLatHeight = CesiumWgs84Ellipsoid.EarthCenteredEarthFixedToLongitudeLatitudeHeight(ecef);
                var longitude = lonLatHeight.x;
                var latitude = lonLatHeight.y;

                var selectedBuilding = buildings.Find(b =>
                    Math.Round(b.Lon, 9, MidpointRounding.ToEven) == Math.Round(longitude, 9, MidpointRounding.ToEven)
                    && Math.Round(b.Lat, 9, MidpointRounding.ToEven) ==
                    Math.Round(latitude, 9, MidpointRounding.ToEven));

                if (selectedBuilding is not null)
                {
                    buildingIsSelected = true;
                    StartCoroutine(FetchBuildingImage(selectedBuilding.ImageUrl));
                    title.text = selectedBuilding.Name;
                    titleExtraDetails.text = selectedBuilding.Name;

                    description.text +=
                        $"Description: {selectedBuilding.Description}";
                    descriptionExtraDetails.text +=
                        $"Description: {selectedBuilding.Description}";
                }
                else
                {
                    buildingIsSelected = false;
                }

                var metadata = hit.transform.GetComponentInParent<CesiumMetadata>();
                if (metadata != null)
                {
                    var features = metadata.GetFeatures(hit.transform, hit.triangleIndex);
                    // List out each metadata property in the UI.
                    foreach (var feature in features)
                    foreach (var propertyName in feature.properties)
                    {
                        var propertyValue = feature.GetString(propertyName, "null");
                        if (propertyValue != "null" && propertyValue != "")
                            bagData.text += "<b>" + propertyName + "</b>" + ": "
                                            + propertyValue + "\n";
                        if (propertyName == "id")
                        {
                            description.text += $"\nid: {propertyValue}";
                            descriptionExtraDetails.text += $"\nid: {propertyValue}";
                        }
                    }
                }
            }
        }

        if (activeDataPanel == ActiveDataPanel.MetadataPanel)
        {
            dataScreen.SetActive(true);
            bigDataScreen.SetActive(false);
        }

        else if (activeDataPanel == ActiveDataPanel.ExtraDataPanel)
        {
            dataScreen.SetActive(false);
            bigDataScreen.SetActive(true);
        }

        else
        {
            dataScreen.SetActive(false);
            bigDataScreen.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.I)) activeDataPanel = ActiveDataPanel.ExtraDataPanel;

        if (Input.GetKeyDown(KeyCode.C)) activeDataPanel = ActiveDataPanel.None;

        if (buildingIsSelected)
        {
            imageDataScreen.texture = currentBuildingImage;
            imageBDS.texture = currentBuildingImage;
        }
        else
        {
            imageDataScreen.texture = defaultImage;
            imageBDS.texture = defaultImage;
        }
    }
}