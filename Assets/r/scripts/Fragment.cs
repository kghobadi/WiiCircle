using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rick {

public class Fragment : MonoBehaviour
{
    new Rigidbody rigidbody;

    Vector3 position;
    Quaternion rotation;


    bool m_active = true;
    public bool active {
        get{
            return m_active;
        }
        set{
            m_active = value;

            rigidbody.useGravity = !value;
            rigidbody.isKinematic = value;
        }
    }

    static float moveSpeed = 10f;
    static float turnSpeed = 180f;


    void Awake() {
        rigidbody = GetComponent<Rigidbody>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        position = transform.localPosition;
        rotation = transform.localRotation;

        active = true;
    }

    void Update() {
        if(active){
            float dt = Time.deltaTime;

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, position, moveSpeed * dt);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotation, turnSpeed * dt);
        }
    }
}

}
