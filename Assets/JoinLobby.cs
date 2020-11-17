using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class JoinLobby : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField lobbyCode;
    public GameObject currentScene;
    public GameObject nextScene;
    public void join()
    {
        GetSocket socketObj = SocketFactory.getSocketForApp(SocketConstants.SERVER_HOST, SocketConstants.SERVER_PORT);
        List<object> result = socketObj.sendLobbyCode(lobbyCode.text, username.text);
        Debug.Log("Received the data from server for creating the lobby!");
        List<int> lobbyInfo = (List<int>) result[0];
        int response = (int)lobbyInfo[0];
        Debug.Log(response);
        int index =0;
        foreach(var p in result) {
            if (index >=1){
                List<object> listUserData = (List<object>) p;
                //Debug.Log((string)listUserData[1]);
                string uuid = (string)listUserData[0];
                string uname = (string)listUserData[1];
                GameState.addUserMap(uname,uuid);
               }
               index+=1;
        }
        if (response == SocketConstants.SE_ROOM_OK){
            Debug.Log("Player Joined");
            GameState.setIscreater(0);
            GameState.setLobbyCode(lobbyCode.text);
            GameState.setUserName(username.text);
            currentScene.SetActive(false);
            nextScene.SetActive(true);
        }
        else{
            Debug.Log("Can not join!");
        }
        
    }
}
