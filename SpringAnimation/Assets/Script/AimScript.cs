using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    public LayerMask aimLayer;
    public float reach = 8;
    public GameObject aim;
    public Attack attackRef;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && !attackRef.attacking)
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


           // Debug.DrawLine(ray.origin, ray.direction * reach, Color.blue, 5f);
            if (Physics.Raycast(ray, out hit, reach, aimLayer))
            {
                // Position the crosshair at the hitpoint
                aim.transform.position = hit.point;
                aim.SetActive(true);
            }
            else
            {
                // If the ray doesn't hit the ground, hide the crosshair
                aim.SetActive(false);
            }
        }
        else if(!attackRef.attacking)
        {
            //Hide crosshair if not aiming
            aim.SetActive(false);
        }
    }
}
