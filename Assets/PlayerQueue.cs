using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQueue : MonoBehaviour
{
    // Start is called before the first frame update
    public GetSocket socketObj ;
    void Start()
    {
        socketObj = SocketFactory.getSocketForApp("127.0.0.1", 10500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
