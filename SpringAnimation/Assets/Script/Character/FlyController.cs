using UnityEngine;

public class FlyController : MonoBehaviour
{
    public float flyingSpeed = 10.0f;
    public float verticalSpeed = 5.0f;
    public float rotationSpeed = 2.0f; // Adjust this for rotation speed
    public float tiltAmount = 30.0f; // Adjust this for the forward tilt

    private Rigidbody rb;
    private Transform playerCameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float upInput = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
        float downInput = Input.GetKey(KeyCode.LeftControl) ? -1.0f : 0.0f;
        float forwardInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = GetMoveDirection(horizontalInput, upInput, downInput, forwardInput);
        ApplyForces(moveDirection);

        // Rotate the character in the direction of movement
        //RotateCharacter(moveDirection);
    }

    private Vector3 GetMoveDirection(float horizontalInput, float upInput, float downInput, float forwardInput)
    {
        Vector3 forward = playerCameraTransform.forward;
        Vector3 right = playerCameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * forwardInput + right * horizontalInput).normalized;
        moveDirection.y = upInput + downInput;
        return moveDirection;
    }

    private void ApplyForces(Vector3 moveDirection)
    {
        rb.AddForce(moveDirection * flyingSpeed);
        rb.AddForce(-rb.velocity * flyingSpeed * 0.1f); // Dampening force

        if (rb.velocity.magnitude > flyingSpeed)
        {
            rb.velocity = rb.velocity.normalized * flyingSpeed;
        }
    }

    private void RotateCharacter(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

     
    }
}
