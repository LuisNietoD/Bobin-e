using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GearGenerator : MonoBehaviour
{
    [Range(12, 24)]
    public int m_cogs = 10;
    public GameObject m_cog;
    public GameObject m_body;
    private float m_radiusFactor = 21f;
    private float m_bodyscale = 1.75f;
    public Material gearMaterial;

    private void Start()
    {
        Generate();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Generate();
        }
    }

    public void Generate()
    {
        float step = 360f / (float)m_cogs;
        float radius = m_cogs / m_radiusFactor;

        for (int i = 0; i < m_cogs; i++)
        {
            var cog = Instantiate(m_cog, transform.position, Quaternion.identity);
            cog.transform.parent = transform;
            cog.transform.eulerAngles = new Vector3(0, 0, step * i + 90);
            cog.transform.position += cog.transform.up * radius;
            cog.GetComponent<Renderer>().material = gearMaterial;
        }

        float bodyScale = (radius * 1.2f) * m_bodyscale;
        var body = Instantiate(m_body, transform.position, Quaternion.identity);
        body.transform.parent = transform;

        body.transform.localPosition = new Vector3(0, 0, 0.08f);
        body.transform.localScale = new Vector3(bodyScale, 0.1f, bodyScale);
        body.transform.eulerAngles = new Vector3(90, 0, 0);
        body.GetComponent<Renderer>().material = gearMaterial;

        
        //collider
        float colliderS = 2.4f + ((1 - (m_cogs - 12) / (24 - 12)) * 0.2f);
        float colliderScale = (radius * 1f) * colliderS;
        var collider = Instantiate(m_body, transform.position, Quaternion.identity);
        collider.transform.parent = transform;

        collider.transform.localPosition = new Vector3(0, 0, 0.08f);
        collider.transform.localScale = new Vector3(colliderScale, 0.1f, colliderScale);
        collider.transform.eulerAngles = new Vector3(90, 0, 0);
        collider.GetComponent<MeshRenderer>().enabled = false;

        GetComponent<Gear>().cogs = m_cogs;
    }
}
