using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class CarAI : MonoBehaviour
{
    public WaypointRoute route;
    public float speed = 10f;
    public float turnSpeed = 5f;
    public float stopDistance = 5f;
    public float detectionRange = 10f;

    private int currentWaypointIndex = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (route == null || route.waypoints.Count == 0) return;

        Transform target = route.waypoints[currentWaypointIndex];

        // Look at and move toward the waypoint
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 move = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // Smooth rotation
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime));

        // Switch to next waypoint if close
        if (Vector3.Distance(transform.position, target.position) < stopDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % route.waypoints.Count;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.forward * 2, transform.forward, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Car")) // Tag all AI cars as "Car"
            {
                // Stop or slow down
                rb.velocity = Vector3.zero;
                return;
            }
        }
    }
}
