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



public class LoadingScene : MonoBehaviour
{
    //wait for given amount of seconds to load the next scene
    //show progress for the seconds to the user
    //the progress bar should be filled in the given amount of seconds and not before that 


    public TextMeshProUGUI progressText;
    public Slider slider;

    public int waitForSeconds;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitForSeconds);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
    }
  

}

