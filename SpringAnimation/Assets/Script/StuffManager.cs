using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffManager : MonoBehaviour
{
    public GameObject bomb;
    public GameObject flamethrower;
    public GameObject helix;
    public GameObject tornado;
    public GameObject slotsTransform;

    public CoilSlot item1;
    public CoilSlot item2;
    

  

    public GameObject getObj(int obj)
    {
        switch (obj)
        {
            case 0:
                return bomb;
                break;
            case 1:
                return flamethrower;
                break;
            case 2:
                return helix;
                break;
        }

        return null;
    }
}
