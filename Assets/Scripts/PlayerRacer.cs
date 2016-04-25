using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerRacer : MonoBehaviour {


    public GameObject waypointContainer;
    public List<Transform> waypoints;
    public int currentWaypoint = 0;
    Vector3 currentWaypointPos;
    public float distanceTravelled = 0;
    Vector3 lastPosition;
    public float distanceTo;


    void Start()
    {
        GetWaypoints();
        currentWaypointPos = waypoints[currentWaypoint].position;
        lastPosition = transform.position;
    }
    void Update()
    {
        if (currentWaypoint == waypoints.Count)
        {
            currentWaypoint = 0;
        }
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        distanceTo = Vector3.Distance(waypoints[currentWaypoint].position, transform.position);
    }
    
    void GetWaypoints()
    {
        Transform[] potentialWaypoints = waypointContainer.GetComponentsInChildren<Transform>();
        waypoints = new List<Transform>();

        foreach (Transform potentialWaypoint in potentialWaypoints)
        {
            if (potentialWaypoint != waypointContainer.transform)
            {
                waypoints.Add(potentialWaypoint);
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.name.Contains("Waypoint"))
        {
            currentWaypoint++;
            if (currentWaypoint == waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }
        }
    }

