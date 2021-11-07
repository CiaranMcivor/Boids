using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Alignment : SteeringBehaviour
{
    private Boid boid;
    private List<Boid> neighbours = new List<Boid>();
    private Vector3 averageVelocity;
    public float deviation;
    // Start is called before the first frame update
    void Start()
    {
        boid = GetComponent<Boid>();
        neighbours = boid.getNeighbours();
    }

    public override Vector3 updateVelocity()
    {

        try
        {
           neighbours = boid.getNeighbours();
        }

        catch
        {

        }

        averageVelocity = Vector3.zero;

        if (neighbours.Count() > 0)                                                             // if theis boid has neighbours
        {
            foreach (Boid n in neighbours)                                                      //Loop through neighbours
            {
                averageVelocity += n.getVelocity();                                             //Add each neighbours velocities together
            }

            averageVelocity /= neighbours.Count();                                              // Find the average of the vector, i.e the average velocity of the surrounding boids
                
        }

        //If boid has no neighbours do no calculation
        else
        {
            averageVelocity = Vector3.zero;
        }
        Vector3.Normalize(averageVelocity);
        return averageVelocity;
    }

}
