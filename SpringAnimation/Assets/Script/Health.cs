using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int life = 2;
    [Space]
    public float invincibilityTime = 0.3f;
    public string tagAttack;
    [Space] public Animator modelAnimator;
    public GameObject objectExplode;
    public MMF_Player sceneLoader;
    
    private bool hit;
    private float invincibilityActual = 0.3f;

    private void Update()
    {
        invincibilityActual -= Time.deltaTime;
        if (invincibilityActual <= invincibilityTime / 2 && hit)
        {
            hit = false;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("AIIIIIIIE");
        if (invincibilityActual <= 0 && other.CompareTag(tagAttack))
        {
            Damage();
        }

        if (other.CompareTag("Void"))
        {
            sceneLoader.PlayFeedbacks();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<SinWaveProjectile>(out var c))
        {
            c.DestroyEffectSpawn(collision.contacts[0].point);
            Destroy(c.gameObject);
            StartCoroutine(GetComponent<PlayerController>().camShake.Vibrate(0.3f, 0.4f));
            Damage();
        }
    }

    public void Damage()
    {
        hit = true;
        invincibilityActual = invincibilityTime;
        life--;
        if (life <= 0)
        {
            GameManager.isPlayerLock = true;
            sceneLoader.PlayFeedbacks();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
