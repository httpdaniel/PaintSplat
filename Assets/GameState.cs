using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public static class GameState 
{
    static int isCreater ;
    static String lobbyCode;
    static String userName;
    public static IDictionary<String, UserObjects> userObjectMaps = new Dictionary<String, UserObjects>();
    public static int isUserCreater() {
        return isCreater;
    }
    public static String getLobbyCode() {
        return lobbyCode;
    }
    public static void setLobbyCode(String lc){
        lobbyCode = lc;
    }
    public static String getUserName() {
        return userName;
    }
    public static void setUserName(String lc){
        userName = lc;
    }
    public static void setIscreater(int createrVal){
        isCreater = createrVal;
    }

    public static void addUserMap(String uname,string uuid){
        UserObjects uObj = new UserObjects(uname,uuid);
        userObjectMaps.Add(uname,uObj);
    }

    public static UserObjects getUobject(String uname){
        return userObjectMaps[uname];
    }

    public static UserObjects getWinner(){
        int index = 0;
        UserObjects uObj = null;
        foreach(var userObj in GameState.userObjectMaps)
        {    
            if (index == 0){
                uObj = userObj.Value;
            }
            else{
                if (uObj.getPoints() < userObj.Value.getPoints()){
                    uObj = userObj.Value;
                }
            }
        }
        return uObj;
    }

    public static void updateaddUserMap(String uname,string uuid){
        // UserObjects uObj = new UserObjects(uname,uuid);
        int dataFound = 0;
        foreach(var userObj in GameState.userObjectMaps)
        {    
            if (userObj.Value.getuuid() == uuid){
                userObj.Value.setUserName(uname);
                dataFound = 1;
            }
        }
        if (dataFound == 0)
        {
            UserObjects uObj = new UserObjects(uname,uuid);
            userObjectMaps.Add(uname,uObj); 
        }

    }

    public static void updateHit(String uuid){
        foreach(var userObj in GameState.userObjectMaps)
        {    
            if (userObj.Value.getuuid() == uuid){
                userObj.Value.updateHitPoints(1);
            }
        }
    }
}