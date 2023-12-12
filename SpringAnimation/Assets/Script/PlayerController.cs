using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float rotationSpeed = 0.2f;
    public float jumpForce = 8;
    public float jumpForwardBrake = 2;
    private Vector3 velocityBonus;
    
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

        //if (_rb.velocity.x < -speed || _rb.velocity.x > speed)
           // velocity.x = _rb.velocity.x;
        
       // if (_rb.velocity.z < -speed || _rb.velocity.z > speed)
          //  velocity.z = _rb.velocity.z;

          //_rb.AddForce(velocity, ForceMode.Force);
        _rb.velocity = new Vector3(velocity.x, _rb.velocity.y, velocity.z);
        
        bool doJump = Input.GetKeyDown(KeyCode.Space) && grounded;
        
        if (doJump)
        {
            Jump(new Vector3(moveDirection.x/jumpForwardBrake, 1, moveDirection.z/jumpForwardBrake));
        }

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

    public void Jump(Vector3 dir) => _rb.AddForce(jumpForce * dir, ForceMode.Impulse);
    
    
    
    //Ground check
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            TargetPosition.y = transform.position.y;
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
