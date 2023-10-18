using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public float speed = 2;
    public float amplitude = 7;
    public Vector3 startPoint;
    public Vector3 endPoint;
    private float t = 0.0f;
    public GameObject explosion;
    public GameObject fireArea;
    
    
    private void FixedUpdate()
    {
        t += Time.deltaTime * speed;
        if (t > 1.0f)
        {
            t = 1.0f;
        }
        
        Vector3 position = CalculateSineRight(t, startPoint, endPoint, amplitude); 
        gameObject.transform.LookAt(Vector3.Lerp(startPoint, endPoint, 0.5f), gameObject.transform.up);
        transform.position = position;
        
        
        if (Mathf.Abs(transform.position.x - endPoint.x )< 0.4f && Mathf.Abs(transform.position.z - endPoint.z )< 0.4f)
        {
            GameObject e = Instantiate(explosion, endPoint, Quaternion.identity);
            e.transform.localScale *= 5;
            t = 0;
            
            if(fireArea != null)
                Instantiate(fireArea, endPoint, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    
    //Calculate path for dive attack
    private Vector3 CalculateSineRight(float t, Vector3 p0, Vector3 p1, float amplitude)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1.0f - t;

        // Calculate the height (Y) based on a sine wave.
        float height = amplitude * Mathf.Sin(t * Mathf.PI);

        // Interpolate the position along the X and Z axes linearly.
        float x = oneMinusT * p0.x + t * p1.x;
        float z = oneMinusT * p0.z + t * p1.z;
        
        return new Vector3(x, height, z);
    }
}
