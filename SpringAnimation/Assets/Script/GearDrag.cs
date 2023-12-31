using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearDrag : MonoBehaviour
{
    public LayerMask gearLayer;
    public LayerMask mouseLayer;

    private GameObject gear;

    private Vector3 savePos;

    public Camera cam;

    public float z = 5;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("test");
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, gearLayer))
            {
                Debug.Log(hit.transform.name);
                
                if (hit.transform.CompareTag("Gear"))
                {
                    if (hit.transform.GetComponent<Gear>().isMovable)
                    {
                        gear = hit.transform.gameObject;
                        gear.GetComponent<Gear>().onHand = true;
                        savePos = gear.transform.localPosition;
                    }
                }
            } 
        }

        if (gear != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("test");
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, mouseLayer))
            {
                gear.transform.position =
                    new Vector3(hit.point.x, hit.point.y, hit.point.z);
            } 
            //Vector3 mPos = Input.mousePosition;
           // mPos.z = z;//Mathf.Abs(cam.transform.position.z - gear.transform.position.z);
            //Vector3 mousePos = cam.ScreenToWorldPoint(mPos);
            //Debug.Log("World pos ======  " + mousePos);
            //Debug.Log(Input.mousePosition);
            
        }

        if (Input.GetMouseButtonUp(0) && gear != null)
        {
            if (gear.GetComponent<Gear>().overlap)
            {
                gear.transform.localPosition = savePos;
            }
            gear.GetComponent<Gear>().ResetMaterial();
            gear.GetComponent<Gear>().onHand = false;
            gear = null;
        }
    }
}
