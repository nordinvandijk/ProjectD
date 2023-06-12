using UnityEngine;

namespace Scene_Management
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void PlayGame()
        {
            ScenesManager.StartGame();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}