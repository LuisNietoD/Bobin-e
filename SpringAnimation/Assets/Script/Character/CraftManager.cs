using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    public static CraftManager instance;
    
    public int itemOnHand = -1;
    public GameObject craftUI;
    public GameObject lastSlot;
    public GameObject cursor;
    public List<Sprite> img;
    public List<int> stack;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
        stack.Add(2);
        stack.Add(8);
        stack.Add(3);
    }

    private void Start()
    {
        
    }

    public void TakeItem(int i)
    {
        if (itemOnHand == i)
        {
            stack[itemOnHand]++;
            cursor.SetActive(false);
            itemOnHand = -1;
            return;
        }
        if (stack[i] <= 0)
        {
            return;
        }
        
        if (itemOnHand != -1)
        {
            stack[itemOnHand]++;
        }

        
        itemOnHand = i;
        stack[itemOnHand]--;
        cursor.SetActive(true);
        cursor.GetComponent<Image>().sprite = img[itemOnHand];
        
    }

    public void ClearCursor()
    {
        itemOnHand = -1;
        cursor.SetActive(false);
    }

    public void SetCursor(int item)
    {
        itemOnHand = item;
        cursor.GetComponent<Image>().sprite = img[itemOnHand];
        cursor.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            craftUI.SetActive(!craftUI.activeSelf);
            if(itemOnHand != -1)
                stack[itemOnHand]++;
            itemOnHand = -1;
            cursor.SetActive(false);
        }
        
        if (craftUI.activeSelf)
        {
            cursor.transform.position = Input.mousePosition;
        }
    }
}
