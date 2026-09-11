using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
    public int currentWeapon = 0;
    public PlayerController playerController;

    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        //scrollAction = playerInput.actions["Scroll"];

        //playerController = GetComponent<PlayerController>();

        SelectWeapon();

    }

    void Update()
    {
        float y = playerController.scrollAction.ReadValue<float>();
        int previousWeapon = currentWeapon;

        if (y > 0)
        {
            if(currentWeapon >= transform.childCount - 1)   currentWeapon = 0;
            else    currentWeapon++;
        }

        else if (y < 0)
        {
            if(currentWeapon <= 0)      currentWeapon = transform.childCount - 1;
            else    currentWeapon--;
        }

        if (playerController.oneAction.WasPressedThisFrame()) currentWeapon = 0;
        if (playerController.twoAction.WasPressedThisFrame() && transform.childCount >= 2 ) currentWeapon = 1;
        if (playerController.threeAction.WasPressedThisFrame() && transform.childCount >= 3 ) currentWeapon = 2;

        if (previousWeapon != currentWeapon) SelectWeapon();

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
                //weapon.gameObject.TryGetComponent<GunMaster>(out GunMaster gunMaster); gunMaster.UpdateHUD();
            //show correct weapon name/ ammo UI overlay
            }

            else weapon.gameObject.SetActive(false);

            i++;
        }
    //loop through each weapon, see if it matches the current one (i), if so sets active, and if not hides it.

    }

}