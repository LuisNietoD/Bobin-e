using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour, AttackBehavior
{
    public string Name { get; set; }
    
    public float hoverForce = 10f; // Adjust this to change the hover force
    public float rotateSpeed = 2;

    private Rigidbody rb;
    private PlayerController playerController;
    public float hoverTimeDefault = 3;
    private float hoverTime = 3;
    public bool hover;

    void Start()
    {
        rb = GameManager.instance.player.GetComponent<Rigidbody>();
        Name = "Propeller";
        playerController = GameManager.instance.player.GetComponent<PlayerController>();
    }

    public void Action()
    {
    }
    

    public void StartMethods(GameObject slot)
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UpdateMethods()
    {
        if (!playerController.grounded && Input.GetKeyDown(KeyCode.Space))
        {
            hover = true;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }

        if (hoverTime > 0 && Input.GetKey(KeyCode.Space) && hover)
        {
            hoverTime -= Time.deltaTime;
            if (Input.GetKey(KeyCode.Space) && !playerController.grounded)
            {
                rb.AddForce(hoverForce * Vector3.up, ForceMode.Acceleration);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + rotateSpeed, 0);
            }
        }
        else if (playerController.grounded)
        {
            hover = false;
            hoverTime = hoverTimeDefault;
        }
    }
}
