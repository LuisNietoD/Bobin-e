using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class HealthEnemy : MonoBehaviour
{
    public int life = 2;
    public float invincibilityTime = 0.3f;
    public float invincibilityActual = 0.6f;
    public string tag;
    public bool enemy;

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
        if (invincibilityActual <= 0 && other.CompareTag(tag))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            
            invincibilityActual = invincibilityTime;
            life--;
            if (life <= 0)
            {
                if(enemy)
                    Destroy(gameObject);
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

            model.transform.DOLocalMoveY(1, 0.4f)
                .SetEase(Ease.OutQuad) // Choose an ease function for the jump
                .OnComplete(() =>
                {
                    // Return to the ground
                    model.transform.DOLocalMoveY(0, 0.2f)
                        .SetEase(Ease.InQuad)
                        .OnComplete(() =>
                        {
                            GetComponent<NavMeshAgent>().isStopped = false;
                        });
                });
        }
    }
}
