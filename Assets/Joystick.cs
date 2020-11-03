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

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        outercircle.SetActive(false);
        innercircle.SetActive(false);
        moveSpeed = 6f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            oneTouch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(oneTouch.position);

            switch (oneTouch.phase)
            {
                case TouchPhase.Began:
                    outercircle.SetActive(true);
                    innercircle.SetActive(true);

                    outercircle.transform.position = touchPosition;
                    innercircle.transform.position = touchPosition;

                    break;

                case TouchPhase.Stationary:
                    moveDuck();

                    break;

                case TouchPhase.Moved:
                    moveDuck();

                    break;

                case TouchPhase.Ended:
                    outercircle.SetActive(false);
                    innercircle.SetActive(false);

                    rb.velocity = Vector2.zero;

                    break;
            }
        }

    }

    private void moveDuck()
    {
        innercircle.transform.position = touchPosition;

        innercircle.transform.position = new Vector2(
            Mathf.Clamp(innercircle.transform.position.x,
            outercircle.transform.position.x - 0.63f,
            outercircle.transform.position.x + 0.63f),
            Mathf.Clamp(innercircle.transform.position.y,
            outercircle.transform.position.y - 0.63f,
            outercircle.transform.position.y + 0.63f)
        );

        moveDirection = (innercircle.transform.position - outercircle.transform.position).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }
}
