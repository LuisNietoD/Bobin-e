using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Wazo : MonoBehaviour, IEnemy
{
    public int vie = 80;
    
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer, whatIsRat;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool ratInSightRange;
    public Collider[] rats;
    public bool gotRat;
    public GameObject catchedRat;
    public GameObject ratAgent;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        rats = Physics.OverlapSphere(transform.position, sightRange, whatIsRat);
        if (rats.Length > 0)
            ratInSightRange = true;
        
        if (!ratInSightRange || (!playerInSightRange && gotRat)) Patroling();
        if (ratInSightRange && !gotRat && rats.Length > 0) ChaseRat();
        if (!playerInAttackRange && playerInSightRange && gotRat) ChasePlayer();
        if (playerInAttackRange && gotRat) AttackPlayer();
    }

    private void Patroling()
    {
        Debug.Log("patrolling");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        Debug.Log("chasePlayer");

        agent.SetDestination(player.position);
    }
    
    private void ChaseRat()
    {
        Debug.Log("Chase Rat");

        if(rats.Length > 0)
            agent.SetDestination(rats[0].transform.position);
    }

    private void AttackPlayer()
    {
        Debug.Log("Attack");

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            //Attack code here
            Rigidbody rb = Instantiate(ratAgent, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            gotRat = false;
            catchedRat.SetActive(false);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rat") && !gotRat)
        {
            
            Destroy(other.gameObject);
            catchedRat.SetActive(true);
            gotRat = true;
        }    
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        vie -= damage;
        Debug.Log("Wazo a perdu des pv");
        if (vie <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Wazo est mort");
    }

}
