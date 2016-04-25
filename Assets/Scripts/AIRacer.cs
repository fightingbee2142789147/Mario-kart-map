using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AIRacer : MonoBehaviour
{

    public GameObject waypointContainer;
    public List<Transform> waypoints;
    public int currentWaypoint = 0;
    private float inputSteer = 0.0f;
    private float inputTorque = 0.0f;
    public Rigidbody rb;
    public Vector3 currentWaypointPos;
    public float speed = 0.1f;
    public NavMeshAgent navmesh;
    public int LapNumber = 0;
    public Countdown count;
    public float distanceTo;
    public GameObject greenShell;

    void Start()
    {
        GetWaypoints();
        rb = GetComponent<Rigidbody>();
        currentWaypointPos = new Vector3(waypoints[0].position.x + Random.Range(-3, 3), waypoints[0].position.y, waypoints[0].position.z + Random.Range(-3, 3));
        navmesh = GetComponent<NavMeshAgent>();
        count = GameObject.Find("CutsceneManager").GetComponent<Countdown>();
    }
    void Update()
    {

        RaycastHit Hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.right, out Hit, 4) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), -transform.right, out Hit, 4) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.forward, out Hit, 4))
        {
            if (Hit.collider.gameObject.tag == "Bounds")
            {
                currentWaypointPos = new Vector3(waypoints[currentWaypoint].position.x + Random.Range(-3, 3), waypoints[currentWaypoint].position.y, waypoints[currentWaypoint].position.z + Random.Range(-3, 3));
            }
            if (Hit.collider.gameObject.tag == "AI")
            {
                currentWaypointPos = new Vector3(waypoints[currentWaypoint].position.x + Random.Range(-3, 3), waypoints[currentWaypoint].position.y, waypoints[currentWaypoint].position.z + Random.Range(-3, 3));
            }
        }

        if (count.canStart == true)
        {
            navmesh.speed = 3.5f;
            NavigateTowardsWaypoint();
            if (speed >= 25)
            {
                speed = 25;
            }
            else if (speed < 0)
            {
                speed = 0;
            }
            if (currentWaypoint == waypoints.Count)
            {
                currentWaypoint = 0;
                navmesh.SetDestination(waypoints[0].position);
                currentWaypointPos = new Vector3(waypoints[0].position.x + Random.Range(-3, 3), waypoints[0].position.y, waypoints[0].position.z + Random.Range(-3, 3));
            }
            distanceTo = Vector3.Distance(waypoints[currentWaypoint].position, transform.position);
        }
        else
        {
            navmesh.speed = 0;
        }
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
    void NavigateTowardsWaypoint()
    {
        speed += Time.deltaTime * 5;
        navmesh.SetDestination(currentWaypointPos);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Shell")
        {
            Destroy(col.gameObject);
            speed = 0;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Waypoint")
        {
            currentWaypoint++;
            NavigateTowardsWaypoint();
            currentWaypointPos = new Vector3(waypoints[currentWaypoint].position.x + Random.Range(-3, 3), waypoints[currentWaypoint].position.y, waypoints[currentWaypoint].position.z + Random.Range(-3, 3));
        }
        if (col.gameObject.tag == "Finish")
        {
            LapNumber++;
        }
    }
}
