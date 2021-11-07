using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Diagnostics;
public class Boid : MonoBehaviour
{
    /*VARIABLES*/
    [SerializeField] Vector3 currentVelocity;                                                                   // Current Velocity of this Boid objext                                   
    [SerializeField] public float neighbourRadius;                                                              // Radius in which to check for neighbours
    private Boid boid;                                                                                          // This boid
    [SerializeField] private List<Boid> allBoids = new List<Boid>();                                            // All boids
    [SerializeField] private List<Boid> neighbours = new List<Boid>();                                          // Neighbours
    [SerializeField] private List<SteeringBehaviour> steeringBehaviours = new List<SteeringBehaviour>();        // All Steering Behaviours
    [SerializeField] public float maxSpeed = 10;                                                                // Maximum desired speed
    [SerializeField] private float maxSteering = 10.0f;                                                         // Max possible steering 



    // Start is called before the first frame update
    void Start()
    {
        // Get this boid and remove it from the list of all boids, so it is not considered for vector calculations.
        boid = this;
        allBoids = FindObjectsOfType<Boid>().ToList();
        allBoids.Remove(boid);

        //Get all steering behavoiur scripts attached to this object
        GetComponents<SteeringBehaviour>(steeringBehaviours);
        //currentVelocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
    }

    private void move()
    {
        this.transform.position += currentVelocity * Time.deltaTime;

    }

    void rotate()
    {
        // Make this objects forward vector equal to its current heading.
        Vector3 velocityUnit = currentVelocity /currentVelocity.magnitude;           // Find unit vector.
        this.transform.forward = velocityUnit;
    }

    void findNeighbours()
    {
        neighbours.Clear();                                                                                         //Clear the List to avaoid duplicate entries.

        foreach (Boid boid in allBoids)
        {
            if (Vector3.Distance(this.transform.position, boid.transform.position) < neighbourRadius)               //If current element is within radius
            {
                neighbours.Add(boid);                                                                               //Add to neigbours list
            }
        }

        //Debug.Log(neighbours);
    }

    public List<Boid> getNeighbours()
    {
        return neighbours;
    }

    void cooperativeArbitration()
    {
        /*This method is based on the method used in the steering behaviours tutorial*/


        // Create a blank slate from which to calculate the new velocity
        Vector3 steeringVelocity = Vector3.zero;

        //Loop through all behaviours
        foreach (SteeringBehaviour behaviour in steeringBehaviours)
        {
            if (behaviour.enabled)
            {
                //Add the calculated velocity from each steeringbehaviour muliplied by the weight of the relevant behaviour.
                steeringVelocity += (behaviour.updateVelocity() * behaviour.getSteeringWeight());

            }
        }

        //Limit the amount of steering that can be applied to the current velocity and add it.
        currentVelocity += limitSteering(steeringVelocity, maxSteering);
        // Limit velocity to max speed.
        currentVelocity = limitVelocity(currentVelocity, maxSpeed);

    }


    // Update is called once per frame
    void Update()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        findNeighbours();
        cooperativeArbitration();        
        move();
        rotate();

        sw.Stop();
        UnityEngine.Debug.Log(sw.Elapsed.TotalMilliseconds);
    }



    Vector3 limitVelocity(Vector3 currentVelocity, float maxSpeed)
    {
        // This limits the velocity to max speed. 
        if (currentVelocity.magnitude > maxSpeed)
        {
            currentVelocity.Normalize();
            currentVelocity *= maxSpeed;
        }

        return currentVelocity;
    }

    static public Vector3 limitSteering(Vector3 currentVelocity, float maxSteering)
    {
        // This limits the velocity to max steering. 
 
        if (currentVelocity.magnitude > maxSteering)
        {
            currentVelocity.Normalize();
            currentVelocity *= maxSteering;
        }
        return currentVelocity;
    }

    public Vector3 getVelocity()
    {
        return currentVelocity;
    }
}
