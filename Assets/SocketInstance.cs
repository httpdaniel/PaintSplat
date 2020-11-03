using System;
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
        socket = ConnectSocket();
    }

    private Socket ConnectSocket()
    {
        Socket s = null;
        IPHostEntry hostEntry = null;

        // Get host related information.
        hostEntry = Dns.GetHostEntry(serverHostName);

        // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
        // an exception that occurs when the host IP Address is not compatible with the address family
        // (typical in the IPv6 case).
        foreach (IPAddress address in hostEntry.AddressList)
        {
            IPEndPoint ipe = new IPEndPoint(address, serverPort);
            Socket tempSocket =
                new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            tempSocket.Connect(ipe);

            if (tempSocket.Connected)
            {
                s = tempSocket;
                break;
            }
            else
            {
                continue;
            }
        }
        return s;
    }

    private byte[] changeIntToByteArray(int value) {
        byte[] valueByte = BitConverter.GetBytes(value);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(valueByte);
        byte[] valueByteRes = valueByte;
        
        return valueByteRes;
    }
    
    private void createLobby(string username, int numberOfPlayers) {
        // function is used to initialise the handshake with the server and then create a lobby
        Console.WriteLine("Creating the lobby !");
        int lengthUname = username.Length;
        Byte[] bytesuName = Encoding.ASCII.GetBytes(username); // converting uname to bytes
        Byte[] bytesuLen = changeIntToByteArray(lengthUname);
        Byte[] bytesPNum = changeIntToByteArray(numberOfPlayers);
        Byte[] bytestype = changeIntToByteArray(0);

        int LengthOfArray = bytesuName.Length+bytesuLen.Length+bytesPNum.Length+bytestype.Length; 
        
        var bytesDataTosend = new byte[LengthOfArray];
        int index = 0; 
        
        for (int i = 0; i < bytestype.Length; i++)
        {
            bytesDataTosend[index] = bytestype[i];
            index += 1;
        }

        for (int i = 0; i < bytesuLen.Length; i++)
        {
            bytesDataTosend[index] = bytesuLen[i];
            index += 1;
        }

        for (int i = 0; i < bytesuName.Length; i++)
        {
            bytesDataTosend[index] = bytesuName[i];
            index += 1;
        }

        for (int i = 0; i < bytesPNum.Length; i++)
        {
            bytesDataTosend[index] = bytesPNum[i];
            index += 1;
        }
        //bytesDataTosend[-1] = numberOfPlayers;
        Console.WriteLine("Sending the data to server for creating the lobby !");
        sendData(bytesDataTosend, new byte[10]);
        Console.WriteLine("Received the data from server for creating the lobby!");
    }

    private void sendData(Byte[] bytesSent, Byte[] bytesRec) {
        /*
         this is the helper method to send the data over to the server and the port.
         the answer to the request will be stored in the bytesRec and bytesSend are the bytes that needs to be sent to the server
         */
        socket.Send(bytesSent, bytesSent.Length, 0);
        int bytes = 0;
        string page = "";
        do
        {
            bytes = socket.Receive(bytesRec, bytesRec.Length, 0);
            page = page + Encoding.ASCII.GetString(bytesRec, 0, bytes);
        }
        while (bytes > 0);
    }

    public static void Main(string[] args)
    {
        string host;
        int port = 80;

        if (args.Length == 0)
            // If no server name is passed as argument to this program,
            // use the current host name as the default.
            host = Dns.GetHostName();
        else
            host = args[0];
        GetSocket socketObj = new  GetSocket(host, port);

        // string result = SocketSendReceive(host, port);
        //Console.WriteLine(result);
    }
}