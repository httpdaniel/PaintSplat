using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Threading;
public class LobbyPopulate : MonoBehaviour
{
    public TMP_Text lobbyCode;
    public Button startButton;
    public String lobbyCodeStr = "lobbyCodeScript";
    private float update = 0.0f;
    GetSocket socketObj;
    GameObject lobbyMemTemplate;
    GameObject g;
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
        lobbyMemTemplate = transform.GetChild(0).gameObject;
        
        startButton.onClick.AddListener(onclickStart);
        foreach(var userObj in GameState.userObjectMaps)
        {
            // Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
            if (userObj.Value.getShowing()==0)
            {
                g = Instantiate(lobbyMemTemplate,transform) as GameObject;
                g.transform.Find("Text").GetComponent <TMP_Text>().text= userObj.Value.getname();
                userObj.Value.setShowing();
            }    
        }
        // Destroy(lobbyMemTemplate);
    }
    public void onclickStart(){
        socketObj.startPainting();
    }
    // Update is called once per frame
    void Update()
    {   update += Time.deltaTime;
        if (update > 1.0f)
        {
            update = 0.0f;

            List<object> result = socketObj.isThereNewUser();
            int recievedPacket = (int)result[0];
            Debug.Log("got the result!!");
            if (recievedPacket == SocketConstants.SE_PLAYER_JOIN)
            {
                //new player is HERE !!!!
                List<object> listUserData = (List<object>) result[1];
                //Debug.Log((string)listUserData[1]);
                string uuid = (string)listUserData[0];
                string uname = "testname";
                Debug.Log(uname);
                GameState.addUserMap(uname,uuid);   
            }
            if (recievedPacket == SocketConstants.SE_PLAYER_NAME_UPD)
            {
                //new player is HERE !!!!
                List<object> listUserData = (List<object>) result[1];
                //Debug.Log((string)listUserData[1]);
                string uuid = (string)listUserData[0];
                string uname = (string)listUserData[1];
                Debug.Log(uname);
                GameState.updateaddUserMap(uname,uuid);
                // userObjectMaps
                // GameObject lobbyMemTemplate = transform.GetChild(0).gameObject;
                // GameObject g;
                foreach(var userObj in GameState.userObjectMaps)
                {
                    if (userObj.Value.getShowing()==0)
                    {
                        g = Instantiate(lobbyMemTemplate,transform) as GameObject;
                        g.transform.Find("Text").GetComponent <TMP_Text>().text= userObj.Value.getname();
                        userObj.Value.setShowing();
                    }   
                }
                // Destroy(lobbyMemTemplate);   
            }
            if (recievedPacket == SocketConstants.SE_GAME_START){
                Thread.Sleep(2000);
                SceneManager.LoadScene("SampleScene");
            }
        }
        
        
    }
}
