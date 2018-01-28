using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure; // Required in C#

public class Intro : MonoBehaviour
{
    public float FadeSpeed;
    public Boing bo;

    private float currentAlpha;
    private SpriteRenderer rend;
    private float r;
    private float g;
    private float b;
    private bool ready = false;
	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
        r = rend.color.r;
        g = rend.color.g;
        b = rend.color.b;
        currentAlpha = -1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!ready)
        {
            if (currentAlpha < 1)
            {
                rend.color = new Color(r, g, b, currentAlpha);
                currentAlpha += FadeSpeed * Time.deltaTime;
            }
            else
            {
                bo.StartBouncing();
                ready = true;
            }
        }
        else
        {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                SceneManager.LoadScene(1);
            }
        }
	}


}
