using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{

    public float minX, maxX, minY, maxY;

    Vector2 targetPosition;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            targetPosition = getRandomPosition2(transform.position);
        }
    }

    Vector2 getRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }

    Vector2 getRandomPosition2(Vector2 pos)
    {
        //return new Vector2(0, 0);
        Vector2 toPos = new Vector2(maxX,maxY);
        if (pos.x == 0 && pos.y == 0)
            toPos = toRight();
        else if (pos.x == minX && pos.y == minY)
            toPos = toRight();
        else if (pos.x == minX && pos.y == maxY)
            toPos = toRight();
        else if (pos.x == maxX && pos.y == minY)
            toPos = toLeft();
        else if (pos.x == maxX && pos.y == maxY)
            toPos = toLeft();
        else if (pos.x == maxX)
            toPos = toBottom();
        else if (pos.y == maxY)
            toPos = toRight();
        else if (pos.x == minX)
            toPos = toTop();
        else if (pos.y == minY)
            toPos = toLeft();
        //Debug.Log(toPos.x +" and "+toPos.y);
        return toPos;

    }

    Vector2 toRight()
    {
        float newY = Random.Range(maxY, minY);
        //Debug.Log("To Right");
        return new Vector2(maxX, newY);
    }
    Vector2 toLeft()
    {
        float newY = Random.Range(maxY, minY);
        //Debug.Log("To Left");
        return new Vector2(minX, newY);
    }
    Vector2 toTop()
    {
        float newX = Random.Range(maxX, minX);
        //Debug.Log("TO Top");
        return new Vector2(newX, maxY);
    }
    Vector2 toBottom()
    {
        float newX = Random.Range(maxX, minX);
        //Debug.Log("To Bottom");
        return new Vector2(newX, minY);
    }
}
