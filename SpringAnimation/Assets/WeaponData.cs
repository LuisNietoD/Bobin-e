using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite weaponIcon;
    public int numberOfWeapon;
    public bool isCraftable;
    public List<WeaponData> itemNeeded = new List<WeaponData>();
}