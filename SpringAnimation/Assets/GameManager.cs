using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera playerCam;
    public Transform player;
    public static bool isPlayerLock = false;
    public LayerMask enemyMask;
    public int nutBolt = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        isPlayerLock = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the game window
        Cursor.visible = false; // Hide the cursor
    }

    private void Update()
    {
        Debug.Log(isPlayerLock);
        

    }

    public void ChangeCamera(Camera cam)
    {
        Camera.main.enabled = false;
        cam.enabled = true;
    }
    
    public static Vector3 CalculateSineWavePoint(float t, Vector3 p0, Vector3 p1, float amplitude)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1.0f - t;

        // Calculate the height (Y) based on a sine wave.
        float height = amplitude * Mathf.Sin(t * Mathf.PI);

        // Interpolate the position along the X and Z axes linearly.
        float x = oneMinusT * p0.x + t * p1.x;
        float z = oneMinusT * p0.z + t * p1.z;
        float y = oneMinusT * p0.y + t * p1.y;
        return new Vector3(x, y + height, z);
    }
}
