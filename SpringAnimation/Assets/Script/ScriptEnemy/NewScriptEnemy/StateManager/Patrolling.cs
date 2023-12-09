using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Patrolling : BaseState
{


    public override void EnterState(StateManager mob, Enemy enemy)
    {
        
    }

    public override void UpdateState(StateManager mob, Enemy enemy)
    {
        
        Run(mob, enemy);

        if (enemy.playerInSightRange && !enemy.CompareTag("Bird")) mob.SwitchState(mob.chasing);

        if (enemy.CompareTag("Bird") && enemy.playerInSightRange) mob.SwitchState(mob.birdCatchRat);

    }


    public void Run(StateManager mob, Enemy enemy)
    {

        if (!enemy.walkPointSet)
        {
            SearchWalkPoint(mob, enemy);
        }

        if (enemy.walkPointSet) {

            enemy.agent.SetDestination(enemy.walkPoint);

        }
            

        Vector3 distanceToWalkPoint = mob.transform.position - enemy.walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2f)
            enemy.walkPointSet = false;
    }



    public void SearchWalkPoint(StateManager mob, Enemy enemy)
    {
        float randomZ = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);
        float randomX = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);

        enemy.walkPoint = new Vector3(mob.transform.position.x + randomX, mob.transform.position.y, mob.transform.position.z + randomZ);


        if (Physics.Raycast(enemy.walkPoint, -mob.transform.up, 5f, enemy.whatIsGround))
            enemy.walkPointSet = true;

    }
}
