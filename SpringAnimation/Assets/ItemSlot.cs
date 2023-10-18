using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemSlot : MonoBehaviour
{
    public int actualItem;
    public List<Sprite> itemImg;
    public Image img;
    public TextMeshProUGUI stackText;

    private void Awake()
    {
        img = transform.GetChild(0).GetComponent<Image>();
        stackText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
        img.sprite = itemImg[actualItem];
    }

    private void Start()
    {
        stackText.text = CraftManager.instance.stack[actualItem].ToString();
    }


    private void Update()
    {
        img.sprite = itemImg[actualItem];
        stackText.text = CraftManager.instance.stack[actualItem].ToString();
    }
}
