using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

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

    public List<object> createLobby(string username, int numberOfPlayers) {
        // function is used to initialise the handshake with the server and then create a lobby
        Console.WriteLine("Creating the lobby !");
        int typeOfPacketInt = SocketConstants.CL_CREATE_GAME;
        int lengthUname = username.Length;
        Byte[] bytesuName = Encoding.ASCII.GetBytes(username); // converting uname to bytes
        byte uLength = (byte) lengthUname;
        byte pNum = (byte) numberOfPlayers;
        byte typeOfPacket = (byte) typeOfPacketInt;

        int LengthOfArray = bytesuName.Length+3; 
        
        var bytesDataTosend = new byte[LengthOfArray];
        
        bytesDataTosend[0] = typeOfPacket;
        bytesDataTosend[1] = uLength;
        int index = 2; 
        for (int i = 0; i < bytesuName.Length; i++)
        {
            bytesDataTosend[index] = bytesuName[i];
            index += 1;
        }
        bytesDataTosend[index] = pNum;
        //bytesDataTosend[-1] = numberOfPlayers;
        Console.WriteLine("Sending the data to server for creating the lobby !");
        List<object> result = sendData(bytesDataTosend);
        Console.WriteLine("Received the data from server for creating the lobby!");
        return result;
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

        List <object> resultLobbyFound = sendData(bytesDataTosend);
        Console.WriteLine("Received the data from server for creating the lobby!");
        int statusLobbyFound = (int) resultLobbyFound[0];
        if (statusLobbyFound == SocketConstants.SE_ROOM_OK) {
            // send username and join game.
            int lengthUName = userName.Length;            
            dataTypeToSend  = SocketConstants.CL_SEND_USERNAME;// converting lobby to bytes
            
            Byte[] bytesUname   = Encoding.ASCII.GetBytes(userName);
            typeOfPacket = (byte) dataTypeToSend;
            byte bytesULen = (byte)lengthUName;

            LengthOfArray = bytesUname.Length  + 2;
            bytesDataTosend = new byte[LengthOfArray];
            bytesDataTosend[0] = typeOfPacket;
            bytesDataTosend[1] = bytesULen;
            index = 2;
            for (int i = 0; i < bytesUname.Length; i++)
            {
                bytesDataTosend[index] = bytesUname[i];
                index += 1;
            }
            List <object> resultAddUserToLobby = sendData(bytesDataTosend);
            return resultAddUserToLobby;
        }
        else{
            return resultLobbyFound;
        }
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
                Byte[] bytesRec = new byte[4];
                bytes = socket.Receive(bytesRec, 4, 0);
                float point = System.BitConverter.ToSingle(bytesRec, 0);
                result.Add(point);
            }
        }
        while (bytes > 0);
        
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