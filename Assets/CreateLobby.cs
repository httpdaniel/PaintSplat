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
        GetSocket socketObj = SocketFactory.getSocketForApp("127.0.0.1", 10500);
        //GetSocket socketObj = new GetSocket("127.0.0.1", 10500);
        Debug.Log("Creating the lobby");
        List<object> result = socketObj.createLobbySimple(username.text, Int16.Parse(maxplayer.text));
        int success = (int)result[0];
        int acceptCode = SocketConstants.SE_ROOM_CODE;
        Debug.Log(acceptCode);
        //bool status = true; // take this variable from the server
        if (success == SocketConstants.SE_ROOM_CODE)
        {
            Debug.Log("User name"+username.text);
            Debug.Log("max Players"+maxplayer.text);
            //start game screen
            Debug.Log("Starting game");
        }
        else{
            Debug.Log("Cant create lobby error");
        }
    }
}
