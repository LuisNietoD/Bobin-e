using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWeapon : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int resolution = 20; // Number of points on the curve
    public float amplitude = 2.0f; // Amplitude of the sine wave
    public bool isAiming = false; // Flag to check if aiming mode is active
    public Bomb bomb;

    void Update()
    {
        // Check for right mouse button click to toggle aiming mode
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        if (isAiming && bomb.aim.activeSelf)
        {
            Vector3[] positions = CalculateSineWavePoints(transform.position, bomb.aim.transform.position, resolution, amplitude);

            // Set the LineRenderer's positions to the calculated points
            lineRenderer.positionCount = resolution;
            lineRenderer.SetPositions(positions);
        }
        else
        {
            // Disable LineRenderer when not aiming
            lineRenderer.positionCount = 0;
        }
    }

    Vector3[] CalculateSineWavePoints(Vector3 origin, Vector3 endPoint, int resolution, float amplitude)
    {
        Vector3[] points = new Vector3[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float t = (float)i / (float)resolution;
            points[i] = GameManager.CalculateSineWavePoint(t, origin, endPoint, amplitude);
        }

        return points;
    }

    
}
