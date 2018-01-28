using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public ScoreTable[] Tables;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeNumber(int n)
    {
        foreach(ScoreTable s in Tables)
        {
            s.ChangeNumber(0);
        }
        for(int i = 0; n > 0; i++)
        {
            Tables[i].ChangeNumber(n % 10);
            n = n / 10;
        }
    }


}
