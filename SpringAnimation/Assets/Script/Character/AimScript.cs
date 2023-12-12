using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    public LayerMask aimLayer;
    public float reach = 8;
    public GameObject aim;
    public float offset = 5;
    
    private bool isAiming = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button clicked
        {
            StartAiming();
        }

        if (Input.GetMouseButtonUp(1)) // Right mouse button released
        {
            StopAiming();
        }

        if (isAiming)
        {
            UpdateAimPosition();
        }
    }

    void StartAiming()
    {
        isAiming = true;
        aim.SetActive(true);
    }

    void StopAiming()
    {
        isAiming = false;
        aim.SetActive(false);
    }

    void UpdateAimPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 dir = Camera.main.transform.forward;
        dir.y = 0;
        ray.origin += dir * offset; 

        if (Physics.Raycast(ray, out hit, reach, aimLayer))
        {
            aim.transform.position = hit.point;
            aim.SetActive(true);
        }
        else
        {
            aim.SetActive(false);
        }
    }
}
