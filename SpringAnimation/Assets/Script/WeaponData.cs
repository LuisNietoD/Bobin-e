using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite weaponIcon;
    public int numberOfWeapon;
    [Header("Compatible Slots")] 
    public bool[] slotCompatibility = { true, true, true };
    [Header("Craft")]
    public bool isCraftable;
    public List<WeaponData> itemNeeded = new List<WeaponData>();
}