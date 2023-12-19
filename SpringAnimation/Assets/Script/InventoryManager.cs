using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory")]
    public static InventoryManager instance;
    public ItemButton selectedSlot;
    public Transform pool;
    public TextMeshProUGUI infos;
    private float textReset = 5f;

    [Header("Craft")] 
    public GameObject craftUI;
    public Image itemNeeded1;
    public Image itemNeeded2;
    

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

    private void Update()
    {
        textReset -= Time.deltaTime;
        if (textReset <= 0)
        {
            infos.text = "";
        }
    }

    public void SelectNewSlot(ItemButton slot)
    {
        if(selectedSlot != null && selectedSlot != slot)
            selectedSlot.selector.SetActive(false);
        if (selectedSlot == slot)
        {
            selectedSlot.selector.SetActive(false);
            ShowEquipUI(false);
            selectedSlot = null;
            return;
        }


        selectedSlot = slot;
        selectedSlot.selector.SetActive(true);

        ShowEquipUI(selectedSlot.weapon.numberOfWeapon > 0);
        if (selectedSlot.weapon.isCraftable)
        {
            itemNeeded1.sprite = selectedSlot.weapon.itemNeeded[0].weaponIcon;
            itemNeeded2.sprite = selectedSlot.weapon.itemNeeded[1].weaponIcon;
            craftUI.SetActive(true);
        }
        else
        {
            craftUI.SetActive(false);
        }
    }

    public void ShowEquipUI(bool show)
    {
        foreach (ItemSlot s in slots)
        {
            if((s.weapon == null || s.weapon != selectedSlot.weapon) && selectedSlot.weapon.slotCompatibility[s.ID])
                s.equipButton.SetActive(show);
            else
                s.equipButton.SetActive(false);
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

    public void CraftItem()
    {
        if (selectedSlot.weapon.itemNeeded[0].numberOfWeapon > 0 &&
            selectedSlot.weapon.itemNeeded[1].numberOfWeapon > 0)
        {
            selectedSlot.weapon.numberOfWeapon++;
            selectedSlot.weapon.itemNeeded[0].numberOfWeapon--;
            selectedSlot.weapon.itemNeeded[1].numberOfWeapon--;
            ShowEquipUI(true);
        }
        else
        {
            string weaponName;
            if (selectedSlot.weapon.itemNeeded[0].numberOfWeapon <= 0)
                weaponName = selectedSlot.weapon.itemNeeded[0].weaponName;
            else
                weaponName = selectedSlot.weapon.itemNeeded[1].weaponName;
            infos.text = "You need more " + weaponName + "!";
            textReset = 5;
        }
    }
}
