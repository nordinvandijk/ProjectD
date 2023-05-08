using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    private void SwitchPlayers() 
    {
        player1.SetActive(!player1.activeSelf);
        player2.SetActive(!player2.activeSelf);
    
    }


    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            SwitchPlayers();
        }
    }
}
