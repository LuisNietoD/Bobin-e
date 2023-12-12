using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour, AttackBehavior
{
    public GameObject bombPrefab;
    public bool napalm;
    public float cooldown = 3;
    private float _elapsedTime = 0;
    public Transform spawnPoint;
    public float power = 10;
    public string weaponName;
    
    // Start is called before the first frame update
    void Start()
    {
        Name = weaponName;
    }

    public string Name { get; set; }
    public void Action()
    {
        if (GameManager.isPlayerLock || _elapsedTime < cooldown)
            return;
        
        GameObject b = Instantiate(bombPrefab, spawnPoint.transform.position, Quaternion.identity);
        b.transform.forward = spawnPoint.forward;
        b.GetComponent<BombBehavior>().power = power;
        b.GetComponent<BombBehavior>().napalm = napalm;
        _elapsedTime = 0;
    }

    public void StartMethods(GameObject slot)
    {
    }

    public void UpdateMethods()
    {
        _elapsedTime += Time.deltaTime;
    }
    
    
}
