using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boing : MonoBehaviour
{
    public AnimationCurve ScaleX;
    public AnimationCurve ScaleY;
    public float SpeedMultiplier;


    private float currentTime;
    private SpriteRenderer rend;

    
	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localScale = new Vector2(ScaleX.Evaluate(currentTime * SpeedMultiplier), ScaleY.Evaluate(currentTime * SpeedMultiplier));
        currentTime += Time.deltaTime;
        if(currentTime * SpeedMultiplier >= 1)
        {
            currentTime = 0;
        }
	}

    public void StartBouncing()
    {
        rend.enabled = true;
    }
}
