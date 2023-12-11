using System.Collections.Generic;
using UnityEngine;

public class WeaponEventHandler : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();

    private void OnEnable()
    {
        // Subscribe to the static events for adding and removing weapons
        WeaponEvents.OnAddWeapon += HandleAddWeapon;
        WeaponEvents.OnRemoveWeapon += HandleRemoveWeapon;
    }

    private void OnDisable()
    {
        // Unsubscribe from the static events to prevent memory leaks
        WeaponEvents.OnAddWeapon -= HandleAddWeapon;
        WeaponEvents.OnRemoveWeapon -= HandleRemoveWeapon;
    }

    private void HandleAddWeapon(GameObject weapon)
    {
        weapons.Add(weapon);
    }

    private void HandleRemoveWeapon(GameObject weapon)
    {
        weapons.Remove(weapon);
    }
}