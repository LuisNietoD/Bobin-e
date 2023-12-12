using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemSlot : MonoBehaviour
{
    public WeaponData weapon;
    public Image icon;
    public GameObject equipButton;
    public CoilSlot slotInGame;
    public GameObject cross;

    public void Equip()
    {
        if(weapon != null)
            weapon.numberOfWeapon++;

        cross.SetActive(true);
        
        weapon = InventoryManager.instance.selectedSlot.weapon;
        weapon.numberOfWeapon--;
        equipButton.SetActive(false);
        if(weapon.numberOfWeapon <= 0)
            InventoryManager.instance.ShowEquipUI(false);
        icon.sprite = weapon.weaponIcon;
        icon.enabled = true;

        if (slotInGame.transform.childCount > 0)
        {
            slotInGame.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            slotInGame.transform.GetChild(0).SetParent(InventoryManager.instance.pool);
        }

        Transform weaponObject = InventoryManager.instance.EquipPrefab(weapon.weaponName, slotInGame.transform);
        weaponObject.SetParent(slotInGame.transform);
        weaponObject.localPosition = Vector3.zero;
        weaponObject.localEulerAngles = Vector3.zero;
        slotInGame.ChangeWeapon();
    }

    public void Unequip()
    {
        cross.SetActive(false);
        
        slotInGame.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        slotInGame.transform.GetChild(0).SetParent(InventoryManager.instance.pool);
        slotInGame.actualAttack = null;
        
        weapon.numberOfWeapon++;
        weapon = null;
        icon.enabled = false;
        if(InventoryManager.instance.selectedSlot.weapon.numberOfWeapon > 0)
            InventoryManager.instance.ShowEquipUI(true);
    }
}
