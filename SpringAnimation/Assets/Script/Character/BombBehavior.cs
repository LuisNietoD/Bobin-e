using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    public GameObject explosion;
    public GameObject fireArea;
    public Rigidbody _rb;
    public float power = 10;
    public bool napalm;
    
    private void Start()
    {
        _rb.AddForce(transform.forward * power, ForceMode.Impulse);
    }

    private void Update()
    {
        if (_rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_rb.velocity.normalized);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject e = Instantiate(explosion, collision.GetContact(0).point, Quaternion.identity);
        e.transform.localScale *= 3;
        if (napalm)
        {
            GameObject f = Instantiate(fireArea, collision.GetContact(0).point, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
