using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carPhysics : MonoBehaviour
{
    public Vector3 globalVelocity;
    public Vector3 localVelocity;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //We shift perspective to the local space
        localVelocity = GlobalToLocalVelocity(globalVelocity);

        //Debug purposes
        localVelocity += new Vector3(10,0,0) * Time.deltaTime;

        //We return to the global coordinate system to apply movement
        globalVelocity = LocalToGlobalVelocity(localVelocity);

        //Simple movement equation - Debug purposes
        transform.position += globalVelocity * Time.deltaTime;
    }

    Vector3 GlobalToLocalVelocity(Vector3 globalVel)
    {
        /*
         * We use the angle between the local z axis and the global vector to compute the Local velocity vector.
         */

        float A = Vector2.SignedAngle(new Vector2(transform.right.x, transform.right.z), new Vector2(globalVel.x, globalVel.z)) * Mathf.Deg2Rad;
        return new Vector3(globalVel.magnitude * Mathf.Cos(A), globalVel.y, globalVel.magnitude * Mathf.Sin(A));
    }

    Vector3 LocalToGlobalVelocity(Vector3 localVel)
    {
        /*
         * We find the angles between:
         * A --> the local velocity vector and the local z axis
         * B --> the local z azis and the world x axis
         * C --> the angle between the world velocity and the world x axis
         * 
         * With this last angle we can compute the Global velocity vector.
         */

        float A = Vector2.SignedAngle(new Vector2(localVel.x, localVel.z), new Vector2(0, 1));
        float B = 90 - transform.eulerAngles.y;
        float C = (B - A) * Mathf.Deg2Rad;
        return new Vector3(localVel.magnitude * Mathf.Cos(C), localVel.y, localVel.magnitude * Mathf.Sin(C));
    }
}
