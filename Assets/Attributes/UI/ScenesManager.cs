using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesManager
{
    public enum SceneEnum
    {
        MainMenu,
        Game
    }

    public static void LoadScene(SceneEnum scene)
    {
        SceneManager.LoadScene((int)scene);
    }

    public static void StartGame()
    {
        SceneManager.LoadScene((int)SceneEnum.Game);
    }
}
