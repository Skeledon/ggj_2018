using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameHandler : MonoBehaviour
{
    public delegate void AdvanceStep();
    public static event AdvanceStep nextGameStep;
    public GameObject MyUI;
    public float RoundTime;
    public int FirstInversion;
    public int SecondInversion;

    private int currentScoreP1;
    private int currentScoreP2;
    private float currentTime;
    private int currentGameStep = 0;
    private float currentStepTime;

    public Text ScoreTextP1;
    public Text ScoreTextP2;
    public Text TimeText;
	// Use this for initialization
	void Start ()
    {
        Checkpoint.Reached += CheckpointReachedByPlayer;
        currentTime = RoundTime;

    }
	
	// Update is called once per frame
	void Update ()
    {
        currentTime -= Time.deltaTime;
        ScoreTextP1.text = currentScoreP1.ToString();
        ScoreTextP2.text = currentScoreP2.ToString();
        TimeText.text = currentTime.ToString("0");
        if (currentTime <= 0)
            Time.timeScale = 0;
        HandleSteps();
	}

    private void CheckpointReachedByPlayer(int player)
    {
        if (player == 1)
            currentScoreP1++;
        else
            currentScoreP2++;
    }

    private void HandleSteps()
    {
        if (currentGameStep == 0)
        {
            if (currentStepTime >= FirstInversion)
            {
                currentGameStep++;
                nextGameStep();
                currentStepTime = 0;

            }
        }
        if (currentGameStep == 1)
        {
            if (currentStepTime >= FirstInversion)
            {
                currentGameStep++;
                nextGameStep();
                currentStepTime = 0;

            }
        }
        currentStepTime += Time.deltaTime;
    }
}
