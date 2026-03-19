using System;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunMaster : MonoBehaviour
{
    public PlayerController playerController;
    [HideInInspector] public Transform cameraTransform;

    public GunData gunData;

    private float currentAmmo = 0;
    private float NextTimeToFire = 0;

    public bool isReloading = false;
    public bool isShooting = false;

    // bullet behaviour
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private float bulletMissDistance = 25f;


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        cameraTransform = Camera.main.transform;

        currentAmmo = gunData.magSize;
        isShooting = false;
        isReloading = false;

    }

    public void Update()
    {
        if (isShooting)
        {
            Debug.Log("attack pressed");
            TryShoot();
        }

        if (isReloading)
        {
            Debug.Log("reload pressed");
            TryReload();
        }
    }


    public void TryReload()
    {
        if (isReloading)
        {
            if (currentAmmo < gunData.magSize)
            {
                StartCoroutine(Reload());
            }
            else
            {
                Debug.Log("Already full");
            }

        }
    }


    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(gunData.reloadTime);
        currentAmmo = gunData.magSize;
        isReloading = false;

        Debug.Log(gunData.gunName + "done!");

    }

    public void TryShoot()
    {
        if (isReloading)
        {
            Debug.Log(gunData.gunName + "currently reloading.");
            return;
        }

        if (currentAmmo <= 0f)
        {
            Debug.Log(gunData.gunName + "empty, please reload");
            return;
        }

        if (Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + (1 / gunData.fireRate);
            // e.g. if fire rate is 2 then wait between is 0.5 secs
            HandleShoot();
        }

    }

    private void HandleShoot()
    {
        currentAmmo--;
        Debug.Log(gunData.gunName + "has shot. Bullets left = " + currentAmmo);
        Shoot();

    }
    // things that still need to happen when firing, even if no hit target like recoil/ screen shake, etc.

    private void Shoot()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.shootingRange))
        {
            Debug.Log(gunData.gunName + "hit" + hit.collider.name);
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            bulletController.hit = true;
        }

    }

}