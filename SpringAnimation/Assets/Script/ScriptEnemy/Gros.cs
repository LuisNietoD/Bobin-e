using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gros : MonoBehaviour, IEnemy
{
    public int vie = 100;

    public void TakeDamage(int damage)
    {
        vie -= damage;
        Debug.Log("Gros a perdu des pv");
        if (vie <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Gros est mort");


    }

}
