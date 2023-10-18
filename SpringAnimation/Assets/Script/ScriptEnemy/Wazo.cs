using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Wazo : MonoBehaviour, IEnemy
{
    public int vie = 80;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public GameObject NewRat;
    public GameObject Rat;

    public bool AttaqueDeRat;

    public bool ready = false;

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

        if (!playerInSightRange && !playerInAttackRange) ChaseRat();
        if (!playerInSightRange && !playerInAttackRange && ready) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 5f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y - 4f, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void ChaseRat() {
        // Trouver l'objet rat le plus proche
        GameObject[] rats = GameObject.FindGameObjectsWithTag("rat");
        GameObject closestRat = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject rat in rats)
        {
            float distance = Vector3.Distance(transform.position, rat.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRat = rat;
            }
        }

        if (closestRat != null)
        {
            agent.SetDestination(closestRat.transform.position);
        }
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked && AttaqueDeRat)
        {

            //Attack code here
            /*Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            */
            // 
            NewRat.SetActive(false);
            alreadyAttacked = true;
            AttaqueDeRat = false;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("rat"))
        {
            Debug.Log("Le rat est pris par Wazo");
            // Changer le parent du rat pour qu'il devienne le parent du Wazo
            NewRat.SetActive(true);
            AttaqueDeRat = true;
            ready = true;
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
