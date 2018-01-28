using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameHandler : MonoBehaviour
{
    public delegate void AdvanceStep();
    public static event AdvanceStep nextGameStep;
    public delegate void StartGame();
    public static event StartGame GameReadyToStart;
    public GameObject MyUI;
    public float RoundTime;
    public int FirstInversion;
    public int SecondInversion;

    private int currentScoreP1;
    private int currentScoreP2;
    private float currentTime;
    private int currentGameStep = 0;
    private float currentStepTime;
    private bool ready = false;

    public ScoreBoard ScoreTextP1;
    public ScoreBoard ScoreTextP2;
    public ScoreBoard TimeText;
    public ScoreTable CountDown;

    public int CurrentStep
    {
        get
        {
            return currentGameStep;
        }
    }

    // Use this for initialization
    void Start ()
    {
        Checkpoint.Reached += CheckpointReachedByPlayer;
        currentTime = RoundTime;
        StartCoroutine(CountdownStart());

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ready)
        {
            currentTime -= Time.deltaTime;
            ScoreTextP1.ChangeNumber(currentScoreP1);
            ScoreTextP2.ChangeNumber(currentScoreP2);
            TimeText.ChangeNumber((int)currentTime);
            if (currentTime <= 0)
                Time.timeScale = 0;
            HandleSteps();
        }
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
                GetComponent<AudioSource>().Play();

            }
        }
        if (currentGameStep == 1)
        {
            if (currentStepTime >= FirstInversion)
            {
                currentGameStep++;
                nextGameStep();
                currentStepTime = 0;
                GetComponent<AudioSource>().Play();

            }
        }
        currentStepTime += Time.deltaTime;

    }

    IEnumerator CountdownStart()
    {
        int count = 3;
        while (count > 0)
        {
            CountDown.ChangeNumber(count);
            count--;
            yield return new WaitForSeconds(1);
        }
        Destroy(CountDown.gameObject);
        GameReadyToStart();
        ready = true;
    }
}
