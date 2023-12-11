using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flamethrower : MonoBehaviour, AttackBehavior
{
    public ParticleSystem flamethrowerParticles;
    public float castRadius = 0.5f; // Radius of the sphere cast.
    public float castDistance = 10f; // Distance for the sphere cast.
    public LayerMask hitLayerMask;
    public Transform castPoint; // Transform reference for the starting point of the cast.
    public Collider collideAttack;
    private bool isFlamethrowerActive = false;

    public void StartMethods(GameObject slot)
    {
        hitLayerMask = GameManager.instance.enemyMask;
        castPoint = transform.parent;
        flamethrowerParticles.gameObject.SetActive(true);
        collideAttack = flamethrowerParticles.transform.GetChild(3).GetComponent<Collider>();
        collideAttack.enabled = false;
        flamethrowerParticles.Stop();
    }

    public void UpdateMethods()
    {
    }

    public string Name { get; set; }

    private void Start()
    {
        Name = "Flamethrower";
    }

    public void Action()
    {
        isFlamethrowerActive = !isFlamethrowerActive;

        if (isFlamethrowerActive)
        {
            flamethrowerParticles.Play();
            collideAttack.enabled = true;
        }
        else
        {
            flamethrowerParticles.Stop();
            collideAttack.enabled = false;
        }
    }
}
