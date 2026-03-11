using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEngine.UI;

public class CameraSwitching : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private int priorityBoostAmount = 10;
    [SerializeField] private Canvas PlayerCanvas;
    [SerializeField] private Canvas AimCanvas;


    private CinemachineCamera aimCamera;
    private InputAction aimAction;

    private void Awake()
    {
        aimCamera = GetComponent<CinemachineCamera>();
        aimAction = playerInput.actions["Aim"];
        PlayerCanvas.enabled = true;
        AimCanvas.enabled = false;
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
        AimCanvas.enabled = true;
        PlayerCanvas.enabled = false;
    }

    private void CancelAim()
    {
        aimCamera.Priority -= priorityBoostAmount;
        AimCanvas.enabled = false;
        PlayerCanvas.enabled = true;
    }
    // adds 10 to the priority order in order to ensure it is well above the current highest (which is 2)

}
