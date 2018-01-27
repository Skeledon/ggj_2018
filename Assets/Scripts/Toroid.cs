using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toroid : MonoBehaviour
{
    private float maxX;
    private float minX;
    private float maxY;
    private float minY;
	// Use this for initialization
	void Start ()
    {
        float offset = .8f;
        maxX = 8 + offset;
        minX = -8 - offset;
        maxY = 5 + offset;
        minY = -5 - offset;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float currentX = transform.position.x;
        float currentY = transform.position.y;

        if (currentY < minY)
        {
            transform.position = new Vector2(currentX, maxY);
        }
        if (currentY > maxY)
        {
            transform.position = new Vector2(currentX, minY);
        }
        if (currentX < minX)
        {
            transform.position = new Vector2(maxX, currentY);
        }
        if (currentX > maxX)
        {
            transform.position = new Vector2(minX, currentY);
        }
    }
}
