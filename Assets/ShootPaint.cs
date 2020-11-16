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
    }

    public void firePaint(){
        Instantiate(pfSplat, new Vector2(rb.transform.localPosition.x, rb.transform.localPosition.y), Quaternion.identity);
    }
}
