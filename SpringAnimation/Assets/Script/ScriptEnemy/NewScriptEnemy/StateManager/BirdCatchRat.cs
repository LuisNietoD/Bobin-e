using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCatchRat : BaseState
{

    public override void EnterState(StateManager mob, Enemy enemy)
    {
        enemy.birdchasing = true;
        enemy.gotRat = false;
        enemy.MakeRatList();
    }

    public override void UpdateState(StateManager mob, Enemy enemy)
    {
        if (enemy.rats.Count > 0 && !enemy.gotRat)
            enemy.agent.SetDestination(enemy.rats[0].transform.position);

        if (enemy.gotRat) mob.SwitchState(mob.chasing);
    }

}
