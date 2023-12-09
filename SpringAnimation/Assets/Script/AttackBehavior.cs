using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AttackBehavior
{
    public  int damage;

    public abstract void Action();

    public abstract void StartMethods(GameObject slot);

    public abstract void UpdateMethods();
}
