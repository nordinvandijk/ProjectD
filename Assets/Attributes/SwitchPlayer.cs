using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    public GameObject aimCamera;

    public Canvas UI;

    private void SwitchPlayers() 
    {
       if(player1.activeSelf) 
       {
            player1.SetActive(false);
            //spawn player 2 at player 1's position
            player2.transform.position = player1.transform.position;
            player2.SetActive(true);
            UI.enabled = true;
       } 
       else 
       {
           
            player2.SetActive(false);
            //spawn player 1 at player 2's position
            player1.transform.position = player2.transform.position;
            if (!aimCamera.activeSelf) 
            {
                UI.enabled = false;
            }
            player1.SetActive(true);
       }
    
    }


    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            SwitchPlayers();
        }
    }
}
