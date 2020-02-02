using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    [SerializeField] Vector3 axis = Vector3.up;
    [SerializeField] float speed = 1f;

    [SerializeField] bool global = false;

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        transform.Rotate(axis, speed * dt, (global)? Space.World:Space.Self);
    }
}
