using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, AttackBehavior
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    
    public float speed = 5.0f;
    public float amplitude = 2.0f;
    
    public GameObject explosion;
    public GameObject bomb;
    
    public float DiveCooldown = 5;
    private float _elapsedTime = 0;
    public GameObject aim;
    public bool attacking;
    private float t = 0.0f;
    public Transform player;
    
    private void Start()
    {
        Name = "Bomb";
        transform.GetChild(0).gameObject.SetActive(false);
        player = GameManager.instance.player;
        aim = player.GetComponent<AimScript>().aim;
    }

    public string Name { get; set; }
    public void Action()
    {
        if (GameManager.isPlayerLock || _elapsedTime < DiveCooldown || !aim.activeSelf)
            return;
        
        GameManager.isPlayerLock = true;
        attacking = true;
        startPoint = player.position;
        endPoint = aim.transform.position;
    }

    public void StartMethods(GameObject slot)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UpdateMethods()
    {
        
    }

    public void FixedUpdate()
    {
        if (_elapsedTime < DiveCooldown)
        {
            _elapsedTime += Time.deltaTime;
            bomb.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, _elapsedTime / DiveCooldown);
        }
        
        if (attacking)
        {
            t += Time.deltaTime * speed;
            if (t > 1.0f)
            {
                t = 1.0f;
            }
            
            Vector3 position = GameManager.CalculateSineWavePoint(t, startPoint, endPoint, amplitude);
            player.LookAt(Vector3.Lerp(startPoint, endPoint, 0.5f), player.up);
            player.position = position;
            
            
            if (Mathf.Abs(player.position.x - endPoint.x )< 0.4f && Mathf.Abs(player.position.z - endPoint.z )< 0.4f)
            {
                attacking = false;
                GameManager.isPlayerLock = false;
                _elapsedTime = 0;
                bomb.transform.localScale = Vector3.zero;
                GameObject e = Instantiate(explosion, endPoint, Quaternion.identity);
                e.transform.localScale *= 5;
                t = 0;
            }
        }
    }
}
