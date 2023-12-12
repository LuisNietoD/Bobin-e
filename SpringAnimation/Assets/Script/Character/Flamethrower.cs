using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flamethrower : MonoBehaviour, AttackBehavior
{
    public ParticleSystem flamethrowerParticles;
    public Collider collideAttack;
    private bool isFlamethrowerActive = false;

    public void StartMethods(GameObject slot)
    {
        transform.GetChild(0).gameObject.SetActive(true);
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
