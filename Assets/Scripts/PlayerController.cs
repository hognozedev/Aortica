using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
//this script will only work if the object has a CC and PI 

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 3.0f;
    [SerializeField]
    private float sprintSpeed = 5f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 2f;
    
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    public InputActionReference moveAction;
    public InputActionReference aimAction;
    public InputActionReference sprintAction;
    public InputActionReference attackAction;
    public InputActionReference interactAction;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        PlayerInput input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }
// to toggle movement, e.g. if entering a pause menu

    void Update()
    {
        transform.position = cameraTransform.position;
        transform.rotation = Quaternion.Euler(cameraTransform.eulerAngles);

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
// gravity


        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        controller.Move(move * Time.deltaTime * playerSpeed);
        // use the vec2 to create a new vec3 where vertical movement is locked to 0 (change for jumping)

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (rotationSpeed) * Time.deltaTime);
        }


    }
}