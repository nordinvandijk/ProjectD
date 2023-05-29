using System;
using System.Collections;
using System.Collections.Generic;
using CesiumForUnity;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static ScenesManager;
using static  TerrainManager;
using TMPro;
using TextMeshProGUI = TMPro.TextMeshProUGUI;



public class LoadingScript : MonoBehaviour
{
    public Canvas loadingCanvas;
    public int loadingTime; 
  

    private void Start()
    {
        loadingCanvas.enabled = true;
        StartCoroutine(WaitFetchBuildings());

    }

    //wait loading time for buildings
    IEnumerator WaitFetchBuildings()
    {
        yield return new WaitForSeconds(loadingTime);
        loadingCanvas.enabled = false;
    }

}


