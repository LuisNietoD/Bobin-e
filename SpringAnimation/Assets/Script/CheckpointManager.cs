using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    public Vector3 respawnPoint;
    public int id = 0;


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

    public void ChangeCheckpoint(int id, Vector3 checkpoint)
    {
        if (id > this.id)
        {
            respawnPoint = checkpoint;
            this.id = id;
        }
    }
    
    
}
