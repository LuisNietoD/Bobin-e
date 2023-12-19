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
    public GameObject motorObject;
    

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

    public void ChangeSpeed(float speed)
    {
        actualSpeed = speed;
        if (actualSpeed == 0)
        {
            foreach (var g in neighbors)
            {
                if(!g.motor)
                    g.actualSpeed = 0;
            }
        }
        else
        {
            foreach (var g in neighbors)
            {
                if(!g.motor)
                    g.actualSpeed = actualSpeed * ((float)cogs / (float)g.cogs) * -1; 
            }
        }
    }

    public void ChangeMaterial(Material mat)
    {
        if (isMovable && onHand)
        {
            foreach (Transform child in transform)
            {
                if(child.transform.childCount > 0)
                    child.transform.GetChild(0).GetComponent<Renderer>().material = mat;
            }
        }
    }

    public void ResetMaterial()
    {
        ChangeMaterial(mainMat);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (actualSpeed != 0 && other.CompareTag("Gear"))
        {
            if(!neighbors.Contains(other.transform.parent.GetComponent<Gear>()))
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
            if (!other.transform.parent.GetComponent<Gear>().motor && !other.transform.parent.GetComponent<Gear>().onHand)
            {
                if (neighbors.Contains(other.transform.parent.GetComponent<Gear>()))
                {
                    Gear child = other.transform.parent.GetComponent<Gear>();
                    child.ChangeSpeed(actualSpeed * ((float)cogs / (float)child.cogs) * -1);
                    child.actualTorque = actualTorque / ((float)cogs / (float)child.cogs) * -1;
                }

                if (!neighbors.Contains(other.transform.parent.GetComponent<Gear>()) &&
                    !other.transform.parent.GetComponent<Gear>().neighbors.Contains(this))
                    neighbors.Add(other.transform.parent.GetComponent<Gear>());
            }
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
            ChangeSpeed(0);
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
