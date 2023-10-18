using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projo : MonoBehaviour
{

    public int attaque = 30;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            test pv = collision.gameObject.GetComponent<test>();

            pv.Coiffeur(attaque);

            Destroy(gameObject);
        }


    }
}
