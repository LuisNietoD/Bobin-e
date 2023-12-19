using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{

    [Header("Patrolling")]
    [Space]
    public NavMeshAgent agent;
    [Space]
    public Vector3 walkPoint;
    public bool walkPointSet;
    [Space]
    public float walkPointRange;
    public bool playerInSightRange;
    [Space]
    public LayerMask whatIsGround, whatIsPlayer;


    [Header("Chasing")]
    [Space]
    public Transform player;
    public float sightRange;

    [Header("Bird")]
    [Space]
    public List<GameObject> rats = new List<GameObject>();
    [Space]
    public GameObject caughtRat;
    public GameObject ratAgent;
    [Space]
    public LayerMask ratLayer;
    [Space]
    public bool birdchasing;
    public bool gotRat;

    [Header("Attacking")]
    [Space]
    public float attackRange;
    public GameObject attackPattern;
    public bool playerInAttackRange;


    private void Start()
    {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Rat") && !gotRat && birdchasing)
        {
            rats.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            gotRat = true;
        }
    }


    public void ThrowRat()
    {
        Debug.Log("Rat jet�");
        Rigidbody rb = Instantiate(ratAgent, caughtRat.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        gotRat = false;
        caughtRat.SetActive(false);
    }

    public void MakeRatList()
    {
        rats.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, ratLayer);

        foreach (Collider collider in colliders)
        {
            rats.Add(collider.gameObject);
        }

        rats.Reverse();
    }
}
