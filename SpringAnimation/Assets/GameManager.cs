using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera playerCam;
    public static bool isPlayerLock = false;
    public LayerMask enemyMask;


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
}
