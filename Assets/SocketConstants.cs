using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketConstants
{
    public static string SERVER_HOST      = "3.127.170.185";
    public static int SERVER_PORT         = 10500;
    public static int CL_CREATE_GAME      = 0; //# Sent by the player who creates the game
    public static int CL_JOIN_GAME        = 1; //# Sent by a player who wants to join a game
    public static int SE_ROOM_CODE        = 2; //# Sent by the server to the player who creates the game
    public static int SE_ROOM_NOT_FOUND   = 3; //# Sent by the server to a joining player when the code
                            //# he sent is not found
    public static int CL_SEND_USERNAME    = 4; //# Sent by a player creating or joining a game
    public static int SE_ROOM_OK          = 5; //# Sent by the server when the player could join a game
    public static int SE_ROOM_FULL        = 6; //# Sent by the server when trying to join a full room
    public static int SE_FAIL_ROOM        = 7; //# Sent by the server when failed to create a room
    public static int SE_PLAYER_JOIN      = 8; //# Sent by the server when a player joins to all other 
                            //# players
    public static int SE_PLAYER_SYNC      = 9; //# Sent by the server to the joining player
    public static int SE_START_REQUEST    = 10;//# Sent by the server when starting the game
    public static int CL_PAINT_HIT_REQ    = 11;//# Sent by the client when the target hits the canvas
    public static int SE_PAINT_HIT_OK     = 12;//# Server confirms paintball hit
    public static int SE_PAINT_HIT_FAIL   = 13;//# Server denies paintball hit
    public static int SE_PAINT_BALL_SYNC  = 14;//# Sent by the server when other player hit the canvas

}
