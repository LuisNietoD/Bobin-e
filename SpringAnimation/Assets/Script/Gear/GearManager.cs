using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearManager : MonoBehaviour
{
    public Animator door;
    public List<Gear> goals = new List<Gear>();
    public bool done;

    private void Update()
    {
        
        
        foreach (Gear gear in goals)
        {
            if (gear.actualSpeed == 0)
            {
                if (done)
                {
                    done = false;
                    door.Play("Close");
                }
                return;
            }
        }
        
        if(done)
            return;
        
        door.Play("Open");
        done = true;
    }
}
