using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private float verticalVelocity;
    private float groundedTimer;

    [Header("Player Settings")]
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = 9.81f;
    public float rotationSpeed = 5.0f;

    [Header("Player Info")]
    public int playerHealth = 100;
    public string playerName = "Hero";

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("CharacterController component is missing!");
        }

        Debug.Log($"Player: {playerName}, Health: {playerHealth}, Speed: {playerSpeed}");
    }

    void Update()
    {
        bool groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            groundedTimer = 0.2f;
        }

        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }

        if (groundedPlayer && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity -= gravityValue * Time.deltaTime;

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(inputX, 0, inputZ);
        Vector3 worldMove = transform.TransformDirection(move) * playerSpeed;

        Vector3 flatMove = new Vector3(worldMove.x, 0, worldMove.z);
        if (flatMove.magnitude > 0.1f && inputZ >= 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && groundedTimer > 0f)
        {
            groundedTimer = 0f;
            verticalVelocity = Mathf.Sqrt(jumpHeight * 2f * gravityValue);
        }

        worldMove.y = verticalVelocity;
        controller.Move(worldMove * Time.deltaTime);
    }
}