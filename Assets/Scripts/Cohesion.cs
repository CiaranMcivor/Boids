using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cohesion : SteeringBehaviour
{
    private Boid boid;
    private List<Boid> neighbours = new List<Boid>();
    private Vector3 averageVelocity;
    public float radius;
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

        if (neighbours.Count() > 0)                                                                     //If this boid has neighbours
        {
            foreach (Boid n in neighbours)                                                              // Loop through each
            {
                averageVelocity += (n.transform.position - boid.transform.position);                    //Add vector pointing towards the neighbour to average velocity

            }

            averageVelocity /= neighbours.Count();                                                      // Find the average from all neighbours

        }

        // If boid has no neighbours return zero vector, has no effect on the cooperative arbitration process.
        else
        {
            averageVelocity = Vector3.zero;
        }


        Vector3.Normalize(averageVelocity);
        return averageVelocity;
    }


}