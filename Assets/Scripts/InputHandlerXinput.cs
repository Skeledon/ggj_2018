using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class InputHandlerXinput : MonoBehaviour
{
    [System.Serializable]
    public struct MyXinputInputs
    {
        public string RightTurning;
        public string LeftTurning;
        public string Trigger;
        public string Button;
    }
    public MyXinputInputs[] MyInputs = new MyXinputInputs[3];
    public PlayerIndex ControllerNumber;
    public GamePadThumbSticks Analog;

    private GamePadState state;
    private string currentStickRight;
    private string currentStickLeft;
    private string currentTrigger;
    private string currentButton;

    private Spaceship myShip;
    private int currentGameStep = 0;
    private bool ready;

    // Use this for initialization
    void Start()
    {
        myShip = GetComponent<Spaceship>();
        GameHandler.nextGameStep += NextStep;
        GameHandler.GameReadyToStart += GreenToGo;
        AssignValues(0);

        
    }

    private void OnDestroy()
    {
        GameHandler.nextGameStep -= NextStep;
        GameHandler.GameReadyToStart -= GreenToGo;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            state = GamePad.GetState(ControllerNumber);
            getInput();
        }
    }

    void getInput()
    {
        if (currentStickRight == "Left")
        { 
            if (state.ThumbSticks.Left.X > 0)
            {
                {
                    myShip.Turn(-state.ThumbSticks.Left.X);
                }
            }
        }
        else
        {
            if (state.ThumbSticks.Right.X > 0)
            {
                {
                    myShip.Turn(-state.ThumbSticks.Right.X);
                }
            }
        }

        if (currentStickLeft == "Left")
        {
            if (state.ThumbSticks.Left.X < 0)
            {
                {
                    myShip.Turn(-state.ThumbSticks.Left.X);
                }
            }
        }
        else
        {
            if (state.ThumbSticks.Right.X < 0)
            {
                {
                    myShip.Turn(-state.ThumbSticks.Right.X);
                }
            }
        }


        if (currentTrigger == "Left")
        {
            if (state.Triggers.Left > 0)
            {
                myShip.Accelerate();
            }
        }
        else
        {
            if (state.Triggers.Right > 0)
            {
                myShip.Accelerate();
            }
        }


        if (currentButton == "Left")
        {
            if(state.Buttons.LeftShoulder == ButtonState.Pressed)
                myShip.Shoot();
        }
        else
        {
            if (state.Buttons.RightShoulder == ButtonState.Pressed)
                myShip.Shoot();
        }

    }

    private void NextStep()
    {
        currentGameStep++;
        AssignValues(currentGameStep);
    }

    void GreenToGo()
    {
        ready = true;
    }

    void AssignValues(int n)
    {
        Debug.Log(n);
        currentTrigger = MyInputs[n].Trigger;
        currentStickRight = MyInputs[n].RightTurning;
        currentStickLeft = MyInputs[n].LeftTurning;
        currentButton = MyInputs[n].Button;
    }

}
