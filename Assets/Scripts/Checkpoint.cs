using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public delegate void CheckpointReached(int player);
    public static event CheckpointReached Reached;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 newPosition;
            do
            {
                newPosition = new Vector2(Random.Range(-6, 6), Random.Range(-3, 3));
            } while (((Vector2)transform.position - newPosition).SqrMagnitude() <= 4);
            transform.position = newPosition;
            Reached(collision.GetComponent<Spaceship>().Player);
        }
    }
}
