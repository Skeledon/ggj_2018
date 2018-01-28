using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
    public Sprite[] Numbers;
    SpriteRenderer myRenderer;

	// Use this for initialization
	void Awake ()
    {
        myRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeNumber(int n)
    {
        myRenderer.sprite = Numbers[n];
    }
}
