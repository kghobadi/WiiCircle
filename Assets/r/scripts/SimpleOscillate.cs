using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOscillate : MonoBehaviour
{

    [SerializeField] Vector3 direction = Vector3.up;
    [SerializeField] float speed = 1f;
    [SerializeField] float distance = 1f;

    [SerializeField] bool randomSpeed = false, randomDistance = false;

    Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {   
        origin = transform.position;

        if(randomSpeed) speed *= Random.Range(0f, 1f);
        if(randomDistance) distance *= Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * (distance / 2f);
        transform.position = origin + (direction * offset);
    }
}
