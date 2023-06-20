using UnityEngine;

namespace Player
{
    public class SwitchPlayer : MonoBehaviour
    {
        public GameObject player1;
        public GameObject player2;

        public GameObject aimCamera;

        public static bool isAiming;
        public GameObject recticle;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) { SwitchPlayers(); }

            if (player2.activeSelf) { recticle.SetActive(true); }

            if (player1.activeSelf ) 
            { 
                if (isAiming) 
                { 
                    recticle.SetActive(true); 
                }
                else
                {
                   recticle.SetActive(false);
                }
            }
        
        }

        private void SwitchPlayers()
        {
            if (player1.activeSelf)
            {
                player1.SetActive(false);
                //spawn player 2 at player 1's position
                player2.transform.position = player1.transform.position;
                player2.SetActive(true);
            }
            else
            {
                player2.SetActive(false);
                //spawn player 1 at player 2's position
                player1.transform.position = player2.transform.position;
                player1.SetActive(true);
            }
        }
    }
}