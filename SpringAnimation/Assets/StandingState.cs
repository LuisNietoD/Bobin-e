using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : ICharacterState
{
    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {
        Debug.Log("standing");
    }

    public void OnExit()
    {
        
    }
}
