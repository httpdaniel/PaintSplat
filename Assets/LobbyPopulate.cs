using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LobbyPopulate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject lobbyMemTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        String s;
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
