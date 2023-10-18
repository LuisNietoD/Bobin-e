using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogGenerator : MonoBehaviour
{
    public int numberOfTeeth = 10;  // Number of teeth in the cog
    public float cylinderRadius = 2f;  // Radius of the base cylinder
    public float cogDepth = 0.5f;  // Depth of the cog teeth
    public float toothLength = 1.0f;  // Length of the cog teeth

    void Start()
    {
        GenerateCog();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GenerateCog();
        }
    }

    void GenerateCog()
    {
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.parent = transform;
        cylinder.transform.localScale = new Vector3(cylinderRadius * 2, cogDepth, cylinderRadius * 2);

        for (int i = 0; i < numberOfTeeth; i++)
        {
            float angle = i * 360f / numberOfTeeth;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 position = rotation * Vector3.forward * cylinderRadius;

            GameObject tooth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tooth.transform.parent = transform;
            tooth.transform.localScale = new Vector3(cylinderRadius * 0.2f, cogDepth, toothLength);  // Adjust tooth length here
            tooth.transform.localPosition = position;
            tooth.transform.localRotation = rotation;
        }
    }
}