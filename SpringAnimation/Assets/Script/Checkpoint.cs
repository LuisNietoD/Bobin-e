using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int id;
    public bool alreadyOn;

    public MeshRenderer flag;
    public Material green;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dddd");

        if (other.CompareTag("Player") && !alreadyOn)
        {
            Debug.Log("test");
            CheckpointManager.instance.ChangeCheckpoint(id, transform.position);
            alreadyOn = true;
            flag.material = green;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ddddddsdasdad");

    }
}
