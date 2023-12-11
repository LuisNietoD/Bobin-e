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

    public void Equip()
    {
        weapon = InventoryManager.instance.selectedSlot.weapon;
        weapon.numberOfWeapon--;
        equipButton.SetActive(false);
        if(weapon.numberOfWeapon <= 0)
            InventoryManager.instance.ShowEquipUI(false);
        icon.sprite = weapon.weaponIcon;
        icon.enabled = true;
        
        Transform weaponObject = InventoryManager.instance.EquipPrefab(weapon.weaponName, slotInGame.transform);
        weaponObject.SetParent(slotInGame.transform);
        weaponObject.localPosition = Vector3.zero;
        weaponObject.localEulerAngles = Vector3.zero;
        slotInGame.ChangeWeapon();
    }
}
