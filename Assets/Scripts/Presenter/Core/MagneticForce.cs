using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Presenter.Core
{
    public static class MagneticForce
    {
        public static void Apply(Transform object1, Transform object2, float magneticForce)
        {
            // Calculate the direction between the two objects
            Vector3 forceDirection = object2.position - object1.position;

            // Calculate the distance between the objects
            float distance = forceDirection.magnitude;

            // Normalize the direction vector
            forceDirection.Normalize();

            // Calculate the magnetic force based on distance
            float forceMagnitude = magneticForce / Mathf.Pow(distance, 2f);

            // Move the objects towards each other
            object1.position += forceDirection * forceMagnitude;
            object2.position -= forceDirection * forceMagnitude;
        }
    }
}