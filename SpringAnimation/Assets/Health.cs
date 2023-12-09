using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int life = 2;
    public float invincibilityTime = 0.3f;
    public float invincibilityActual = 0.6f;
    public string tag;
    public bool hit;

    

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

            transform.DOLocalMoveY(0.5f, 0.2f)
                .SetEase(Ease.OutQuad) // Choose an ease function for the jump
                .OnComplete(() =>
                {
                    // Return to the ground
                    transform.DOLocalMoveY(0, 0.1f)
                        .SetEase(Ease.InQuad)
                        .OnComplete(() =>
                        {
                            hit = false;
                        });
                });
        }
    }
}
