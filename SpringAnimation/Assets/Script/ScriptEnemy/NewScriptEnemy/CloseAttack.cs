using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : MonoBehaviour
{
    public float tempsEcoule = 0f;
    public float intervalle = 1f;
    public float damage;

    private void OnTriggerStay(Collider other)
    {
        Movement attacking = other.GetComponent<Movement>();

        if (attacking != null)
        {
            tempsEcoule += Time.deltaTime;
            if (tempsEcoule >= intervalle)
            {
                attacking.TakeDamage();

                tempsEcoule = 0f;
            }
        }

    }
}
