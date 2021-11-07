using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Separation : SteeringBehaviour
{
    private Boid boid;
    private List<Boid> neighbours = new List<Boid>();
    private Vector3 averageVelocity;
    [SerializeField]private float desiredDistance;
    [SerializeField] private float force;
    // Start is called before the first frame update
    void Start()
    {
        boid = GetComponent<Boid>();
        neighbours = boid.getNeighbours();

    }

    public override Vector3 updateVelocity()
    {
        // Error handling if no neighbours
        try
        {
            neighbours = boid.getNeighbours();
        }

        catch
        {

        }

        averageVelocity = Vector3.zero;

        if (neighbours.Count() > 0)                                                                             //If this boid has any neighbours
        {
            foreach (Boid n in neighbours)                                                                      //Loop through this Boids neighbours
            {
                if (Vector3.Distance(n.transform.position, boid.transform.position) <= desiredDistance)          // Check if within distance
                {
                    averageVelocity -= (n.transform.position - boid.transform.position);                        //Calculate the vector that points away from the other boid.                                 
                }             
            }
            averageVelocity /= neighbours.Count();                                                              // Find the new heading based on the average of all neighbours.
        }

        //If no neighbours do no calculation
        else
        {
            averageVelocity = Vector3.zero; 
        }

        Vector3.Normalize(averageVelocity);
        return averageVelocity;
    }

    // Update is called once per frame

}
