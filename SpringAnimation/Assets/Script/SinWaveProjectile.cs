using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SinWaveProjectile : MonoBehaviour
{
    public Vector3 endPoint;
    public Vector3 startPoint;
    public float amplitude = 5;
    public float speed = 4;
    private float t = 0;
    public GameObject destroyEffect;
        
    private void Start()
    {
        startPoint = transform.position;
        endPoint = GameManager.instance.player.position;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * speed;
        if (t > 1.0f)
        {
            t = 1.0f;
        }

        transform.position = GameManager.CalculateSineWavePoint(t, startPoint, endPoint, amplitude);
        transform.LookAt(Vector3.Lerp(startPoint, endPoint, 0.5f), transform.up);
        
        if (Vector3.Distance(transform.position, endPoint) <= 0.3f)
        {
            DestroyEffectSpawn(transform.position);
            Destroy(gameObject);
        }
    }

    public void DestroyEffectSpawn(Vector3 pos)
    {
        Instantiate(destroyEffect, pos, Quaternion.identity);
    }
}
