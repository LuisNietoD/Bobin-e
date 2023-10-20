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
    

    public void UpStuff()
    {
        if (item1.actualItem == -1 || item2.actualItem == -1)
        {
            if (item1 == item2)
            {
                foreach (Transform child in slotsTransform.transform)
                {
                    Destroy(child);
                }
                return;
            }

            if (item1.actualItem > -1)
            {
                foreach (Transform child in slotsTransform.transform)
                {
                    Destroy(child);
                }
                GameObject s = Instantiate(getObj(item1.actualItem), slotsTransform.transform);
            }
            if (item2.actualItem > -1)
            {
                foreach (Transform child in slotsTransform.transform)
                {
                    Destroy(child);
                }
                GameObject s = Instantiate(getObj(item1.actualItem), slotsTransform.transform);
            }
        }
    }

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
