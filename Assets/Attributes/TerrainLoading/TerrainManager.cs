using System;
using System.Collections;
using System.Collections.Generic;
using Attributes.Player;
using Attributes.TerrainLoading;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public static class TerrainManager
{
    public static List<Building> buildings;
    public static Texture currentBuildingImage;
    public static BagData bagData;
    private static bool areBuildingsFetched;
    public static bool isBagDataReady;

    public static IEnumerator FetchBuildings()
    {
        using (var request = UnityWebRequest.Get("https://bnxikyhccjojmjjawofa.supabase.co/rest/v1/buildings?select=*"))
        {
            request.SetRequestHeader("apikey",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJueGlreWhjY2pvam1qamF3b2ZhIiwicm9sZSI6ImFub24iLCJpYXQiOjE2ODE3NTk4ODQsImV4cCI6MTk5NzMzNTg4NH0.VCxMTJhWS7G-66kTY6DidyQyItIc8ariyT1hTYRy08k");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError) throw new Exception(request.error);
            areBuildingsFetched = true;
            buildings = JsonConvert.DeserializeObject<List<Building>>(request.downloadHandler.text);
        }
    }

    public static IEnumerator FetchBuildingImage(string url)
    {
        var request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError ||
            request.result == UnityWebRequest.Result.DataProcessingError) throw new Exception(request.error);

        currentBuildingImage = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }

    public static IEnumerator FetchBagData(string bagId)
    {
        using (var request =
               UnityWebRequest.Get($"https://api.bag.kadaster.nl/lvbag/individuelebevragingen/v2/panden/{bagId}"))
        {
            request.SetRequestHeader("X-Api-Key", "l792fa761f4f9f43ef9201f7a78bdc06d5");
            request.SetRequestHeader("Accept-Crs", "epsg:28992");
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError) throw new Exception(request.error);
            bagData = JsonConvert.DeserializeObject<BagData>(request.downloadHandler.text);
            isBagDataReady = true;
        }
    }

    public static IEnumerator WaitFetchBuildings()
    {
        while (!areBuildingsFetched) yield return null;
        // StartGame();
    }
}