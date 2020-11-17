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
    public TMP_InputField maxGameLength;
    public TMP_InputField splashSize;
    public GameObject currentScene;
    public GameObject nextScene;

    public void createLobby()
    {
        GetSocket socketObj = SocketFactory.getSocketForApp(SocketConstants.SERVER_HOST, SocketConstants.SERVER_PORT);
        Debug.Log("Creating the lobby");
        List<object> result = socketObj.createLobby(username.text, Int16.Parse(maxplayer.text), Int16.Parse(maxGameLength.text),Int16.Parse(splashSize.text));
        int success = (int)result[0];
        // int success = SocketConstants.SE_ROOM_CODE;
        int acceptCode = SocketConstants.SE_ROOM_CODE;
        Debug.Log(success);
        //bool status = true; // take this variable from the server
        if (success == SocketConstants.SE_ROOM_CODE)
        {
            Debug.Log("Got the success");
            //Debug.Log("User name"+username.text);
            //Debug.Log("max Players"+maxplayer.text);
            //start game screen
            GameState.setIscreater(1);
            GameState.setLobbyCode((string)result[1]);
            GameState.setUserName(username.text);
            GameState.addUserMap(username.text,"testID");
            currentScene.SetActive(false);
            nextScene.SetActive(true);
            Debug.Log("Starting game");
        }
        else{
            Debug.Log("Cant create lobby error");
        }
    }
}
