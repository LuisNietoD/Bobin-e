using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    public int itemOnHand;
    public GameObject craftUI;
    public GameObject lastSlot;
    public GameObject cursor;
    public List<Sprite> img;

    public void TakeItem()
    {
        int swapItem = 0;
        if (itemOnHand != 0)
        {
            swapItem = itemOnHand;
        }
        itemOnHand = EventSystem.current.currentSelectedGameObject.GetComponent<ItemSlot>().actualItem;
        if (swapItem == 0)
        {
            lastSlot = EventSystem.current.currentSelectedGameObject;
            lastSlot.SetActive(false);
        }
        else
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<ItemSlot>().actualItem = swapItem;
        }

        cursor.SetActive(true);
        cursor.GetComponent<Image>().sprite = img[itemOnHand - 1];
    }

    public void PutItem()
    {
        
    }

    public void PutSlot()
    {
        if (itemOnHand != 0)
        {
            lastSlot.GetComponent<ItemSlot>().actualItem = itemOnHand;
            itemOnHand = 0;
            cursor.SetActive(false);
            lastSlot.transform.SetAsLastSibling();
            if (lastSlot != null)
                lastSlot.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            craftUI.SetActive(!craftUI.activeSelf);
            itemOnHand = 0;
            cursor.SetActive(false);
            if(lastSlot != null)
                lastSlot.SetActive(true);
        }
        
        if (craftUI.activeSelf)
        {
            cursor.transform.position = Input.mousePosition;
        }
    }
}
