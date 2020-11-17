using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections;
using UnityEngine;
using System.Threading;
public class GetSocket
{

    private string serverHostName;
    private int serverPort;
    private Socket socket;
    public GetSocket(string hostName, int port)
    {
        serverHostName = hostName;
        serverPort = port;
        socket = connectSocket();
        socket.ReceiveTimeout = 1000;
    }
    // method to connect the socket to the host name and the port
    private Socket connectSocket()
    {
        IPAddress host = IPAddress.Parse(serverHostName);//  
        IPEndPoint ipendpoint = new IPEndPoint(host, serverPort); // assign host and port                                                               
        Socket tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            tempSocket.Connect(ipendpoint);
        }
        catch (SocketException e)
        {
            //MessageBox.Show(e.Message);
            tempSocket.Close();
        }
        return tempSocket;
    }
    // helper function to change int to byte array
    private byte[] changeIntToByteArray(int value) {
        byte[] valueByte = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(valueByte);
        byte[] valueByteRes = valueByte;
        
        return valueByteRes;
    }
    //helper function to change byte array to float
    public float changeByteToFloat(byte[] bytes)
    {
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes); // Convert big endian to little endian
        }
        float myFloat = BitConverter.ToSingle(bytes, 0);
        return myFloat;
    }

    public List<object> createLobby(string userName, int numberOfPlayers, int maxGameLength, int splashSize) {
        // function is used to initialise the handshake with the server and then create a lobby
        //Console.WriteLine("Creating the lobby !");
        int typeOfPacketInt = SocketConstants.CL_CREATE_GAME;
        byte pNum = (byte) numberOfPlayers;
        byte typeOfPacket = (byte) typeOfPacketInt;
        byte maxGameLengthB = (byte) maxGameLength;
        byte splashSizeB = (byte) splashSize;

        int LengthOfArray = 4; 
        
        var bytesDataTosend = new byte[LengthOfArray];
        
        bytesDataTosend[0] = typeOfPacket;
        bytesDataTosend[1] = pNum;
        bytesDataTosend[2] = maxGameLengthB;
        bytesDataTosend[3] = splashSizeB;
        List<object> result = sendLobbyData(bytesDataTosend);
        int success = (int)result[0];
        if (success == SocketConstants.SE_ROOM_CODE){
            System.Diagnostics.Debug.WriteLine(result[1]);
            sendUserName(userName);
        }

        //bytesDataTosend[-1] = numberOfPlayers;
        //Console.WriteLine("Sending the data to server for creating the lobby !");
        
        // Console.WriteLine("Received the data from server for creating the lobby!");
        return result;
    }

    public void sendUserName(String userName){
        int lengthUName = userName.Length;            
        int dataTypeToSend  = SocketConstants.CL_SEND_USERNAME;// converting lobby to bytes
        
        Byte[] bytesUname   = Encoding.ASCII.GetBytes(userName);
        byte typeOfPacket = (byte) dataTypeToSend;
        byte bytesULen = (byte)lengthUName;

        int LengthOfArray = bytesUname.Length  + 2;
        var bytesDataTosend = new byte[LengthOfArray];
        bytesDataTosend[0] = typeOfPacket;
        bytesDataTosend[1] = bytesULen;
        int index = 2;
        for (int i = 0; i < bytesUname.Length; i++)
        {
            bytesDataTosend[index] = bytesUname[i];
            index += 1;
        }
        sendUserNameLobbyCreator(bytesDataTosend);
        // return resultAddUserToLobby;    
    }

    public List<object> sendLobbyCode(string lobbycode,string userName)
    {
        // function is used to initialise the handshake with the server and then join a lobby
        Console.WriteLine("Joining the lobby !");
        int dataTypeToSend  = SocketConstants.CL_JOIN_GAME;
        Byte[] bytesLcode   = Encoding.ASCII.GetBytes(lobbycode); // converting lobby to bytes
        byte typeOfPacket = (byte) dataTypeToSend;

        int LengthOfArray = 1  + bytesLcode.Length;

        var bytesDataTosend = new byte[LengthOfArray];
        bytesDataTosend[0] = typeOfPacket;
        int index = 1;
        for (int i = 0; i < bytesLcode.Length; i++)
        {
            bytesDataTosend[index] = bytesLcode[i];
            index += 1;
        }
        Console.WriteLine("Sending the data to server for creating the lobby !");

        List <object> resultLobbyFound = sendJoinLobbyData(bytesDataTosend);
        Console.WriteLine("Received the data from server for creating the lobby!");
        List<int> lobbyInfo = (List<int>) resultLobbyFound[0];
        int statusLobbyFound = (int)lobbyInfo[0];
        if (statusLobbyFound == SocketConstants.SE_ROOM_OK) {
            // send username and join game.
            UnityEngine.Debug.Log("Send the user data");
            // sendUserName(userName);
        }
            return resultLobbyFound;
    }

    public void sendHitRequest(float crossx, float crossy, float canx, float cany)
    {
        // function is used to send Crosshair and Canvas cordinates on fire button
        Console.WriteLine("Sending Crosshair and Canvas cordinates on fire button");
        int dataTypeToSend = SocketConstants.CL_PAINT_HIT_REQ;
        int LengthOfArray = 5;
        Byte[] bytesDataTosend = new byte[LengthOfArray];
        bytesDataTosend[0] = (byte)dataTypeToSend;
        bytesDataTosend[1] = (byte)crossx;
        bytesDataTosend[2] = (byte)crossy;
        bytesDataTosend[3] = (byte)canx;
        bytesDataTosend[4] = (byte)cany;

        Console.WriteLine("Sending the data to server");

        socket.Send(bytesDataTosend, bytesDataTosend.Length, 0);
        //Console.WriteLine("Received the data from server for hit request");
        //List<int> lobbyInfo = (List<int>)resultLobbyFound[0];
        //int statusLobbyFound = (int)lobbyInfo[0];
       // if (statusLobbyFound == SocketConstants.SE_ROOM_OK)
        //{
       //     // send username and join game.
       //     UnityEngine.Debug.Log("Send the user data");
      //     // sendUserName(userName);
       // }
        //return hitReq;
    }

    public List<object> receiveScore()
    {
        return null;
    }

    public List<object> recieveInPlayerInformation(){
        //SE_PLAYER_JOIN
        int packetNum = 0;
        int bytes = 0;
        int playerNameLength = -1;
        List<object> result = new List<object>();
        String uNameRec;
        Byte[] bytesRec;
        do
        {   if (packetNum == 0)
            {
                bytesRec = new byte[4];
                bytes = socket.Receive(bytesRec, 4, 0); // recieve one packet at a time
                // first packet is always int
                int flagNum = BitConverter.ToInt32(bytesRec, 0);
                if (flagNum == SocketConstants.SE_PLAYER_JOIN){
                    continue;
                }
                else{
                    break;
                }
            }
            else if (packetNum%2 == 0){
                // we will get playername length
                bytesRec = new byte[4];
                bytes = socket.Receive(bytesRec, 4, 0); // recieve one packet at a time
                // first packet is always int
                playerNameLength = BitConverter.ToInt32(bytesRec, 0);
            }
            else{
                //we will get user name
                if (playerNameLength >0){
                    bytesRec = new byte[playerNameLength];
                    bytes = socket.Receive(bytesRec, playerNameLength, 0); // recieve one packet at a time
                    // first packet is always int
                    // uNameRec = Encoding.ASCII.GetString(bytes,0,bytesRec);
                    //result.Add(uNameRec);
                    playerNameLength = -1;
                }
            }
        }
        while (bytes >0); // listen till we are getting the bytes.
        return result;
    }


    public List<object> recieveInGamePositionData(){
        int packetNum = 0;
        int bytes = 0;
        List<object> result = new List<object>();
        do
        {   if (packetNum == 0)
            {
                Byte[] bytesRec = new byte[4];
                bytes = socket.Receive(bytesRec, 4, 0); // recieve one packet at a time
                // first packet is always int
                int flagNum = BitConverter.ToInt32(bytesRec, 0);
                if (flagNum == SocketConstants.SE_PAINT_BALL_SYNC){
                    continue;
                }
                else{
                    break;
                }
            }
            else{
                Byte[] bytesRec = new byte[4];
                bytes = socket.Receive(bytesRec, 4, 0);
                float point = System.BitConverter.ToSingle(bytesRec, 0);
                result.Add(point);
            }
        }
        while (bytes >0); // listen till we are getting the bytes.
        return result;
    }

    private List<object> sendLobbyData(Byte[] bytesSent) {
        /*
         this is the helper method to send the data over to the server and the port.
         the answer to the request will be stored in the bytesRec and bytesSend are the bytes that needs to be sent to the server
        */
        socket.Send(bytesSent, bytesSent.Length, 0);
        UnityEngine.Debug.Log("Send the daata");
        List<object> result = new List<object>();
        int bytes = 0;
        int packetNum = 0;

        do
        {
            /*
            Below is the logic to receive the data. We everytime recieve only one packet at a time.
            First packet is always the state.
            and the packets following are the values.
            */
            UnityEngine.Debug.Log(packetNum);
            if (packetNum == 0){
                Byte[] bytesRec = new byte[4];
                bytes = socket.Receive(bytesRec, 4, 0); // recieve one packet at a time
                int flagNum = (int)bytesRec[0];
                UnityEngine.Debug.Log(flagNum);
                result.Add(flagNum);
            }
            else{
                Byte[] bytesRec = new byte[5];
                bytes = socket.Receive(bytesRec, 5, 0);
                var lobbyCode = Encoding.UTF8.GetString(bytesRec, 0, bytesRec.Length);
                result.Add(lobbyCode);
            }
            packetNum += 1;
        }
        while (packetNum <= 1);
        
        return result;
    }

