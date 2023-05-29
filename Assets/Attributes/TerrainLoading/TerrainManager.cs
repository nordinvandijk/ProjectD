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
    private static bool isLoaded;

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
        }
    }

    public static IEnumerator WaitFetchBuildings()
    {
        while (!isLoaded)
        {
            yield return null;
        }
        // StartGame();
    }
}
