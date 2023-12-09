using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flamethrower : AttackBehavior
{
    public ParticleSystem flamethrowerParticles;
    public float castRadius = 0.5f; // Radius of the sphere cast.
    public float castDistance = 10f; // Distance for the sphere cast.
    public LayerMask hitLayerMask;
    public Transform castPoint; // Transform reference for the starting point of the cast.
    public CoilSlot cs;
    public Collider collideAttack;

    private bool isFlamethrowerActive = false;

    public override void StartMethods(GameObject slot)
    {
        cs = slot.GetComponent<CoilSlot>();
        flamethrowerParticles = cs.flame;
        hitLayerMask = cs.enemyMask;
        castPoint = cs.transform;
        flamethrowerParticles.gameObject.SetActive(true);
        collideAttack = flamethrowerParticles.transform.GetChild(3).GetComponent<Collider>();
        collideAttack.enabled = false;
        flamethrowerParticles.Stop();
    }

    public override void UpdateMethods()
    {
    }

    public override void Action()
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
