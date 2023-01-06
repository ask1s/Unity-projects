using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField]
    private FollowPlayer camera;
    public float step = 20;
    public float speed = 3000;
    public float jump = 4000;
    public float jumpCool = 1.2f;
    public float trampPower = 350;
    public float mass = 10;

    private bool strafeRight = false;
    private bool strafeLeft = false;
    private bool doJump = false;

    private float lastJump = 0;

    public bool isDead = false;

    private float jumpDuration = 0;

    private int passiveSpeedIncrease = 600;
    private int speedTimer = 0;

    private Vector3 forwardForce;
    private Vector3 leftForce;
    private Vector3 rightForce;

    private int direction = 1;

    private float lastSpeedIncrease = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "JumppadLeft")
        {
            rb.AddForce(rightForce * trampPower, ForceMode.Impulse);
        }
        if (collision.collider.tag == "JumppadRight")
        {
            rb.AddForce(leftForce * trampPower, ForceMode.Impulse);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TurnLeft")
        {
            Rotate(false);
            other.gameObject.SetActive(false);
        }
        if (other.tag == "TurnRight")
        {
            Rotate(true);
            other.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        forwardForce.x = 0;
        forwardForce.y = 0;
        forwardForce.z = speed * Time.deltaTime;

        leftForce.x = -step * Time.deltaTime;
        leftForce.y = 0;
        leftForce.z = 0;

        rightForce.x = step * Time.deltaTime;
        rightForce.y = 0;
        rightForce.z = 0;
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKey("d"))
            {
                strafeRight = true;
            }
            else
            {
                strafeRight = false;
            }

            if (Input.GetKey("a"))
            {
                strafeLeft = true;
            }
            else
            {
                strafeLeft = false;
            }

            if (Input.GetKeyDown("space"))
            {
                if (Time.time - lastJump >= jumpCool)
                {
                    doJump = true;
                    lastJump = Time.time;
                }
            }

            if (Time.time - lastSpeedIncrease >= 600)
            {
                speed += 25;
                lastSpeedIncrease = Time.time;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            rb.AddForce(forwardForce);
            if (strafeLeft)
            {
                rb.AddForce(leftForce, ForceMode.VelocityChange);
            }
            if (strafeRight)
            {
                rb.AddForce(rightForce, ForceMode.VelocityChange);
            }
            if (doJump)
            {
                rb.AddForce(0, jump * Time.deltaTime, 0, ForceMode.Impulse);
                doJump = false;
            }
        }
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public float GetJump()
    {
        return jump;
    }
    public void SetJump(float newJump, float duration)
    {
        jump = newJump;
        jumpDuration = duration;
    }

    public void Rotate(bool right)
    {
        if (right == true)
            direction++;
        else
            direction--;

        if (direction < 1)
            direction = 4;
        else if (direction > 4)
            direction = 1;

        camera.SetDir(direction);

        switch(direction)
        {
            case 1:
                forwardForce.x = 0;
                forwardForce.y = 0;
                forwardForce.z = speed * Time.deltaTime;

                leftForce.x = -step * Time.deltaTime;
                leftForce.y = 0;
                leftForce.z = 0;

                rightForce.x = step * Time.deltaTime;
                rightForce.y = 0;
                rightForce.z = 0;
                break;

            case 2:
                forwardForce.x = speed * Time.deltaTime;
                forwardForce.y = 0;
                forwardForce.z = 0;

                leftForce.x = 0;
                leftForce.y = 0;
                leftForce.z = step * Time.deltaTime;

                rightForce.x = 0;
                rightForce.y = 0;
                rightForce.z = -step * Time.deltaTime;
                break;

            case 3:
                forwardForce.x = 0;
                forwardForce.y = 0;
                forwardForce.z = -speed * Time.deltaTime;

                leftForce.x = step * Time.deltaTime;
                leftForce.y = 0;
                leftForce.z = 0;

                rightForce.x = -step * Time.deltaTime;
                rightForce.y = 0;
                rightForce.z = 0;
                break;

            case 4:
                forwardForce.x = -speed * Time.deltaTime;
                forwardForce.y = 0;
                forwardForce.z = 0;

                leftForce.x = 0;
                leftForce.y = 0;
                leftForce.z = -step * Time.deltaTime;

                rightForce.x = 0;
                rightForce.y = 0;
                rightForce.z = step * Time.deltaTime;
                break;
        }
    }

    public void KillPlayer()
    {
        this.isDead = true;
    }
}
