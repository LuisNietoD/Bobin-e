using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Disassemble : MonoBehaviour
{
    public List<GameObject> objectParts = new List<GameObject>();
    public List<GameObject> toDisable = new List<GameObject>();
    public GameObject explodePrefab;
    public float explodeScale = 1;
    private Rigidbody rb;
    private bool toDestroy;
    private float timeBeforeDestroy = 5;
    
    private Dictionary<Transform, Vector3> originalScales = new Dictionary<Transform, Vector3>();
    
    void Start()
    {
        // Store the original scales of children
        foreach (Transform child in transform)
        {
            originalScales[child] = child.localScale;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            if (rb.velocity.y < 0)
            {
                DestroyObject();
                rb = null;
            }
        }

        if (toDestroy)
        {
            timeBeforeDestroy -= Time.deltaTime;
            if (timeBeforeDestroy <= 2)
            {
                // Loop through all child objects of this GameObject
                foreach (Transform child in transform)
                {
                    // Get the original scale from the dictionary and scale down each child to zero over 2 seconds
                    Vector3 originalScale;
                    if (originalScales.TryGetValue(child, out originalScale))
                    {
                        child.localScale = Vector3.Lerp(originalScale, Vector3.zero, 1 - (timeBeforeDestroy / 2f));
                    }
                }
            }
        }
    }

    public void DestroyObject()
    {
        foreach (GameObject o in toDisable)
        {
            o.SetActive(false);
        }
        
        foreach (GameObject o in objectParts)
        {
            o.AddComponent<Rigidbody>();
            o.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 5f, ForceMode.Impulse);
            o.GetComponent<Collider>().enabled = true;
        }
        GameObject e = Instantiate(explodePrefab, transform.position, quaternion.identity);
        e.transform.localScale = new Vector3(explodeScale, explodeScale, explodeScale);
        DestroyOverTime d = transform.parent.gameObject.AddComponent<DestroyOverTime>();
        d.destroyTime = 5;
        toDestroy = true;
    }
}
