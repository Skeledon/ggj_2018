using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCarrier : MonoBehaviour
{
    public enum ShipOrder { Accelerate, TurnRight, TurnLeft, Shoot, NoOrder };
    public ShipOrder CurrentOrder;
    public float EnlargeSpeed;

    public int player;


    public void Initialize(ShipOrder or, int p)
    {
        CurrentOrder = or;
        player = p;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localScale += Vector3.one * EnlargeSpeed * Time.deltaTime;
        if(transform.localScale.x >= 40)
        {
            Destroy(gameObject);
        }
	}
}
