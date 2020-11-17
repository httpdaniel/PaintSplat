using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootPaint : MonoBehaviour
{
    public int hitCounter;
    public Text scoreText;
    [SerializeField] public Transform pfSplat;
    public Rigidbody2D rb;
    public GameObject canvas;

    private void awake() {
        //GetComponent<Button>().onClick() += firePaint;
    }

    // Start is called before the first frame update
    void Start()
    {
        hitCounter = 0;
        scoreText.text = "Score: " + hitCounter;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + hitCounter;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hitCounter++;
        }
        checkScore();
    }

    public void firePaint(){
        float crossx = rb.transform.localPosition.x;
        float crossy = rb.transform.localPosition.y;

        float canx = canvas.transform.localPosition.x;
        float cany = canvas.transform.localPosition.y;

        Debug.Log("crosshair:" + crossx + "," + crossy);
        Debug.Log("canvas:" + canx + "," + cany);

        GetSocket socketObj = SocketFactory.getSocketForApp(SocketConstants.SERVER_HOST, SocketConstants.SERVER_PORT);
        socketObj.sendHitRequest(crossx,crossy,canx,cany);
        Debug.Log("Received the data from server ");

        //Instantiate(pfSplat, new Vector2(rb.transform.localPosition.x, rb.transform.localPosition.y), Quaternion.identity);

    }

    public void checkScore()
    {
        GetSocket socketObj = SocketFactory.getSocketForApp(SocketConstants.SERVER_HOST, SocketConstants.SERVER_PORT);
        socketObj.receiveScore();
    }
}
