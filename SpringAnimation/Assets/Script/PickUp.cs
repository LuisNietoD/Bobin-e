using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Animator pickUpUi;
    public bool activeUI;
    private float timeOnScreen = 5;

    private void Update()
    {
        if (timeOnScreen < 5)
        {
            timeOnScreen += Time.deltaTime;
        }
        if (timeOnScreen > 5 && activeUI)
        {
            activeUI = false;
            pickUpUi.Play("Hide");
        }
        
    }

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
                else if (item.items == PickUpItem.itemList.Ecrou)
                {
                    activeUI = true;
                    pickUpUi.Play("Show");
                    timeOnScreen = 0;
                    GameManager.instance.nutBolt++;
                }

                Destroy(other.gameObject);
            }
        }
    }
}
