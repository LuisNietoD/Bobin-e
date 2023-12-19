using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public WeaponData weapon;
    public Image icon;
    public TextMeshProUGUI stock;
    public GameObject selector;
    

    private bool isSelected;
    
    private void Start()
    {
        icon.sprite = weapon.weaponIcon;
    }

    private void Update()
    {
        stock.text = weapon.numberOfWeapon.ToString();
    }

    public void Select()
    {
        InventoryManager.instance.SelectNewSlot(this);
    }
}