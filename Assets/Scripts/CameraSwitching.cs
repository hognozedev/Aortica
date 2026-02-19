using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraSwitching : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int priorityBoostAmount = 10;


    private CinemachineCamera aimCamera;
    private InputAction aimAction;

    private void Awake()
    {
        aimCamera = GetComponent<CinemachineCamera>();
        aimAction = playerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();

    }

    private void StartAim()
    {
        aimCamera.Priority += priorityBoostAmount;
    }

    private void CancelAim()
    {
        aimCamera.Priority -= priorityBoostAmount;
    }
        // adds 10 to the priority order in order to ensure it is well above the current highest (which is 2)

}
