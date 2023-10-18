using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wazo : MonoBehaviour, IEnemy
{
    public int vie = 80;

    public void TakeDamage(int damage)
    {
        vie -= damage;
        Debug.Log("Wazo a perdu des pv");
        if (vie <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Wazo est mort");
    }

}
