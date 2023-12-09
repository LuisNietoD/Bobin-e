using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public bool motor;
    public bool goal;
    public bool roll = true;
    public bool overlap;
    public bool isMovable;
    
    public float actualSpeed = 0;
    public float actualTorque = 0;
    public int cogs;
    public List<Gear> neighbors = new List<Gear>();
    public Material mainMat;
    public Material wrongPosMaterial;
    public Material GoodPosMaterial;
    public bool onHand;
    

    private void FixedUpdate()
    {
        if(roll && !onHand)
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z + actualSpeed * Time.deltaTime);
    
        if(!onHand)
            ChangeMaterial(mainMat);

        if (onHand && !overlap)
        {
            ChangeMaterial(GoodPosMaterial);
        }
    }

    public void ChangeMaterial(Material mat)
    {
        if (isMovable && onHand)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Renderer>().material = mat;
            }
        }
    }

    public void ResetMaterial()
    {
        ChangeMaterial(mainMat);
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

        if (other.CompareTag("GearBase"))
        {
            overlap = true;
            ChangeMaterial(wrongPosMaterial);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Gear") && actualSpeed != 0 && !onHand)
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
        
        if (other.CompareTag("GearBase"))
        {
            overlap = false;
            ChangeMaterial(GoodPosMaterial);
        }
    }
}
