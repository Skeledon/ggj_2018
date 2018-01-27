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

    public int Player;

    public GameObject Bullet;

    private Vector2 currentVelocity;
    private bool isBraking;
    private bool canShoot = true;
    private bool isTrusting = false;
    private bool turningCoroutinRunning = false;
    private ParticleSystem myParticle;

    private InputCarrier.ShipOrder currentOrder = InputCarrier.ShipOrder.NoOrder;
    private float currentOrderTime;

    private Animator myAnimator;

    float currentRotation = 0;
    float targetRotation;


    Transform t;
    Rigidbody2D myRigidbody;
    // Use this for initialization

    private void Awake()
    {
        t = transform;
        myRigidbody = GetComponent<Rigidbody2D>();
        myParticle = GetComponentInChildren<ParticleSystem>();
        myAnimator = GetComponent<Animator>();
    }
    void Start ()
    {
        myParticle.Stop();
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
        }
    }

    public void Accelerate()
    {
        AddAcceleration(Acceleration);
        if(!myParticle.isPlaying)
        {
            myParticle.Play();
        }
        isTrusting = true;
    }

    public void LateUpdate()
    {
        if (!isTrusting)
            myParticle.Stop();
        isTrusting = false;
    }
    /// <summary>
    /// Turns the Spaceship
    /// </summary>
    /// <param name="direction">positive sx, negative dx</param>
    public void Turn(float direction) 
    {
        float rotateValue = TurningSpeed * Time.deltaTime;
        t.Rotate(Vector3.forward, rotateValue * direction, Space.Self);
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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        myAnimator.SetTrigger("Dolore");
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

}
