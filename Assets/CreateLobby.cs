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
        // GetSocket socketObj = SocketFactory.getSocketForApp(SocketConstants.SERVER_HOST, SocketConstants.SERVER_PORT);
        //GetSocket socketObj = new GetSocket("127.0.0.1", 10500);
        Debug.Log("Creating the lobby");
        // List<object> result = socketObj.createLobby(username.text, Int16.Parse(maxplayer.text));
        // int success = (int)result[0];
        int success = SocketConstants.SE_ROOM_CODE;
        int acceptCode = SocketConstants.SE_ROOM_CODE;
        Debug.Log(acceptCode);
        //bool status = true; // take this variable from the server
        if (success == SocketConstants.SE_ROOM_CODE)
        {
            Debug.Log("User name"+username.text);
            Debug.Log("max Players"+maxplayer.text);
            //start game screen
            SceneLoader.loadIt("playerQueue");
            Debug.Log("Starting game");
        }
        else{
            Debug.Log("Cant create lobby error");
        }
    }
}
