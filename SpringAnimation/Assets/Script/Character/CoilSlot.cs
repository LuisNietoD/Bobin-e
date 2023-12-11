using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoilSlot : MonoBehaviour
{
    public KeyCode action;
    public AttackBehavior actualAttack;

    public void ChangeWeapon()
    {
        actualAttack = transform.GetChild(0).GetComponent<AttackBehavior>();
        actualAttack.StartMethods(gameObject);
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
}
