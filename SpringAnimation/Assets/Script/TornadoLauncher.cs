using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoLauncher : MonoBehaviour, AttackBehavior
{
    public string Name { get; set; }
    public Transform spawnPoint;
    public GameObject tornadoPrefab;
    public Animator animator;
    public LayerMask groundLayer;

    private void Start()
    {
        Name = "Tornado";
    }

    public void Action()
    {
        animator.Play("ShootTornado");
        Vector3 spawnPos = spawnPoint.position + Vector3.down * 2f;
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint.position, Vector3.down, out hit, 2f, groundLayer))
        {
            spawnPos = hit.point;
        }
        GameObject t = Instantiate(tornadoPrefab, spawnPos, Quaternion.identity);
        
        t.transform.localScale = Vector3.zero;
        t.transform.forward = spawnPoint.forward;
    }

    public void StartMethods(GameObject slot)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UpdateMethods()
    {
    }
}
