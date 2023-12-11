using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public ItemButton selectedSlot;
    public Transform pool;

    public List<ItemSlot> slots = new List<ItemSlot>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public void SelectNewSlot(ItemButton slot)
    {
        if(selectedSlot != null && selectedSlot != slot)
            selectedSlot.selector.SetActive(false);

        selectedSlot = slot;
        selectedSlot.selector.SetActive(true);
        
        if(selectedSlot.weapon.numberOfWeapon > 0)
            ShowEquipUI(true);
    }

    public void ShowEquipUI(bool show)
    {
        foreach (ItemSlot s in slots)
        {
            if(s.weapon == null)
                s.equipButton.SetActive(show);
        }
    }

    public Transform EquipPrefab(string attack, Transform parent)
    {
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                child.parent = pool;
                child.gameObject.SetActive(false);

            }
        }
        
        foreach (Transform weapon in pool)
        {
            Debug.Log("name" + weapon.gameObject.GetComponent<AttackBehavior>().Name);
            if (weapon.gameObject.GetComponent<AttackBehavior>().Name == attack)
            {
                weapon.parent = parent;
                weapon.gameObject.SetActive(true);
                return weapon;
            }
        }

        return null;
    }
}
