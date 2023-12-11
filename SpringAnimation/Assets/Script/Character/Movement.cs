using System;
using System.Collections;
using UnityEngine;
using Physics = UnityEngine.Physics;

public class Movement : MonoBehaviour
{
    [Header("Settings")] public float jumpForce = 10f;
    public float speed = 5f;
    public float rotationSpeed = 0.2f;
    public float retractDuration = 0.3f;
    public float gravity = -9.81f;
    public float chargedJump = 40;
    public float chargedTime = 3f;
    public float elapsedChargedTime = 0f;
    public bool jumped;

    [Space] [Header("Needed references")] public Camera playerCamera;
    public MeshMorphing spring;

    [Space] [Header("Raycast settings")] public float maxRayDistance = 2f;
    public float minRayDistance = 0.5f;

    public bool grounded = true;

    private bool _retracted = true;
    public bool _bounced = false;

    private float _restWeight = 0.1f; // Weight for the rest position
    private float _compressedWeight = 0f; // Weight for the compressed position
    private float _extendedWeight = 0.3f; // Weight for the extended position

    private Coroutine _retractCoroutine; // Coroutine to retract the spring


    public Rigidbody _rb;
    private Attack _attackRef;

    public CameraMov camMov;

    // Start is called before the first frame update
    void Start()
    {
        CheckpointManager.instance.respawnPoint = transform.position;

        _rb = gameObject.GetComponent<Rigidbody>();
        _attackRef = GetComponent<Attack>();
        ResetGravity();
    }

    public void ResetGravity()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
    }

    private void Update()
    {
        //if (!GameManager.instance.lockPlayer)
        //{
        //    _rb.velocity = Vector3.zero;
         //   return;
        //}

        if (_attackRef.action == Attack.State.Nothing)
            Move();
        if (_attackRef.action != Attack.State.ChargeJump)
            RetractTest();

        if (Input.GetKeyUp(KeyCode.Space) && !jumped && grounded)
        {
            Debug.Log(elapsedChargedTime);
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            _rb.AddForce(new Vector3(0, 10 + chargedJump * (elapsedChargedTime / chargedTime), 0), ForceMode.Impulse);
            _bounced = true;
            jumped = true;
            elapsedChargedTime = 0;
            //---------spring.m_MorphTargets[0].Weight = 0.04f;
            _retracted = false;
        }

        if (Input.GetKey(KeyCode.Space) && !jumped)
        {
            _bounced = false;
            elapsedChargedTime = Mathf.Clamp(elapsedChargedTime + Time.deltaTime, 0f, chargedTime);
            //---------spring.m_MorphTargets[0].Weight = Mathf.Clamp(0.03f - (elapsedChargedTime / chargedTime) * 0.03f, 0.01f, 0.03f);
        }
    }

    /// <summary>
    /// Move the player
    /// </summary>
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
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the character
        if (!(Input.GetKey(KeyCode.Space) && !grounded))
        {
            Vector3 velocity = moveDirection.normalized * speed;
            _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
        }

        if (horizontal == 0 && vertical == 0)
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        }
    }

    void RetractTest()
    {
        float thickness = 0.5f;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down;
        RaycastHit hit;


        Debug.DrawLine(rayOrigin, rayOrigin + (Vector3.down * maxRayDistance), Color.red);
        if (Physics.SphereCast(rayOrigin, thickness, rayDirection, out hit, maxRayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;


            if (hitObject.CompareTag("Ground") && _bounced)
            {
                //Extend the spring before jumping
                _retracted = false;
                float weight = Mathf.Lerp(_compressedWeight, _extendedWeight,
                    Mathf.InverseLerp(minRayDistance, maxRayDistance, hit.distance));
                weight = Mathf.Clamp(weight, _compressedWeight, _extendedWeight);
                //---------spring.m_MorphTargets[0].Weight = weight;
            }
            else if (hitObject.CompareTag("Ground"))
            {
                //Only retract the spring before landing
                float weight = Mathf.Lerp(_extendedWeight, _compressedWeight,
                    Mathf.InverseLerp(maxRayDistance, minRayDistance, hit.distance));
                weight = Mathf.Clamp(weight, _compressedWeight, spring.m_MorphTargets[0].Weight);
                //---------spring.m_MorphTargets[0].Weight = weight;
            }
        }
        else if (!_retracted)
        {
            _retracted = true;
            _bounced = false;
            // Stop the coroutine if exist
            if (_retractCoroutine != null)
            {
                StopCoroutine(_retractCoroutine);
            }

            _retractCoroutine = StartCoroutine(RetractSpring());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        bool moving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        bool retracted = spring.m_MorphTargets[0].Weight < 0.13 && spring.m_MorphTargets[0].Weight > -0.02f;

        //Start extend and jump when collide the ground
        if (collision.gameObject.CompareTag("Ground") && !_bounced && moving && retracted && !_attackRef.attacking)
        {
            if (_attackRef.flyCoroutine != null)
                StopCoroutine(_attackRef.flyCoroutine);
            ResetGravity();
            if (!Input.GetKey(KeyCode.Space))
            {
                _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                _rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                _bounced = true;
            }

            _attackRef.alreadyFly = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            camMov.me = transform.position.y;
            Debug.Log(camMov.me);

            grounded = true;
            jumped = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }


    //retract animation raycast based
    IEnumerator RetractSpring()
    {
        float elapsedTime = 0f;

        //retract over time
        while (elapsedTime < retractDuration)
        {
            float weight = Mathf.Lerp(_extendedWeight, _restWeight, elapsedTime / retractDuration);
            //---------spring.m_MorphTargets[0].Weight = weight;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the weight to rest weight
        //---------spring.m_MorphTargets[0].Weight = _restWeight;

        _retractCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Void"))
        {
            Respawn();
        }

        if (other.CompareTag("Teleporter"))
        {
            Teleport(other.GetComponent<Teleporter>().goal);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Door"))
        {
            GameManager.instance.ChangeCamera(other.GetComponent<CogGameScript>().cam);
            other.GetComponent<CogGameScript>().HideAll();
        }
    }

    public void Respawn()
    {
        _rb.velocity = Vector3.zero;
        transform.position = CheckpointManager.instance.respawnPoint;
    }

    public void Teleport(Transform goal)
    {
        _rb.velocity = Vector3.zero;
        transform.position = goal.position;
        Vector3 rotate = transform.eulerAngles;
        rotate.y = goal.eulerAngles.y;
        transform.eulerAngles = rotate;
    }

    public void TakeDamage()
    {
        Debug.Log("Outch");
    }
}
