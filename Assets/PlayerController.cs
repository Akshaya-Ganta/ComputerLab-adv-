using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;         // Movement speed
    public float gravity = -9.81f;   // Gravity force
    public float jumpHeight = 2f;    // Jump force

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public Transform groundCheck;    // Empty object at player’s feet
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if player is on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keep grounded
        }

        // Get input (WASD)
        float x = Input.GetAxis("Horizontal"); // A, D
        float z = Input.GetAxis("Vertical");   // W, S

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}

