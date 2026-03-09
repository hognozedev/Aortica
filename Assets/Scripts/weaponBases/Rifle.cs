using UnityEngine;

public class Rifle : GunMaster
{
    public override void Shoot()
    {
        RaycastHit hit;

        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.shootingRange, gunData.targetLayerMask))
        {
            Debug.Log(gunData.gunName + "hit" + hit.collider.name);
        }

    }

}