private List<object> sendJoinLobbyData(Byte[] bytesSent) {
       /*
        this is the helper method to send the data over to the server and the port.
        the answer to the request will be stored in the bytesRec and bytesSend are the bytes that needs to be sent to the server
       */
       /*
       9|max_players|game_length|splash_size|number_of_players|[uuid[32]|name_length|name[name_length]]
       player =
      
       */
       socket.Send(bytesSent, bytesSent.Length, 0);
       // Thread.Sleep(2000);
       UnityEngine.Debug.Log("Send the daata");
       List<object> result = new List<object>();
       int bytes = 0;
       int packetNum = 0;
       Byte[] bytesRec ;
       Byte[] playerUname;
       int playerNameLength ;
       int numPlayersInLobby =6;
       List <int> gameInfo = new List <int>();
       do
       {
           /*
           Below is the logic to receive the data. We everytime recieve only one packet at a time.
           First packet is always the state.
           and the packets following are the values.
           */
           // UnityEngine.Debug.Log(packetNum);
           if (packetNum < 6){
               bytesRec = new byte[1];
               bytes = socket.Receive(bytesRec, 1, 0); // recieve one packet at a time
               
               int intDataRec = (int)bytesRec[0];
               UnityEngine.Debug.Log(intDataRec);
               gameInfo.Add(intDataRec);
               if (packetNum == 5){
                   
                   result.Add(gameInfo);
                   UnityEngine.Debug.Log(intDataRec);
                   numPlayersInLobby += intDataRec;
               }
           }
           else{
               
               UnityEngine.Debug.Log("Getting user info");
               // numPlayersInLobby = gameInfo[4];
               // recieve each playerInfo
               bytesRec = new byte[33];
               bytes = socket.Receive(bytesRec, 33, 0); // recieve one packet at a time
               // UnityEngine.Debug.Log(bytesRec);
               playerNameLength = (int)bytesRec[32];
               playerUname = new byte[playerNameLength];
               bytes = socket.Receive(playerUname, playerNameLength, 0);
               var playerUnameStr = Encoding.UTF8.GetString(playerUname, 0, playerUname.Length);
               UnityEngine.Debug.Log(playerUnameStr);
               UnityEngine.Debug.Log(playerNameLength);
               List <object> playerInfo = new List <object>();
               string uuid = BitConverter.ToString(bytesRec).Replace("-","");
               playerInfo.Add(uuid);
               playerInfo.Add(playerUnameStr);
               result.Add(playerInfo);
           }
           packetNum += 1;
       }
       while (packetNum < numPlayersInLobby);
      
       return result;
   }



    private List<object> sendData(Byte[] bytesSent) {
        /*
         this is the helper method to send the data over to the server and the port.
         the answer to the request will be stored in the bytesRec and bytesSend are the bytes that needs to be sent to the server
        */
        socket.Send(bytesSent, bytesSent.Length, 0);
        List<object> result = new List<object>();
        int bytes = 0;
        int packetNum = 0;

        do
        {
            /*
            Below is the logic to receive the data. We everytime recieve only one packet at a time.
            First packet is always the state.
            and the packets following are the values.
            */
            if (packetNum == 0){
                Byte[] bytesRec = new byte[4];
                bytes = socket.Receive(bytesRec, 4, 0); // recieve one packet at a time
                // first packet is always int
                int flagNum = BitConverter.ToInt32(bytesRec, 0);
                result.Add(flagNum);
            }
            else{
                Byte[] bytesRec = new byte[5];
                bytes = socket.Receive(bytesRec, 5, 0);
                float point = System.BitConverter.ToSingle(bytesRec, 0);
                result.Add(point);
            }
        }
        while (bytes > 0);
        
        return result;
    }
    private void sendUserNameLobbyCreator(Byte[] bytesSent) {
        /*
         this is the helper method to send the data over to the server and the port.
         the answer to the request will be stored in the bytesRec and bytesSend are the bytes that needs to be sent to the server
        */
        socket.Send(bytesSent, bytesSent.Length, 0);
    }

    public void startPainting(){
        int dataTypeToSend  = SocketConstants.SE_START_REQUEST;
        byte typeOfPacket = (byte) dataTypeToSend;
        int LengthOfArray = 1;
        var bytesDataTosend = new byte[LengthOfArray];
        bytesDataTosend[0] = typeOfPacket;
        socket.Send(bytesDataTosend, bytesDataTosend.Length, 0);
    }

    public List<object> isThereNewUser(){
        // int dataTypeToSend  = SocketConstants.SE_PLAYER_JOIN;
       Byte[] bytesRec ;
       int bytes = 0;
       Byte[] playerUname;
       int playerNameLength ;
       int numIteration =1;
       int packetNum = 0;
       List <object> result = new List <object>();
       int currentFlag = 0;
       do
       {
           /*
           Below is the logic to receive the data. We everytime recieve only one packet at a time.
           First packet is always the state.
           and the packets following are the values.
           */
           // UnityEngine.Debug.Log(packetNum);
           if (packetNum == 0){
                // accept only one byte now !
                try
                {
                    bytesRec = new byte[4];
                    bytes = socket.Receive(bytesRec, 4, 0); // recieve one packet at a time
                    // first packet is always int
                    currentFlag = (int)bytesRec[0];
                    UnityEngine.Debug.Log(currentFlag);
                    if (currentFlag == SocketConstants.SE_PLAYER_JOIN || currentFlag == SocketConstants.SE_PLAYER_NAME_UPD || 
                    currentFlag == SocketConstants.SE_GAME_START )
                    {
                        numIteration +=1;
                        
                    }
                }
                catch (SocketException se) 
                {  
                    Console.WriteLine("SocketException : {0}",se.ToString());  
                }
                result.Add(currentFlag);
           }
           else{
               // once flag is recieved update
               if (currentFlag == SocketConstants.SE_PLAYER_NAME_UPD){
                   // condition if the new player joins
                    bytesRec = new byte[33];
                    bytes = socket.Receive(bytesRec, 33, 0); // recieve one packet at a time
                    string uuid = BitConverter.ToString(bytesRec).Replace("-","");
                    // UnityEngine.Debug.Log(bytesRec);
                    playerNameLength = (int)bytesRec[32];
                    playerUname = new byte[playerNameLength];
                    bytes = socket.Receive(playerUname, playerNameLength, 0);
                    var playerUnameStr = Encoding.UTF8.GetString(playerUname, 0, playerUname.Length);
                    UnityEngine.Debug.Log(playerUnameStr);
                    List <object> playerInfo = new List <object>();
                    playerInfo.Add(uuid);
                    playerInfo.Add(playerUnameStr);
                    result.Add(playerInfo);
               }
               if (currentFlag == SocketConstants.SE_PLAYER_JOIN){
                   // condition if the new player joins
                    bytesRec = new byte[32];
                    bytes = socket.Receive(bytesRec, 32, 0); // recieve one packet at a time
                    // UnityEngine.Debug.Log(bytesRec);
                    string uuid = BitConverter.ToString(bytesRec).Replace("-","");
                    List <object> playerInfo = new List <object>();
                    playerInfo.Add(uuid);
                    result.Add(playerInfo);
               }
           }
           packetNum += 1;
       }
       while (packetNum < numIteration);
       return result;
    }

    public static void Main(string[] args)
    {
        string host="localhost";
        int port = 10500;

        if (args.Length == 0)
            // If no server name is passed as argument to this program,
            // use the current host name as the default.
            host = Dns.GetHostName();
        else
            host = args[0];
        //GetSocket socketObj = new  GetSocket(host, port);
        //socketObj.ConnectSocket();
        // string result = SocketSendReceive(host, port);
        //Console.WriteLine(result);
    }
}