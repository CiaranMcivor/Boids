using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    [SerializeField] private float steeringWeight;


    // This is used as an override in each separate behaviour. This allows the vector calculation in Cooperatvie Arbitration to be done via for loop, as opposed to adding each named behaviour separately.
    public abstract Vector3 updateVelocity();
    public float getSteeringWeight() { return steeringWeight; }

}
