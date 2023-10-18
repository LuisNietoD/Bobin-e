using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public ParticleSystem flamethrowerParticles;
    public float castRadius = 0.5f; // Radius of the sphere cast.
    public float castDistance = 10f; // Distance for the sphere cast.
    public LayerMask hitLayerMask;
    public Transform castPoint; // Transform reference for the starting point of the cast.

    private bool isFlamethrowerActive = false;

    private void Start()
    {
        flamethrowerParticles.Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleFlamethrower();
        }

        if (isFlamethrowerActive)
        {
            FireFlamethrower();
        }
    }

    private void ToggleFlamethrower()
    {
        isFlamethrowerActive = !isFlamethrowerActive;

        if (isFlamethrowerActive)
        {
            flamethrowerParticles.Play();
        }
        else
        {
            flamethrowerParticles.Stop();
        }
    }

    private void FireFlamethrower()
    {
        if (castPoint == null)
        {
            Debug.LogError("Cast point not assigned.");
            return;
        }

        Vector3 castOrigin = castPoint.position;
        RaycastHit[] hits = Physics.SphereCastAll(castOrigin, castRadius, castPoint.forward, castDistance, hitLayerMask);

        foreach (RaycastHit hit in hits)
        {
            // You can apply damage or other effects to the hit object here.
            Debug.DrawLine(castOrigin, hit.point, Color.red);
        }
    }
}
