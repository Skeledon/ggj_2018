using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

[RequireComponent(typeof(Spaceship))]
public class InputHandler : MonoBehaviour
{
    [System.Serializable]
    public struct InputTable
    {
        public string TurnRight;
        public string TurnLeft;
        public string Accelerate;
        public string Shoot;
    }
    public InputTable[] MyInputs = new InputTable[3];

    private Spaceship myShip;
    private int currentGameStep = 0;
    private bool ready;

	// Use this for initialization
	void Start ()
    {
        myShip = GetComponent<Spaceship>();
        GameHandler.nextGameStep += nextStep;
        GameHandler.GameReadyToStart += GreenToGo;
    }

    private void OnDestroy()
    {
        GameHandler.nextGameStep -= nextStep;
        GameHandler.GameReadyToStart -= GreenToGo;
    }
    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            getInput();
        }
    }

    void getInput()
    {
        if (Input.GetAxis(MyInputs[currentGameStep].TurnRight) > 0)
        {
            myShip.Turn(-Input.GetAxis(MyInputs[currentGameStep].TurnRight));
        }
        if (Input.GetAxis(MyInputs[currentGameStep].TurnLeft) < 0)
        {
            myShip.Turn(-Input.GetAxis(MyInputs[currentGameStep].TurnLeft));
        }
        if (Input.GetAxis(MyInputs[currentGameStep].Accelerate) > 0)
        {
            myShip.Accelerate();
        }
        if (Input.GetButtonDown(MyInputs[currentGameStep].Shoot))
        {
            myShip.Shoot();
        }

    }

    private void nextStep()
    {
        currentGameStep++;
    }

    void GreenToGo()
    {
        ready = true;
    }
}
