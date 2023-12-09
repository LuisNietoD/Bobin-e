using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringSimulation : MonoBehaviour
{
    public MeshMorphing spring;
    public SpringJoint joint;
    public float maxDistance;
    public float minDistance;
    public float springValue;

    private void Start()
    {
        joint = GetComponent<SpringJoint>();
    }

    private void Update()
    {
        if (joint != null && joint.connectedBody != null)
        {
            // Calculate the current length of the spring
            
            float currentLength = Vector3.Distance(transform.position, joint.connectedBody.transform.position);
            if (currentLength <= maxDistance)
            {
                springValue = currentLength / maxDistance * 0.4f;
            }
            else
            {
                springValue = 0.4f;
            }

            if (transform.position.y < joint.connectedBody.transform.position.y)
                springValue = 0;

            spring.m_MorphTargets[0].Weight = springValue;
            // Print or use the length as needed
        }
    }

    /* public float amplitude = 0.3f; 
     public float frequency = 1.0f;  
     public float damping = 0.1f;
     public float retractSpeed = 0.1f;
     
     public MeshMorphing spring;
     public Movement movementRef;
     
     private float initialPosition;
     private float t = 0.0f; 
     private void Start()
     {
         initialPosition = 0.12f;
     }
     
     private void Update()
     {
         float x = amplitude * Mathf.Exp(-damping * t) * Mathf.Sin(2 * Mathf.PI * frequency * t);
         float newPosition = spring.m_MorphTargets[0].Weight;
         
         RaycastHit hit;
         if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
         {
             // Adjust the position based on the collision point
             if (hit.distance * 0.4f < newPosition)
             {
                 newPosition = hit.point.y;
                 
             }
             else
             {
                 newPosition = initialPosition + x;
             }
         }
         spring.m_MorphTargets[0].Weight = Mathf.Clamp(newPosition, 0, 0.4f);
 
         t += Time.deltaTime;
     }*/
}
