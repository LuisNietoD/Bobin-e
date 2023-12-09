using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera playerCam;
    public bool canMove = true;

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

    private void Update()
    {
        Debug.Log(Camera.main);
        Debug.Log(playerCam);
        if (Camera.main == playerCam)
            canMove = true;
        else
            canMove = false;

    }

    public void ChangeCamera(Camera cam)
    {
        Camera.main.enabled = false;
        cam.enabled = true;
    }
}
