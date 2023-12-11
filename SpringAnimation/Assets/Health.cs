using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int life = 2;
    [Space]
    public float invincibilityTime = 0.3f;
    public string tag;
    [Space] public Animator modelAnimator;
    
    private bool hit;
    private float invincibilityActual = 0.3f;

    private void Update()
    {
        invincibilityActual -= Time.deltaTime;
        if (invincibilityActual <= invincibilityTime / 2 && hit)
        {
            hit = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("AIIIIIIIE");
        if (invincibilityActual <= 0 && other.CompareTag(tag))
        {
            hit = true;
            invincibilityActual = invincibilityTime;
            life--;
            if (life <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (other.CompareTag("Void"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
