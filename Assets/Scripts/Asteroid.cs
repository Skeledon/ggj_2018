using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float RotationSpeed;
    public float MovementSpeed;

    Vector2 currentDirection;
    int[] seeds = { 321, 45, 654756, 254, 78, 346, 23654, 258, 38, 376, 551, 12, 769, 269, 7654, 5, 6876, 3587, 2459, 44745, 872, 567457, 723465769, 3465542, 45866545, 62597652, 3534 };

    private bool ready = false;
	// Use this for initialization
	void Start ()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        Random.InitState(seeds[Random.Range(0, seeds.Length)]);
        currentDirection = new Vector2(Random.value, Random.value);
        currentDirection.Normalize();
        transform.position = new Vector2(Random.Range(-20, 20), Random.Range(-15, 15));
        GameHandler.GameReadyToStart += GreenToGo;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ready)
        {
            transform.Translate(currentDirection * Time.deltaTime * MovementSpeed, Space.World);
            transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
        }
	}

    void GreenToGo()
    {
        ready = true;
    }

}
