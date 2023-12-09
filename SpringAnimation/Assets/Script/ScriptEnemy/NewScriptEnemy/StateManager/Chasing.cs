using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : BaseState
{

    public override void EnterState(StateManager mob, Enemy enemy)
    {
        Debug.Log("Chasing ?");
        enemy.birdchasing = false;
        if (enemy.CompareTag("Bird")) enemy.caughtRat.SetActive(true);
    }

    public override void UpdateState(StateManager mob, Enemy enemy)
    {
        enemy.agent.SetDestination(enemy.player.position);

        if (enemy.playerInAttackRange) mob.SwitchState(mob.attacking);

        else if (!enemy.playerInSightRange) mob.SwitchState(mob.patrolling);
    }
}
