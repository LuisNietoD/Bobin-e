using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour,IEnemy
{
    public int vie = 40;

    public void TakeDamage(int damage)
    {
        vie -= damage;
        Debug.Log("Rat a perdu des pv");
        if (vie <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Rat est mort");
    }

}
