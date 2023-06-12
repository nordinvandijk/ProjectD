using System.Collections;
using UnityEngine;
using static Api.Api;


namespace Scene_Management
{
    public class LoadingScript : MonoBehaviour
    {
        public Canvas loadingCanvas;

        private void Start()
        {
            loadingCanvas.enabled = true;
            StartCoroutine(FetchBuildings());
            StartCoroutine(Wait());
        }

        //wait loading time for buildings
        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(10);
            loadingCanvas.enabled = false;
        }
    }
}