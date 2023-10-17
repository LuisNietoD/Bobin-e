using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSettings : MonoBehaviour
{
    public static CharacterSettings instance;
    
    [Header("Settings")]
    public float jumpForce = 10f;
    public float speed = 5f;
    public float rotationSpeed = 0.2f;
    public float retractDuration = 0.3f;
    public float gravity = -9.81f;
    
    public Rigidbody rb;
    public Camera playerCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
