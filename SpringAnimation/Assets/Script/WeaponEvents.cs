using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEvents : MonoBehaviour
{
    public delegate void WeaponAction(GameObject weapon);
    public static event WeaponAction OnAddWeapon;
    public static event WeaponAction OnRemoveWeapon;

    public static void AddWeapon(GameObject weapon)
    {
        OnAddWeapon?.Invoke(weapon);
    }

    public static void RemoveWeapon(GameObject weapon)
    {
        OnRemoveWeapon?.Invoke(weapon);
    }
}
