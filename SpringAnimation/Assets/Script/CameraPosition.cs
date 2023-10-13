using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{

    public GameObject cameraAim;
    private void Update()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {
            
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                cameraAim.transform.position = hit.point;
            }
        }
    }
}
