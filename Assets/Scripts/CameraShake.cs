using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeDuration;
    public float ShakeAmplitude;
    public float ShakeFrequency;

	// Use this for initialization
	void Start ()
    {
        GameHandler.nextGameStep += StartShaking;
	}

    private void OnDestroy()
    {
        GameHandler.nextGameStep -= StartShaking;
    }
    // Update is called once per frame
    void Update ()
    {
		
	}

    void StartShaking()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float currentTime = 0;
        int direction = 1;
        Vector3 startPosition = transform.position;
        while(currentTime < ShakeDuration)
        {
            transform.position = startPosition + Vector3.right * ShakeAmplitude * direction;
            direction *= -1;
            currentTime += 1 / ShakeFrequency;
            yield return new WaitForSeconds(1 / ShakeFrequency);
        }

        transform.position = startPosition;
    }
}
