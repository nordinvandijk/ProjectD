using System.Collections;
using System.Collections.Generic;
using Scene_Management;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject gameMenuUI;
    public GameObject rootButtonsUI;
    public GameObject controlsUI;

    public List<GameObject> objectsToHide;


    void Start() {
        gameMenuUI.SetActive(false);
        controlsUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && LoadingScript.isLoaded) {
            if (GameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        gameMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        toggleObjectToHide(true);

    }

    void Pause() {
        gameMenuUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        toggleObjectToHide(false);
    }

    public void QuitToDesktop() {
        Application.Quit();
    }

    public void QuitToMenu() {
        GameIsPaused = false;
        Time.timeScale = 1f;
        ScenesManager.LoadScene(0);
    }

    public void OpenControls() {
        controlsUI.SetActive(true);
        rootButtonsUI.SetActive(false);
    }

    public void BackToRootButtons() {
        controlsUI.SetActive(false);
        rootButtonsUI.SetActive(true);
    }

    void toggleObjectToHide(bool active) {
        foreach (var item in objectsToHide)
        {
            item.SetActive(active);
        }
    }
}
