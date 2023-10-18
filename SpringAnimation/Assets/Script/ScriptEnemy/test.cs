using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int pdv = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Coiffeur(int degats)
    {
        Debug.Log("le joueur prends des degats");
        pdv -= degats;

        if (pdv <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Joueur est mort");

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        IEnemy enemy = other.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(20);
        }
    }

}
