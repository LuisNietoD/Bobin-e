using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(StateManager mob, Enemy enemy);

    public abstract void UpdateState(StateManager mob, Enemy enemy);

}
