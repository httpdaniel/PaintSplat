using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

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

    private void createLobby(string username, int numberOfPlayers) { 
    // function is used to initialise the handshake with the server and then create a lobby
    
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

        // string result = SocketSendReceive(host, port);
        Console.WriteLine(result);
    }
}