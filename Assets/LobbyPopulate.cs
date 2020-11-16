using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LobbyPopulate : MonoBehaviour
{
    public TMP_Text lobbyCode;
    public Button startButton;
    public String lobbyCodeStr = "lobbyCodeScript";
    GetSocket socketObj;
    // Start is called before the first frame update
    void Start()
    {
        socketObj = SocketFactory.getSocketForApp(SocketConstants.SERVER_HOST, SocketConstants.SERVER_PORT);
        lobbyCode.text = GameState.getLobbyCode();
        if (GameState.isUserCreater() == 1){
            Debug.Log("Show start");
        }
        else{
            startButton.gameObject.SetActive(false);
            Debug.Log("Dont show start");
        }
        // hack to write the username
        GameObject lobbyMemTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        for (int i =0; i<1;i++){
            g = Instantiate(lobbyMemTemplate,transform) as GameObject;
            // s = i.ToString();
            // Debug.Log();
            g.transform.Find("Text").GetComponent <TMP_Text>().text= GameState.getUserName();
        }
        Destroy(lobbyMemTemplate);
    }
    void onclickStart(){
        socket.startPainting();
    }
    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
