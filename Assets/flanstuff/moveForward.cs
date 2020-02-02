using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveForward : MonoBehaviour
{

    public int speed;
    public AudioSource whip; 
    public float whipTimer;

    // Start is called before the first frame update
    void Start()
    {
        whipTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= 10822)
        {
            Debug.Log("to julian's scene now");
        }

        if (whipTimer >= 1)
        {
            whipTimer = 1; 
        }

        transform.position += new Vector3(0, 0, 1) * Time.deltaTime * speed;

        if (whipTimer > 0)
        {
            speed = 350;
            whipTimer -= Time.deltaTime;
        }
        else if (whipTimer <= 0)
        {
            speed = 50; 
        }

        if (Input.GetMouseButton(0))
        {
            whipTimer += .5f;
            whip.Play();
        }
         
        
    }
}
