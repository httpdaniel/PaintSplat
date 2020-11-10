using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            g = Instantiate(lobbyMemTemplate,transform);
            // s = i.ToString();
            //Debug.Log(s);
            // g.transform.GetChild(0).GetComponent <TMP_InputField>().text= s;
        }
        Destroy(lobbyMemTemplate);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
