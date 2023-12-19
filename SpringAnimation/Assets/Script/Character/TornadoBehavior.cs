using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoBehavior : MonoBehaviour
{
    public float speed = 5;
    public float amplitude = 4;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public int right;
    private float t = 0.0f;
    public float reach = 10;

    private float scaleTime = 1.0f; // Adjust this to set the duration of scaling
    private float scaleTimer = 0.0f;
    private bool isScaling = false;

    private void Start()
    {
        startPoint = transform.position;
        endPoint = transform.position + transform.forward * reach;
        transform.localScale = Vector3.zero;
        isScaling = true;
    }

    private void FixedUpdate()
    {
        if (isScaling)
        {
            scaleTimer += Time.deltaTime;
            float t = Mathf.Clamp01(scaleTimer / scaleTime);
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            if (scaleTimer >= scaleTime)
            {
                isScaling = false;
                scaleTimer = 0.0f;
            }
        }
        
        t += Time.deltaTime * speed;
        if (t > 1.0f)
        {
            t = 1.0f;
        }

        Vector3 position = CalculateSineRight(t, startPoint, endPoint, amplitude, right); 
        //gameObject.transform.LookAt(Vector3.Lerp(startPoint, endPoint, 0.5f), gameObject.transform.up);
        transform.position = new Vector3(position.x, transform.position.y, position.z);
        
        
        if (Mathf.Abs(transform.position.x - endPoint.x )< 0.4f && Mathf.Abs(transform.position.z - endPoint.z )< 0.4f)
        {
            Destroy(gameObject);
        }
    }
    
    //Calculate path for dive attack
    private Vector3 CalculateSineRight(float t, Vector3 p0, Vector3 p1, float amplitude, int right)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1.0f - t;

        // Calculate the height (Y) based on a sine wave.
        float height = amplitude * Mathf.Sin(t * Mathf.PI);

        // Interpolate the position along the X and Z axes linearly.
        float x = oneMinusT * p0.x + t * p1.x;
        float z = oneMinusT * p0.z + t * p1.z;
        
        return new Vector3(x + (height * right), 0, z);
    }
}
