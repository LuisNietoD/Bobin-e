using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    private ICharacterState currentState;

    public void ChangeState(ICharacterState state)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = state;
        currentState.OnEnter();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }
}
