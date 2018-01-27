using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float InitialForce;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * InitialForce,ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.Translate(Vector2.up * Speed * Time.deltaTime);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DestryoAfterFrame());
    }

    IEnumerator DestryoAfterFrame()
    {
        yield return new WaitForSeconds(0);
        Destroy(gameObject);
    }
}
