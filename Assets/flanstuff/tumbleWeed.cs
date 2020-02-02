using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tumbleWeed : MonoBehaviour
{

    int speed;
    Vector3 tumblingDir;

    float weedTimer; 
    // Start is called before the first frame update
    void Start()
    {
        tumblingDir = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        speed = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        weedTimer += Time.deltaTime;

        transform.position += tumblingDir * Time.deltaTime * speed; 

        if (weedTimer>= 3)
        {
            tumblingDir = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            speed = Random.Range(100, 500);
        }
    }
}
