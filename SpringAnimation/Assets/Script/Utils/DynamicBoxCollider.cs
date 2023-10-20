using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBoxCollider : MonoBehaviour
{
    private BoxCollider boxCollider;

    private void Start()
    {
        // Get the BoxCollider component of your GameObject
        boxCollider = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        Destroy(boxCollider);
        boxCollider = gameObject.AddComponent<BoxCollider>();
    }
}

