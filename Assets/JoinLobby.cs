using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class JoinLobby : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField lobbyCode;

    public void join()
    {
        GetSocket socketObj = SocketFactory.getSocketForApp(SocketConstants.SERVER_HOST, SocketConstants.SERVER_PORT);
        List<object> result = socketObj.sendLobbyCode(lobbyCode.text, username.text);
        int response = (int)result[0];
        if (response == SocketConstants.SE_PLAYER_JOIN){
            Debug.Log("Player Joined");
        }
        else{
            Debug.Log("Can not join!");
        }
        
    }
}
