using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : BaseState
{


    public override void EnterState(StateManager mob, Enemy enemy)
    {
        if (!enemy.CompareTag("Bird") && enemy.attackPattern != null) 
            enemy.attackPattern.SetActive(true);


        
    }

    public override void UpdateState(StateManager mob, Enemy enemy)
    {
        Debug.Log("atack");
        enemy.agent.SetDestination(enemy.transform.position);

        Vector3 aim = new Vector3(enemy.player.position.x, enemy.transform.position.y, enemy.player.position.z);
        enemy.transform.LookAt(aim);
        if (mob.range != null)
        {
            mob.range.enabled = true;
        }
            

        if (!enemy.playerInAttackRange && !enemy.CompareTag("Bird"))
        {
            if(enemy.attackPattern!= null)
                enemy.attackPattern.SetActive(false);
            if (mob.range != null)
            {
                mob.range.enabled = false;
            }
            mob.SwitchState(mob.chasing);
        }

        if (enemy.CompareTag("Bird")) enemy.ThrowRat();

        if (!enemy.gotRat && enemy.CompareTag("Bird"))
        {
            mob.SwitchState(mob.birdCatchRat);
        }
            

    }
}
