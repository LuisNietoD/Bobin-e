using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ModelExplode : MonoBehaviour
{
    float inflationSpeed = 5f;
    public static Mesh mesh;
    Vector3[] originalVertices ;
    Vector3[] currentVertices ;
    public GameObject explodePrefab;
    public float explodeScale = 1; 

    public float timeExplode = 0.5f;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        currentVertices = mesh.vertices;
    }

    void InflateMesh() {
        Vector3 center = Vector3.zero; // Center point for the sphere

        for (int i = 0; i < currentVertices.Length; i++) {
            // Calculate direction towards the center of the sphere
            Vector3 direction = (center - currentVertices[i]).normalized;

            // Calculate the distance from the center
            float distance = Vector3.Distance(currentVertices[i], center);

            // Define a desired distance from the center (radius of the sphere)
            float sphereRadius = 0.4f; // Change this value as needed

            // Move vertices towards a sphere shape
            currentVertices[i] -= direction * (sphereRadius - distance) * inflationSpeed * Time.deltaTime;
        }

        mesh.vertices = currentVertices;
        mesh.RecalculateNormals(); // Update normals for lighting
    }

    void ResetMesh()
    {
        mesh.vertices = originalVertices;
        mesh.RecalculateNormals();
    }
    

    private void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            timeExplode -= Time.deltaTime;
            if (timeExplode <= 0)
            {
                GameObject e = Instantiate(explodePrefab, transform.position, quaternion.identity);
                e.transform.localScale = new Vector3(explodeScale, explodeScale, explodeScale);
                Destroy(gameObject);
            }

            InflateMesh();
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            ResetMesh();
        }
    }
    
    
}
