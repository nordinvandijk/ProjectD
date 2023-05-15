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

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    private void OnEnable()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        float progressLoadingBuildings = 0;
        float progressLoadingScene = 0;

        StartCoroutine(FetchBuildings());
        StartCoroutine(WaitFetchBuildings());
    }
}
