using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float MaxSpeed;
    public float TurningSpeed;
    public float Acceleration;
    public float Braking;
    public float TimeBetweenShots;

    public GameObject RightFlame;
    public GameObject LeftFlame;

    public AudioClip Dolore;
    public AudioClip Happy;
    public AudioClip Shot;

    public int Player;

    public GameObject Bullet;

    public string FireColorChange;
    public string FireColorStart;

    private Vector2 currentVelocity;
    private bool isBraking;
    private bool canShoot = true;
    private bool isTrusting = false;
    private bool turningCoroutinRunning = false;

    private InputCarrier.ShipOrder currentOrder = InputCarrier.ShipOrder.NoOrder;
    private float currentOrderTime;

    private Animator myAnimator;

    float currentRotation = 0;
    float targetRotation;

    private GameObject trail;

    private bool isTurning;

    private int currentStep = 0;


    Transform t;
    Rigidbody2D myRigidbody;
    // Use this for initialization

    private void Awake()
    {
        t = transform;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        trail = transform.Find("Trail").gameObject;
        GameHandler.nextGameStep += NextGameStep;
    }
    void Start ()
    {
        trail.GetComponent<SpriteRenderer>().enabled = false;
        trail.GetComponent<Animator>().SetTrigger(FireColorStart);
        RightFlame.GetComponent<Animator>().SetTrigger(FireColorStart);
        LeftFlame.GetComponent<Animator>().SetTrigger(FireColorStart);
    }

    private void Update()
    {
        /*if(currentOrder != InputCarrier.ShipOrder.NoOrder)
        {
            ExectueOrder();
        }    
        currentOrderTime += Time.deltaTime;*/
    }

    #region delayedinputs
    private void ExectueOrder()
    {
        switch(currentOrder)
        {
            case InputCarrier.ShipOrder.Accelerate:
                Accelerate();
                if (currentOrderTime >= .3f)
                {
                    currentOrder = 0;
                    currentOrder = InputCarrier.ShipOrder.NoOrder;
                }
                break;
            case InputCarrier.ShipOrder.Shoot:
                Shoot();
                if (currentOrderTime >= .3f)
                {
                    currentOrder = 0;
                    currentOrder = InputCarrier.ShipOrder.NoOrder;
                }
                break;
            case InputCarrier.ShipOrder.TurnLeft:
                if (!turningCoroutinRunning)
                {
                    StartCoroutine(TurnCoroutine(1));
                }
                else
                {
                    targetRotation += 45;
                }
                currentOrder = 0;
                currentOrder = InputCarrier.ShipOrder.NoOrder;
                break;
            case InputCarrier.ShipOrder.TurnRight:
                if (!turningCoroutinRunning)
                {
                    StartCoroutine(TurnCoroutine(-1));
                }
                else
                {
                    targetRotation -= 45;
                }
                currentOrder = 0;
                currentOrder = InputCarrier.ShipOrder.NoOrder;
                break;
            default:
                break;
        }
 
    }
    #endregion

    public void Shoot()
    {
        if(canShoot)
        {
            ShootBullet();
            canShoot = false;
            StartCoroutine(ShotCooldown());
            myAnimator.SetTrigger("Sparo");
            GetComponent<AudioSource>().PlayOneShot(Shot);
        }
    }

    public void Accelerate()
    {
        AddAcceleration(Acceleration);
        trail.GetComponent<SpriteRenderer>().enabled = true;
        if(!trail.GetComponent<AudioSource>().isPlaying)
            trail.GetComponent<AudioSource>().Play();
        isTrusting = true;
    }

    public void LateUpdate()
    {
        if (!isTrusting)
        {
            trail.GetComponent<SpriteRenderer>().enabled = false;
            trail.GetComponent<AudioSource>().Stop();
        }
        if (!isTurning)
        {
            RightFlame.GetComponent<SpriteRenderer>().enabled = false;
            LeftFlame.GetComponent<SpriteRenderer>().enabled = false;
            RightFlame.GetComponent<AudioSource>().Stop();
            LeftFlame.GetComponent<AudioSource>().Stop(); 
        }
        isTrusting = false;
        isTurning = false;
    }
    /// <summary>
    /// Turns the Spaceship
    /// </summary>
    /// <param name="direction">positive sx, negative dx</param>
    public void Turn(float direction) 
    {
        float rotateValue = TurningSpeed * Time.deltaTime;
        t.Rotate(Vector3.forward, rotateValue * direction, Space.Self);
        if(direction > 0)
        {
            RightFlame.GetComponent<SpriteRenderer>().enabled = true;
            if (!RightFlame.GetComponent<AudioSource>().isPlaying)
                RightFlame.GetComponent<AudioSource>().Play();
        }
        else
        {
            LeftFlame.GetComponent<SpriteRenderer>().enabled = true;
            if (!LeftFlame.GetComponent<AudioSource>().isPlaying)
                LeftFlame.GetComponent<AudioSource>().Play();
        }
        isTurning = true;

    }


    private void AddAcceleration(float accelerationValue)
    {
        myRigidbody.AddForce(t.up * accelerationValue);
        if(myRigidbody.velocity.magnitude >= MaxSpeed)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * MaxSpeed;
        }
    }

    private void ShootBullet()
    {
        GameObject.Instantiate(Bullet, t.position + t.up * 1.2f, t.rotation);
    }

    private IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(TimeBetweenShots);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("InputCarrier") && collision.GetComponent<InputCarrier>().player == Player)
        {
            currentOrderTime = 0;
            currentOrder = collision.GetComponent<InputCarrier>().CurrentOrder;
        }
        if(collision.CompareTag("Pickup"))
        {
            myAnimator.SetTrigger("Happy");
            GetComponent<AudioSource>().PlayOneShot(Happy);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        myAnimator.SetTrigger("Dolore");
        GetComponent<AudioSource>().PlayOneShot(Dolore);
    }

    IEnumerator TurnCoroutine(int t)
    {
        turningCoroutinRunning = true;
        Debug.Log("started");
        float currentAngle = transform.rotation.eulerAngles.z;
        targetRotation = currentAngle + 45 * t;
        while(Mathf.Abs(currentAngle - targetRotation) >= .2f)
        {
            if(currentAngle > targetRotation)
            {
                t = -1;
            }
            else
            {
                t = 1;
            }
            Turn(t);
            currentRotation += TurningSpeed * Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        turningCoroutinRunning = false;

    }

    int nearest45(float n)
    {
        for(int i = 0; i <= 360; i+=45)
        {
            Debug.Log("n = " + n + "    i = " + i);
            if(Mathf.Abs(n-i) <= 22.5)
            {
                return i;
            }
        }
        return 0;
    }

    private void NextGameStep()
    {
        currentStep++;
        if (currentStep == 1)
            RightFlame.GetComponent<Animator>().SetTrigger(FireColorChange);
        if (currentStep == 2)
            trail.GetComponent<Animator>().SetTrigger(FireColorChange);
    }

}
