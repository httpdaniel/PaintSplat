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


}