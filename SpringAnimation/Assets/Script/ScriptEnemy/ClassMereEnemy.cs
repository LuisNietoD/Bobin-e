using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassMereEnemy : MonoBehaviour
{
    public float pv;

    public virtual void TakeDamage(float damage)
    {
        pv -= damage;
    }
}
