using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public enum itemList
    {
        Bomb,
        Flamethrower,
        Helix
    }

    public itemList items = itemList.Bomb;
    
    
}
