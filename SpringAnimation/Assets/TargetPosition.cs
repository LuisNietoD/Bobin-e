using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    public GameObject player;
    public static float y;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position;
        y = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = player.transform.position;
        position.y = y;
        
        transform.position = position;
    }
}
