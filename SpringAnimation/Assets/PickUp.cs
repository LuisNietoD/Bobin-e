using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            PickUpItem item = other.GetComponent<PickUpItem>();
            if (item != null)
            {
                if (item.isWeapon)
                {
                    item.isWeapon = false;
                    item.weapon.numberOfWeapon++;
                }

                Destroy(other.gameObject);
            }
        }
    }
}
