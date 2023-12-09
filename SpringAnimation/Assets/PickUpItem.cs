using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public enum itemList
    {
        Bomb,
        Flamethrower,
        Helix
    }

    public List<GameObject> objects = new List<GameObject>();

    public itemList items = itemList.Bomb;
    public int itemId;

    private void Start()
    {
        switch (items)
        {
            case itemList.Bomb:
                objects[0].SetActive(true);
                objects[1].SetActive(false);
                objects[2].SetActive(false);
                break;
            case itemList.Flamethrower:
                objects[0].SetActive(false);
                objects[1].SetActive(true);
                objects[2].SetActive(false);
                break;
            case itemList.Helix:
                objects[0].SetActive(false);
                objects[1].SetActive(false);
                objects[2].SetActive(true);
                break;
        }
    }

    public int GetID()
    {
        if (items == itemList.Bomb)
            return 0;
        if (items == itemList.Flamethrower)
            return 1;
        if (items == itemList.Helix)
            return 2;
        return -1;
    }
}
