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

        public void PlayGame2()
        {
            SceneManager.LoadScene(2);            
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}