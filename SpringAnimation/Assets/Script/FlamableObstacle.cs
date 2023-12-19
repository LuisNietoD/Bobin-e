using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamableObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flamable"))
        {
            Destroy(other.gameObject);
        }
    }
}
