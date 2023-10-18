using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStateManager playerStateManager;
    
    // Start is called before the first frame update
    void Start()
    {
        playerStateManager = GetComponent<PlayerStateManager>();
        playerStateManager.ChangeState(new StandingState());
    }

    // Update is called once per frame
    void Update()
    {
        //Move
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            playerStateManager.ChangeState(new MoveState());
        }
    }
}
