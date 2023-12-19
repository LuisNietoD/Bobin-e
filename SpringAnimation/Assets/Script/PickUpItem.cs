using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [Header("Select item")]
    public itemList items = itemList.Bomb;

    public enum itemList
    {
        Bomb,
        Flamethrower,
        Propeller,
        Ecrou
    }

    [Space]
    public List<GameObject> objects = new List<GameObject>();

    public WeaponData weapon;
    public bool isWeapon = true;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        objects[GetID()].SetActive(true);
    }

    public int GetID()
    {
        if (items == itemList.Bomb)
            return 0;
        if (items == itemList.Flamethrower)
            return 1;
        if (items == itemList.Propeller)
            return 2;
        
        isWeapon = false;
        return 3;
    }
}
