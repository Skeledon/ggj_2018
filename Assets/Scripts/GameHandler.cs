using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure; // Required in C#


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
    private bool finished = false;

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
        currentGameStep = 0;
        StartCoroutine(CountdownStart());

    }

    private void OnDestroy()
    {
        Checkpoint.Reached -= CheckpointReachedByPlayer;
    }
    // Update is called once per frame
    void Update ()
    {
        if (ready && !finished)
        {
            currentTime -= Time.deltaTime;
            ScoreTextP1.ChangeNumber(currentScoreP1);
            ScoreTextP2.ChangeNumber(currentScoreP2);
            TimeText.ChangeNumber((int)currentTime);
            if (currentTime <= 0)
            {
                finished = true;
                StartCoroutine(WaitAndRestart());
            }
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
                StartCoroutine(Vibrate());

            }
        }
        if (currentGameStep == 1)
        {
            if (currentStepTime >= SecondInversion)
            {
                currentGameStep++;
                nextGameStep();
                currentStepTime = 0;
                GetComponent<AudioSource>().Play();
                StartCoroutine(Vibrate());

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
    
    IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(3);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        
    }

    IEnumerator Vibrate()
    {
        GamePad.SetVibration(PlayerIndex.One, 1, 1);
        GamePad.SetVibration(PlayerIndex.Two, 1, 1);
        yield return new WaitForSeconds(2);
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
        GamePad.SetVibration(PlayerIndex.Two, 0, 0);
    }
}
