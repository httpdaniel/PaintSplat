using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LobbyPopulate : MonoBehaviour
{
    public TMP_Text lobbyCode;
    public String lobbyCodeStr = "lobbyCodeScript";
    // Start is called before the first frame update
    void Start()
    {
        GameObject lobbyMemTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        String s;
        
        lobbyCode.text = lobbyCodeStr;
        for (int i =0; i<5;i++){
            g = Instantiate(lobbyMemTemplate,transform) as GameObject;
            s = i.ToString();
            // Debug.Log();
            g.transform.Find("Text").GetComponent <TMP_Text>().text= s;
        }
        Destroy(lobbyMemTemplate);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
