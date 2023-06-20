using System.Collections;
using UnityEngine;
using static Api.Api;


namespace Scene_Management
{
    public class LoadingScript : MonoBehaviour
    {
        public Canvas loadingCanvas;
        public static bool isLoaded;

        private void Start()
        {
            isLoaded = false;
            loadingCanvas.enabled = true;
            StartCoroutine(FetchBuildings());
            StartCoroutine(Wait());
        }

        //wait loading time for buildings
        private IEnumerator Wait()
        {   
            yield return new WaitForSeconds(10);
            isLoaded = true;
            loadingCanvas.enabled = false;
        }
    }
}