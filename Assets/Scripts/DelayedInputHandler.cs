using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedInputHandler : MonoBehaviour
{
    [System.Serializable]
    public struct InputTable
    {
        public KeyCode TurnRight;
        public KeyCode TurnLeft;
        public KeyCode Accelerate;
        public KeyCode Shoot;
    }
    public InputTable MyInputsPlayer1;
    public InputTable MyInputsPlayer2;
    public GameObject InputCarrier;
    public Vector2 BasePosition1;
    public Vector2 BasePosition2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetInputPlayer1();
        GetInputPlayer2();
    }

    void GetInputPlayer1()
    {
        if (Input.GetKeyDown(MyInputsPlayer1.TurnRight))
        {
            GameObject ob = Instantiate(InputCarrier, BasePosition1, Quaternion.identity);
            ob.GetComponent<InputCarrier>().Initialize(global::InputCarrier.ShipOrder.TurnRight, 1);
        }
        if (Input.GetKeyDown(MyInputsPlayer1.TurnLeft))
        {
            GameObject ob = Instantiate(InputCarrier, BasePosition1, Quaternion.identity);
            ob.GetComponent<InputCarrier>().Initialize(global::InputCarrier.ShipOrder.TurnLeft, 1);
        }
        if (Input.GetKeyDown(MyInputsPlayer1.Accelerate))
        {
            GameObject ob = Instantiate(InputCarrier, BasePosition1, Quaternion.identity);
            ob.GetComponent<InputCarrier>().Initialize(global::InputCarrier.ShipOrder.Accelerate, 1);
        }
        if (Input.GetKeyDown(MyInputsPlayer1.Shoot))
        {
            GameObject ob = Instantiate(InputCarrier, BasePosition1, Quaternion.identity);
            ob.GetComponent<InputCarrier>().Initialize(global::InputCarrier.ShipOrder.Shoot, 1);
        }

    }

    void GetInputPlayer2()
    {
        if (Input.GetKeyDown(MyInputsPlayer2.TurnRight))
        {
            GameObject ob = Instantiate(InputCarrier, BasePosition2, Quaternion.identity);
            ob.GetComponent<InputCarrier>().Initialize(global::InputCarrier.ShipOrder.TurnRight, 2);
        }
        if (Input.GetKeyDown(MyInputsPlayer2.TurnLeft))
        {
            GameObject ob = Instantiate(InputCarrier, BasePosition2, Quaternion.identity);
            ob.GetComponent<InputCarrier>().Initialize(global::InputCarrier.ShipOrder.TurnLeft, 2);
        }
        if (Input.GetKeyDown(MyInputsPlayer2.Accelerate))
        {
            GameObject ob = Instantiate(InputCarrier, BasePosition2, Quaternion.identity);
            ob.GetComponent<InputCarrier>().Initialize(global::InputCarrier.ShipOrder.Accelerate, 2);
        }
        if (Input.GetKeyDown(MyInputsPlayer2.Shoot))
        {
            GameObject ob = Instantiate(InputCarrier, BasePosition2, Quaternion.identity);
            ob.GetComponent<InputCarrier>().Initialize(global::InputCarrier.ShipOrder.Shoot, 2);
        }

    }
}
