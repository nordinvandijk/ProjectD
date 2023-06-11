using UnityEngine;
using UnityEngine.SceneManagement;

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
            //load scene called Game
            SceneManager.LoadScene("Game");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}