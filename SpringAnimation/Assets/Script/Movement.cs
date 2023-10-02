using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float speed = 5f;
    public float rotationSpeed = 0.2f;
    public float gravity = -9.81f;
    
    public Camera playerCamera;
    public MeshMorphing spring;
    public AnimationCurve springEffect;
    public AnimationCurve preGround;
    
    public float maxRayDistance = 2f; 
    public float minRayDistance = 0.5f;

    public float extendDuration = 0.3f;
    public bool retracted = true;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        
        RetractTest();

        Physics.gravity = new Vector3(0, gravity, 0);
    }

    void Move()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0; 

        Vector3 moveDirection = cameraForward.normalized * vertical + playerCamera.transform.right * horizontal;

        // Rotate the character
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the character
        Vector3 velocity = moveDirection.normalized * speed;
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
    }

    void RetractTest()
    {
        //Shoot a ray down
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, maxRayDistance))
        {
            //Check if you hit the ground
            GameObject hitObject = hit.collider.gameObject;
            Debug.DrawLine(rayOrigin, hit.point, Color.red);
            if (hitObject.CompareTag("Ground"))
            {
                // Calculate the weight using linear interpolation
                float weight = Mathf.Lerp(0f, 0.3f, Mathf.InverseLerp(minRayDistance, maxRayDistance, hit.distance));

                // Clamp the weight to the desired range
                weight = Mathf.Clamp(weight, 0f, 0.3f);

                spring.m_MorphTargets[0].Weight = weight;
            }
            else
            {
                spring.m_MorphTargets[0].Weight = 0.3f;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //Start extend when collide the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            _rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            //StopCoroutine(RetractSpring());
            //StartCoroutine(ExtendSpring());
        }
    }

    //Extend animation curve based
    IEnumerator ExtendSpring()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < extendDuration)
        {
            elapsedTime += Time.deltaTime;
            spring.m_MorphTargets[0].Weight = springEffect.Evaluate(elapsedTime/extendDuration);
            yield return null;
        }

        retracted = false;
    }
    
    //retract animation curve based
    IEnumerator RetractSpring()
    {
        float elapsedTime = 0.0f;
        while (spring.m_MorphTargets[0].Weight > 0)
        {
            elapsedTime += Time.deltaTime;
            spring.m_MorphTargets[0].Weight = preGround.Evaluate(elapsedTime/extendDuration);
            yield return null;
        }
    }
}
