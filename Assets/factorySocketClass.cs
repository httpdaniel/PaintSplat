using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class SocketFactory 
{
    static GetSocket socket = null;
    public static GetSocket getSocketForApp(string hostName, int port) {
        if (socket == null) {
            socket = new GetSocket(hostName, port);

        }
        return socket;
    }
}
