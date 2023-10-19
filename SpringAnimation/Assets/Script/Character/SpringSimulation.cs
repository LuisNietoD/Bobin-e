using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringSimulation : MonoBehaviour
{
    public float amplitude = 0.3f; 
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

            spring.m_MorphTargets[0].Weight = Mathf.Clamp(initialPosition + x, 0, 0.4f);

            t += Time.deltaTime;
    }
}
