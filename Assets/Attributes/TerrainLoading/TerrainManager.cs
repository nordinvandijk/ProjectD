using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Attributes.Player;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using static ScenesManager;

public static class TerrainManager
{
    public static List<Building> buildings;
    public static bool isLoaded;

    public static IEnumerator FetchBuildings()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://bnxikyhccjojmjjawofa.supabase.co/rest/v1/buildings?select=*"))
        {
            request.SetRequestHeader("apikey",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJueGlreWhjY2pvam1qamF3b2ZhIiwicm9sZSI6ImFub24iLCJpYXQiOjE2ODE3NTk4ODQsImV4cCI6MTk5NzMzNTg4NH0.VCxMTJhWS7G-66kTY6DidyQyItIc8ariyT1hTYRy08k");
            
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                throw new Exception(request.error);
            }
            isLoaded = true;
            buildings = JsonConvert.DeserializeObject<List<Building>>(request.downloadHandler.text); 
            //load image if building.Image_url is not null 
            
            
        }
    }

    public static IEnumerator FetchImages()
    {
        foreach (var building in buildings)
        {
            if (building.Image_url == null)
            {
                continue;
            }
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(building.Image_url))
            {
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    throw new Exception(request.error);
                }
                building.Image = DownloadHandlerTexture.GetContent(request);
                Debug.Log("Image loaded");
            }
        }
    }

    public static IEnumerator WaitFetchBuildings()
    {
        while (!isLoaded)
        {
            yield return null;
        }
        StartGame();
    }
}
