using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public int NumberOfFlashes;
    public float FlashSpeed;
    public float TargetAlpha;

    private SpriteRenderer rend;
    private int count = 0;
    private float r;
    private float g;
    private float b;
	// Use this for initialization
	void Awake ()
    {
        rend = GetComponent<SpriteRenderer>();
        r = rend.color.r;
        g = rend.color.g;
        b = rend.color.b;
	}

    private void Start()
    {
        GameHandler.nextGameStep += StartFlashing;
    }

    private void OnDestroy()
    {

        GameHandler.nextGameStep -= StartFlashing;
    }
    // Update is called once per frame
    void Update ()
    {
		
	}

    private void StartFlashing()
    {
        count = 0;
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        float currentAlpha = 0;
        while (currentAlpha <= TargetAlpha)
        {
            currentAlpha += FlashSpeed * Time.deltaTime;
            rend.color = new Color(r, g, b, currentAlpha);
            yield return null;
        }
        rend.color = new Color(r, g, b, TargetAlpha);
        while (currentAlpha > 0 )
        {
            currentAlpha -= FlashSpeed * Time.deltaTime;
            rend.color = new Color(r, g, b, currentAlpha);
            yield return null;
        }
        rend.color = new Color(r, g, b, 0);
        count++;
        if (count < NumberOfFlashes)
            StartCoroutine(Flashing());
        else
            count = 0;
    }
}
