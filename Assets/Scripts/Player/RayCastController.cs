using CesiumForUnity;
using Models;
using Newtonsoft.Json;
using UI;
using UnityEngine;
using static Api.Api;

namespace Player
{
    public class RayCastController : MonoBehaviour
    {
        public UnityEngine.Camera cam;
        public DataPanelManager dataPanelManager;

        private void Start()
        {
            // Fix the cursor to the center of the screen and hide it.
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                if (UnityEngine.Camera.main is not null && Physics.Raycast(
                        UnityEngine.Camera.main.transform.position,
                        UnityEngine.Camera.main.transform.TransformDirection(Vector3.forward),
                        out var hit,
                        Mathf.Infinity))
                {
                    dataPanelManager.SetBasicData("No title available", "", "");
                    dataPanelManager.SetActiveDataPanel(ActiveDataPanel.MetadataPanel);
                    var metadata = hit.transform.GetComponentInParent<CesiumMetadata>();

                    if (metadata is not null)
                    {
                        var features = metadata.GetFeatures(hit.transform, hit.triangleIndex);
                        // List out each metadata property in the UI.
                        foreach (var feature in features)
                        foreach (var propertyName in feature.properties)
                        {
                            var propertyValue = feature.GetString(propertyName, "null");

                            switch (propertyName)
                            {
                                case "attributes":
                                {
                                    var attributesMetaData =
                                        JsonConvert.DeserializeObject<MetaData>(propertyValue);
                                    dataPanelManager.SetMetadata(attributesMetaData);
                                    break;
                                }
                                case "id":
                                {
                                    dataPanelManager.SetBasicData(null, null, propertyValue);

                                    var selectedBuilding =
                                        buildings.Find(b => b.BagId.ToString() == propertyValue.Substring(4));

                                    if (selectedBuilding is not null)
                                    {
                                        StartCoroutine(FetchBuildingImage(selectedBuilding.ImageUrl));
                                        dataPanelManager.SetBasicData(selectedBuilding.Name,
                                            selectedBuilding.Description, propertyValue);
                                    }
                                    else
                                    {
                                        dataPanelManager.SetImages(dataPanelManager.defaultImage);
                                    }

                                    StartCoroutine(FetchBagData(propertyValue.Substring(4)));
                                    break;
                                }
                            }
                        }
                    }
                }

            dataPanelManager.SetActiveDataPanel();

            if (isBagDataReady)
            {
                dataPanelManager.SetBagData(bagData);
                isBagDataReady = false;
            }

            if (isImageReady)
            {
                dataPanelManager.SetImages(currentBuildingImage);
                isImageReady = false;
            }
        }
    }
}