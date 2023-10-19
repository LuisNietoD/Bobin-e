using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public bool motor;
    public bool goal;
    public float actualSpeed = 0;
    public float actualTorque = 0;
    public int cogs;
    public bool roll = true;
    public List<Gear> neighbors = new List<Gear>();

    private void FixedUpdate()
    {
        if(roll)
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z + actualSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (actualSpeed > 0 && other.CompareTag("Gear"))
        {
            neighbors.Add(other.transform.parent.GetComponent<Gear>());
        }
        
        if(other.CompareTag("Cog"))
        {
            roll = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Gear") && actualSpeed != 0)
        {
            Gear child = other.transform.parent.GetComponent<Gear>();
            child.actualSpeed = actualSpeed * ((float)cogs / (float)child.cogs) * -1;
            child.actualTorque = actualTorque / ((float)cogs / (float)child.cogs) * -1;
        }
        
        if(!other.CompareTag("Cog"))
        {
            roll = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!motor && !other.CompareTag("Cog"))
        {
            actualSpeed = 0;
            actualTorque = 0;
        }
        
        if (other.CompareTag("Gear"))
        {
            if(neighbors.Contains(other.transform.parent.GetComponent<Gear>()))
                neighbors.Remove(other.transform.parent.GetComponent<Gear>());
        }
    }
}
