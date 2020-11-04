using System.Collections;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateLobby : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField maxplayer;
    public void createLobby()
    {
        GetSocket socketObj = new GetSocket("127.0.0.1", 10500);
        Debug.Log("Creating the lobby");
        socketObj.createLobby(username.text, Int16.Parse(maxplayer.text));
        bool status = true; // take this variable from the server
        if (status == true)
        {
            Debug.Log("User name"+username.text);
            Debug.Log("max Players"+maxplayer.text);
            //start game screen
            Debug.Log("Starting game");
        }
    }
}
