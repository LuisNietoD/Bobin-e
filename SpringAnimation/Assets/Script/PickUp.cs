using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private CraftManager craftManager;

    private void Start()
    {
        craftManager = CraftManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
//            craftManager.stack[other.GetComponent<PickUpItem>().GetID()]++;
            Destroy(other.gameObject);
        }
    }
}
