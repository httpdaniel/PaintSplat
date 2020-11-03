using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLobby : MonoBehaviour
{
    public void createLobby()
    {
        Debug.Log("Creating the lobby");
        bool status = true; // take this variable from the server
        if (status == true)
        {
            //start game screen
            Debug.Log("Starting game");
        }
    }
}
