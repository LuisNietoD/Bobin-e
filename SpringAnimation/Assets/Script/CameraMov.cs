using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public Transform player;
    public Camera cam;
    public Movement movement;
    public float me;

    private void Update()
    {
        //if (movement.grounded == true)
        //{
        //    me = player.position.y + 2.5f;
        //}

        transform.position = new Vector3(player.transform.position.x, me, player.transform.position.z);

    }
    
}
