using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//for moving mario's horse on nav mesh
public class MoveOnNavMesh : MonoBehaviour
{
    NavMeshAgent nma;

    public Transform[] waypoints;
    public int currentPoint = -1;
    Vector3 currentDestination;
    float currentDist;

    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        SetDestination(); 
    }

    void Update()
    {
        currentDist = Vector3.Distance(transform.position, currentDestination);

        if(currentDist < nma.stoppingDistance)
        {
            SetDestination();
        }
    }

    public void SetDestination()
    {
        if(currentPoint < waypoints.Length - 1)
        {
            currentPoint++;
            nma.SetDestination(waypoints[currentPoint].position);
        }
        else
        {
            Debug.Log("reached final point");
        }
    }
}
