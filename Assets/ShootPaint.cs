using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootPaint : MonoBehaviour
{
    public int hitCounter;
    public Text scoreText;
    [SerializeField]
    public GameObject GameManager;


    [SerializeField] public Transform pfSplat;
    public Rigidbody2D rb;
    public GameObject canvas;
    GetSocket socketObj;
    private float update = 0.0f;
    private void awake() {
        //GetComponent<Button>().onClick() += firePaint;
    }

    // Start is called before the first frame update
    void Start()
    {
        socketObj = SocketFactory.getSocketForApp(SocketConstants.SERVER_HOST, SocketConstants.SERVER_PORT);
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
        // checkScore();
        update += Time.deltaTime;
        if (update > 1.0f)
        {
            update = 0.0f;
            List<object> results = socketObj.getHitPositions();
            int isAhit = (int)results[0];
            if (isAhit == SocketConstants.SE_PAINT_HIT_OK){
                string uuid = (string)results[1];
                float x = (float)results[2];
                float y = (float)results[3];
                Instantiate(pfSplat, new Vector2(x, y), Quaternion.identity);
            }
            if (isAhit == SocketConstants.SE_GAME_OVER){
                Debug.Log("!!!!!!!!!!! Game Over !!!!!!!!!!!");
                // GameManager.endGame();
                GameManager.GetComponent<GameManager>().endGame();
            }
        }
    }

    public void firePaint(){
        float crossx = rb.transform.localPosition.x;
        float crossy = rb.transform.localPosition.y;

        float canx = canvas.transform.localPosition.x;
        float cany = canvas.transform.localPosition.y;

        Debug.Log("crosshair:" + crossx + "," + crossy);
        Debug.Log("canvas:" + canx + "," + cany);

        
        socketObj.sendHitRequest(crossx,crossy,canx,cany);
    
        Debug.Log("Received the data from server ");

        // 

    }

    public void checkScore()
    {
        // GetSocket socketObj = SocketFactory.getSocketForApp(SocketConstants.SERVER_HOST, SocketConstants.SERVER_PORT);
        socketObj.receiveScore();
    }
}
