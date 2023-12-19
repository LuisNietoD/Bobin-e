using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearManager : MonoBehaviour
{
    public Animator door;
    public List<Gear> goals = new List<Gear>();
    public bool done;
    public CogGameScript cgs;
    public List<Transform> allGears = new List<Transform>();
    public Transform wall;

    private void Start()
    {
        foreach (var g in allGears)
        {
            g.transform.localPosition = new Vector3(g.transform.localPosition.x, g.transform.localPosition.y,
                wall.localPosition.z);
        }
    }

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
        cgs.ShowAll();
        GameManager.instance.ChangeCamera(GameManager.instance.playerCam);
    }
    
    
}
