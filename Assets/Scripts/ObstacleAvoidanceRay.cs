using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



/* Casts rays in a sphere, to check for collisions around the boid.
 This method for generating points on a sphere is based on Sebastian Lague's method which was derived from this post on points on a sphere https://stackoverflow.com/questions/9600801/evenly-distributing-n-points-on-a-sphere/44164075#44164075 */
public class ObstacleAvoidanceRay
{
    const int numViewDirections = 300;                                                  // Field of view of the rays
    public static Vector3[] directions;                                                 // Direction of the rays

    static ObstacleAvoidanceRay()
    {
        directions = new Vector3[ObstacleAvoidanceRay.numViewDirections];

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < numViewDirections; i++)
        {
            float t = (float)i / numViewDirections;
            float inclination = Mathf.Acos(1 - 2 * t);
            float pointOnSphereAngle = angleIncrement * i;                                         

            float x = Mathf.Sin(inclination) * Mathf.Cos(pointOnSphereAngle);
            float y = Mathf.Sin(inclination) * Mathf.Sin(pointOnSphereAngle);
            float z = Mathf.Cos(inclination);
            directions[i] = new Vector3(x, y, z);

        }
    }
}
