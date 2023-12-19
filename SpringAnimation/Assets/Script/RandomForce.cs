using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomForce : MonoBehaviour
{
    private Rigidbody rb;
    public float maxForce = 2;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(-maxForce, maxForce), Random.Range(0f, 1f), Random.Range(-maxForce, maxForce));
    }

    private void Update()
    {
        if(transform.childCount <= 0)
            Destroy(gameObject);
        
        if(rb == null)
            return;
        if (rb.velocity.magnitude == 0)
        {
            Destroy(rb);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Void"))
        {
            Destroy(gameObject);
        }
    }
}
