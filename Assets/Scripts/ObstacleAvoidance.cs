using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : SteeringBehaviour
{

    Boid boid;
    private Vector3 averageVelocity;
    public LayerMask obstacleLayer;
    public float raySphereRadius = .27f;
    public float collisionDistance = 1;
    private void Start()
    {
        boid = GetComponent<Boid>();

    }

    //Bool to check if object will hit something in future using a raycast.
    bool IsHeadingForCollision()
    {
        RaycastHit hit;                 // Ouput from raycast

        /*Cast a sphere (think of it as a thicker raycast) out from the forward vector of the boid, for a distance of collisionDistance.
        Obstacle layer tells the ray the only return a hit if the object is on that layer, else it will be ignored. */
        if (Physics.SphereCast(boid.transform.position, raySphereRadius, boid.transform.forward, out hit, collisionDistance, obstacleLayer))
        {
            return true;
        }
        else 
        { 
            return false;
        }

    }

    /*This function finds the avoidance direction by looking for a raycast which returns false, therefore having no obstacle
     * in that direction. It Uses the calculations in the OBstacleAvoidanceRay class to get all the directions to check.*/
    Vector3 AvoidanceRay()
    {
        Vector3[] avoidanceRays = ObstacleAvoidanceRay.directions;                              // Vector array which holds the vector positions of where to shoot out each ray. 

        for (int i = 0; i < avoidanceRays.Length; i++)                                          // Check each direction until all are checked and one returns a clear direction.
        {
            Vector3 direction = boid.transform.TransformDirection(avoidanceRays[i]);
            Ray ray = new Ray(boid.transform.position, direction);
            if (!Physics.SphereCast(ray, raySphereRadius, collisionDistance, obstacleLayer))    // If the raycast returns false this direction is safe to move.
            {
                return direction;
            }
        }
        
        return boid.transform.forward;
    }

    public override Vector3 updateVelocity()
    {
        if (IsHeadingForCollision())                                // if the boid will collide with something in the near future.
        {
            Vector3 avoidanceVelocity = AvoidanceRay();             // Find a direction in which it will not collide with anything.
            averageVelocity = avoidanceVelocity.normalized;         // Set average velocity to this avoidance velocity
        }

        //If not heading for collision do nothing
        else
        {
            averageVelocity = Vector3.zero;
        }
        //Debug.Log(IsHeadingForCollision());
        Vector3.Normalize(averageVelocity);
        return averageVelocity;
    }

}
