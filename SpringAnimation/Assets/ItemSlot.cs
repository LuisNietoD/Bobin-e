using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemSlot : MonoBehaviour
{
    public int actualItem;
    public List<Sprite> itemImg;
    public Image img;

    private void Start()
    {
        img = transform.GetChild(0).GetComponent<Image>();
        actualItem = Random.Range(1, 4);
    }

    private void Update()
    {
        img.sprite = itemImg[actualItem - 1];
    }
}
