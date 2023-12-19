using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HealthEnemy : MonoBehaviour
{
    public int life = 2;
    public float invincibilityTime = 0.3f;
    public float invincibilityActual = 0.6f;
    public string tagAttack;
    public bool enemy;
    public int maxLoot;
    public GameObject loot;

    private GameObject model;
    

    private void Start()
    {
        model = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        invincibilityActual -= Time.deltaTime;
        if (invincibilityActual <= invincibilityTime / 2 && GetComponent<NavMeshAgent>().isStopped)
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (invincibilityActual <= 0 && other.CompareTag(tagAttack))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            
            invincibilityActual = invincibilityTime;
            life--;
            if (life <= 0)
            {
                if(enemy)
                    Die();
                else
                {
                    
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }

    public void Die()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        GetComponent<StateManager>().enabled = false;
        if(GetComponent<CloseAttack>())
            GetComponent<CloseAttack>().enabled = false;
        if(GetComponent<RangeAttack>())
            GetComponent<RangeAttack>().enabled = false;

        
        Destroy(GetComponent<Rigidbody>());
        Destroy(transform.GetChild(1).gameObject);
        
        transform.GetChild(0).GetComponent<Disassemble>().DestroyObject();

        int lootCount = Random.Range(0, maxLoot);
        Vector3 lootPos = transform.position;
        lootPos.y += 2;
        for (int i = 0; i <= lootCount; i++)
        {
            Instantiate(loot, lootPos, Quaternion.identity);
        }
        
        Destroy(this);
    }
}
