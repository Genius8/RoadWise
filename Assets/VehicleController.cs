using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float maxSpeed = 10f;
    public float acceleration = 2f;
    public float brakingForce = 5f;
    public float steeringAngle = 15f;

    [Header("Detection Settings")]
    public float detectionDistance = 10f;
    public LayerMask vehicleLayer;

    private float currentSpeed = 0f;

    private void Update()
    {
        RaycastHit hit;

        // Raycast directly ahead from the front of the vehicle
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        Vector3 rayDirection = transform.forward;

        bool obstacleDetected = Physics.Raycast(rayOrigin, rayDirection, out hit, detectionDistance, vehicleLayer);

        if (obstacleDetected)
        {
            float distanceToObstacle = hit.distance;

            // Smooth deceleration
            float targetSpeed = Mathf.Lerp(0f, maxSpeed, distanceToObstacle / detectionDistance);
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, brakingForce * Time.deltaTime);

            // Try to steer slightly to the side
            Vector3 obstaclePos = hit.collider.transform.position;
            Vector3 offset = obstaclePos - transform.position;

            float steerDirection = Vector3.Dot(transform.right, offset) > 0 ? -1f : 1f; // steer away

            transform.Rotate(0, steerDirection * steeringAngle * Time.deltaTime, 0);
        }
        else
        {
            // Smooth acceleration to maxSpeed
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }

        // Move the vehicle forward
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize raycast in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f + transform.forward * detectionDistance);
    }
}
