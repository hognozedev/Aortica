using UnityEngine;
using UnityEngine.InputSystem;

public class Rifle : GunMaster
{

    public override void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.shootingRange, gunData.targetMask))
        {
            Debug.Log(gunData.gunName + "has hit" + hit.collider.name);
        }

    }


}
