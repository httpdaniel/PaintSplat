using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    private GameObject innercircle, outercircle;
    private Rigidbody2D rb;
    private float moveSpeed;
    private Touch oneTouch;
    private Vector2 touchPosition;
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        outercircle.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
