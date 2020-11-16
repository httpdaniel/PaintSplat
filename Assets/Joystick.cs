using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public GameObject innercircle, outercircle;
    private Rigidbody2D rb;
    private float moveSpeed;
    private Touch oneTouch;
    private Vector2 touchPosition;
    private Vector2 moveDirection;
    private Vector2 location;
    private float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        // Get original location of joystick
        location = new Vector2(innercircle.transform.position.x, innercircle.transform.position.y);
        rb = GetComponent<Rigidbody2D>();

        // Speed to move crosshair
        moveSpeed = 9f;

        // set boundaries
        minX = -8.463f;
        maxX = 8.463f;
        minY = -4.58f;
        maxY = 4.58f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Get touch phases and move joystick accordingly 
            oneTouch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(oneTouch.position);

            switch (oneTouch.phase)
            {
                case TouchPhase.Began:

                    break;

                case TouchPhase.Stationary:
                    moveTarget();

                    break;

                case TouchPhase.Moved:
                    moveTarget();

                    break;

                case TouchPhase.Ended:

                    // Return to original position
                    innercircle.transform.position = location;

                    // Set velocity back to 0
                    rb.velocity = Vector2.zero;

                    break;
            }
        }

        // prevent crosshair going outside boundary
        if (rb.transform.position.x < minX){
         rb.transform.position = new Vector3(minX, rb.transform.position.y, rb.transform.position.z);
        } else if (rb.transform.position.x > maxX){
         rb.transform.position = new Vector3(maxX, rb.transform.position.y, rb.transform.position.z);
        } else if (rb.transform.position.y < minY){
         rb.transform.position = new Vector3(rb.transform.position.x, minY, rb.transform.position.z);
        } else if (rb.transform.position.y > maxY){
         rb.transform.position = new Vector3(rb.transform.position.x, maxY, rb.transform.position.z);
        }

    }

    // Move joystick
    private void moveTarget()
    {
        // Find touch position
        innercircle.transform.position = touchPosition;

        // Move inner circle within circumference of outer
        innercircle.transform.position = new Vector2(
            Mathf.Clamp(innercircle.transform.position.x,
            outercircle.transform.position.x - 0.63f,
            outercircle.transform.position.x + 0.63f),
            Mathf.Clamp(innercircle.transform.position.y,
            outercircle.transform.position.y - 0.63f,
            outercircle.transform.position.y + 0.63f)
        );

        // Update move direction for crosshair 
        moveDirection = (innercircle.transform.position - outercircle.transform.position).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }
}
