using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoilSlot : MonoBehaviour
{
    public int actualItem = -1;
    public CraftManager craftManager;
    public Image img;
    public KeyCode action;
    public AttackBehavior actualAttack;
    public LayerMask enemyMask;
    public ParticleSystem flame;

    private void Start()
    {
        actualAttack = new Flamethrower();
        actualAttack.StartMethods(gameObject);
        craftManager = CraftManager.instance;
        img = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(action) && actualAttack != null)
        {
            actualAttack.Action();
        }
        
        if(actualAttack != null)
            actualAttack.UpdateMethods();
    }

    public void ClickOnSlot()
    {
        if (actualItem <= -1 && craftManager.itemOnHand <= -1)
            return;
        
        if (actualItem <= -1 && craftManager.itemOnHand >= 0)
        {
            PutItem();
        }
        else if (craftManager.itemOnHand <= -1 && actualItem >= 0)
        {
            TakeItem();
        }
        else
        {
            SwitchItem();
        }
    }

    public void PutItem()
    {
        actualItem = craftManager.itemOnHand;
        craftManager.ClearCursor();
        UpdateStuff();
    }

    public void TakeItem()
    {
        craftManager.SetCursor(actualItem);
        actualItem = -1;
        UpdateStuff();
    }

    public void SwitchItem()
    {
        int oldSlotItem = actualItem;
        PutItem();
        craftManager.SetCursor(oldSlotItem);
    }

    public void UpdateStuff()
    {
        if (actualItem >= 0)
        {
            img.sprite = craftManager.img[actualItem];
            img.gameObject.SetActive(true);
        }
        else
        {
            img.sprite = null;
            img.gameObject.SetActive(false);

        }
    }
}
