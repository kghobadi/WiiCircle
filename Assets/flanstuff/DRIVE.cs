using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRIVE : MonoBehaviour
{
    ParticleSystem sparksL;
    ParticleSystem sparksR;


    public float camSpeed;

    public float acc;
    public float accRate;
    public float speed;
    public float backspeed;
    public float backAcc;
    public float backAccRate;

    public float turnspeed;
    public float driftSpeed;
    public float topspeed;
    public float backTopSpeed;
    public float dec;

    float turningSpeedtouse;

    public bool SparksR;
    public bool SparksL;



    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        acc = 0;
        backspeed = 0;
        backAcc = 0;

        //sparksL = transform.FindChild("DriftSparksL").GetComponent<ParticleSystem>();
        //sparksR = transform.FindChild("DriftSparksR").GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        //moving 
        transform.localPosition += transform.forward * speed;
        Camera.main.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 2,
        transform.localPosition.z - 4);


        //acceleration 
        if (Input.GetKey(KeyCode.Return))
        {
            speed += acc;
            acc += accRate;

        }
        else
        {
            speed -= dec;
        }
        //reverse 
        if (Input.GetKey(KeyCode.S) && (!Input.GetKey(KeyCode.Return)))
        {
            backspeed += backAcc;
            backAcc += backAccRate;
            transform.localPosition += -transform.forward * backspeed;

        }
        else
        {
            backspeed -= dec;
        }
        if (backspeed <= 0)
        {
            backspeed = 0;
        }
        if (backspeed >= backTopSpeed)
        {
            backspeed = backTopSpeed;
        }

        if (speed <= 0)
        {
            speed = 0;
        }
        if (speed >= topspeed)
        {
            speed = topspeed;
        }

        //drift
        if (Input.GetKey(KeyCode.Semicolon))
        {
            turningSpeedtouse = driftSpeed;

            if (speed > 0)
            {
                accRate = -accRate;
            }

        }
        else
        {
            turningSpeedtouse = turnspeed;

        }

        //steering 
        if ((Input.GetKey(KeyCode.A) && ((speed > 0) || (backspeed > 0))))
        {
            transform.localEulerAngles += new Vector3(0, -turningSpeedtouse, 0);


        }
        if ((Input.GetKey(KeyCode.D) && ((speed > 0) || (backspeed > 0))))
        {
            transform.localEulerAngles += new Vector3(0, turningSpeedtouse, 0);
        }

        //sparks 

        if ((Input.GetKey(KeyCode.Semicolon)) && (Input.GetKey(KeyCode.A)) && speed > 0)
        {
            sparksR.Play();
            SparksR = true;
        }
        else
        {
            sparksR.Stop();
            SparksR = false;
        }
        if ((Input.GetKey(KeyCode.Semicolon)) && (Input.GetKey(KeyCode.D)) && speed > 0)
        {
            sparksL.Play();
            SparksL = true;
        }
        else
        {
            sparksL.Stop();
            SparksL = false;
        }
    }
    /*
    void FixedUpdate()
    {


        foreach (GameObject newDerb in arena.carsList)
        {

            if (newDerb != null)
            {
                Renderer circleRender = newDerb.transform.FindChild("circle").GetComponent<SpriteRenderer>();
                float dist = Vector3.Distance(newDerb.transform.position, transform.position);

                if (dist < 10)
                {

                    circleRender.enabled = true;
                }
                else if (dist > 10)
                {
                    circleRender.enabled = false;
                }
            }
        }
    }
    */
}