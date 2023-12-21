using DG.Tweening;
using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float rotationSpeed = 0.2f;
    public float jumpForce = 8;
    public float jumpForwardBrake = 2;
    
    bool chargingJump = false;
    float chargeStartTime;
    public float maxChargeTime = 2.0f; // Maximum charge time in seconds
    public float minJumpForce = 8.0f; // Minimum jump force
    public float maxJumpForce = 12.0f; // Maximum jump force
    
    private Vector3 velocityBonus;

    [Header("Feedback")]
    public GameObject walkVFX;

    public GameObject chargedVFX;
    public ThirdPersonCamera camShake;
    [Space] public AudioSource audioBoing;
    
    [Header("Needed references")]
    public Camera playerCamera;
    public Animator modelAnimator;

        [HideInInspector]
    public bool grounded = true;

    private Rigidbody _rb;
    
    private void OnEnable()
    {
        MenuManager.OnOpenPauseMenu += DisableAnimation;
        MenuManager.OnClosePauseMenu += EnableAnimation;
    }

    private void OnDisable()
    {
        MenuManager.OnOpenPauseMenu -= DisableAnimation;
        MenuManager.OnClosePauseMenu -= EnableAnimation;
    }

    private void Start() => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        if (GameManager.isPlayerLock)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        Move();
    }

    private void Move()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0;
        Vector3 moveDirection = cameraForward.normalized * vertical + playerCamera.transform.right * horizontal;
        
        RotateCharacter(moveDirection);

        // Move the character
        Vector3 velocity = moveDirection.normalized * speed;
        
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            chargingJump = true;
            chargeStartTime = Time.time;
            //transform.DOScaleY(0.7f, 2f);
        }

        if (Time.time - chargeStartTime > 0.5f && chargingJump)
        {
            chargedVFX.SetActive(true);
            camShake.vibrating = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && chargingJump)
        {
            float chargeTime = Time.time - chargeStartTime;
            float normalizedCharge = Mathf.Clamp(chargeTime / maxChargeTime, 0f, 1f);
            float chargedJumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, normalizedCharge);

            Debug.Log(chargedJumpForce);
            Jump(new Vector3(moveDirection.x / jumpForwardBrake, 1, moveDirection.z / jumpForwardBrake), chargedJumpForce);

            chargingJump = false;
            chargedVFX.SetActive(false);
            camShake.vibrating = false;
            audioBoing.Play();
            //transform.localScale = Vector3.one;
        }
        
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);

        //If no input stop instantly the character
        if (horizontal == 0 && vertical == 0)
        {
            //_rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            modelAnimator.SetBool("isMoving", false);
        }
        else
        {
            modelAnimator.SetBool("isMoving", true);
        }
    }

    public void Jump(Vector3 dir, float force) => _rb.AddForce(force * dir, ForceMode.Impulse);
    
    
    //Ground check
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            //TargetPosition.y = transform.position.y;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void RotateCharacter(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void EnableAnimation()
    {
        modelAnimator.enabled = true;
        GameManager.isPlayerLock = false;
    }

    public void DisableAnimation()
    {
        modelAnimator.enabled = false;
        GameManager.isPlayerLock = true;
    }
}
