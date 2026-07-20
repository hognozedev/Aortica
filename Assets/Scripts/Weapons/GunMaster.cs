using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerData;
using static ItemData;

public class GunMaster : MonoBehaviour
{
    private PlayerController playerController;
    [HideInInspector] public Transform cameraTransform;

    public GunData gunData;

    private int currentAmmo;
    private float NextTimeToFire = 0;
    private ItemData ammoType;

    public bool isReloading = false;
    public bool isShooting = false;

    // bullet behaviour
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private float bulletMissDistance = 25f;


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        cameraTransform = Camera.main.transform;

        currentAmmo = gunData.currentAmmo;
        isShooting = false;
        isReloading = false;

    }

    public void Update()
    {
        if (isShooting)
        {
            TryShoot();
        }

        if (isReloading)
        {
            TryReload();
        }

    }

    public void TryReload()
    {
        if (isReloading)
        {
            if (currentAmmo < gunData.magSize)
            {
                Debug.Log("");
                Debug.Log("reloading...");
                StartCoroutine(Reload());
            }
            else
            {
                Debug.Log("");
                Debug.Log("Already full");
            }

        }

    }

    private IEnumerator Reload()
    {
        
        yield return new WaitForSeconds(gunData.reloadTime);

        /*
        if(ItemData.quantity < gunData.magSize)
        {
            currentAmmo = ammoType;
        }

        else
        {
            
        currentAmmo = gunData.magSize;
        }
        */

        isReloading = false;
        Debug.Log("");
        Debug.Log(gunData.gunName + " reload complete.");
    }

    public void TryShoot()
    {
        if (currentAmmo <= 0f)
        {
            Debug.Log("");
            Debug.Log("Reload " + gunData.gunName + " with 'R'");
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
    //FIX HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
        //currentAmmo--;
        //PlayerData.iRifle--;
        Debug.Log("");
        Debug.Log(currentAmmo + " bullets left");
        Shoot();
    }
    // things that still need to happen when firing, even if no hit target like recoil/ screen shake, etc.

    private void Shoot()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, gunData.shootingRange))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;

            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if(hit.collider.TryGetComponent<VivisectorAI>(out VivisectorAI vEnemy))
                {
                    vEnemy.TakeDamage(gunData.bulletDamage);

                }

            }
    
        }

        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            bulletController.hit = true;

        }

    }

    public void ChangeGun()
    {
        
    }

}