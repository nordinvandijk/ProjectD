using System;
using System.Collections;
using System.Collections.Generic;
using Attributes.Player;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public static class TerrainManager
{
    public static List<Building> buildings;
    public static Texture currentBuildingImage;
    private static bool isLoaded;

    public static IEnumerator FetchBuildings()
    {
        using (var request = UnityWebRequest.Get("https://bnxikyhccjojmjjawofa.supabase.co/rest/v1/buildings?select=*"))
        {
            request.SetRequestHeader("apikey",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJueGlreWhjY2pvam1qamF3b2ZhIiwicm9sZSI6ImFub24iLCJpYXQiOjE2ODE3NTk4ODQsImV4cCI6MTk5NzMzNTg4NH0.VCxMTJhWS7G-66kTY6DidyQyItIc8ariyT1hTYRy08k");

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError) throw new Exception(request.error);
            isLoaded = true;
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

    private static Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f),
            100.0f);
    }

    public static IEnumerator WaitFetchBuildings()
    {
        while (!isLoaded) yield return null;
        // StartGame();
    }
}