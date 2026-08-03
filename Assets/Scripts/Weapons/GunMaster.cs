using System.Collections;
using TMPro;
using UnityEngine;

public class GunMaster : MonoBehaviour
{
    [HideInInspector] public Transform cameraTransform;

    //public
    public GunData gunData;
    public TextMeshProUGUI currentAmmoText, totalAmmoText;
    public bool isReloading = false;
    public bool isShooting = false;

    //private
    private int currentAmmo;
    private float NextTimeToFire = 0;
    private float val;
    private bool isJammed;

    // bullet behaviour
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private float bulletMissDistance = 25f;


    private void Start()
    {
        cameraTransform = Camera.main.transform;

        isShooting = false;
        isReloading = false;
        isJammed = false;

        UpdateHUD();
    }

    public void Update()
    {
        if (isShooting && isJammed == false)
        {
            TryShoot();
        }

        if (isReloading && isJammed == false)
        {
            TryReload();
        }

    }

    public void TryReload()
    {
        if (isReloading)
        {
            if (currentAmmo < gunData.magSize && gunData.ammoType.playerHas > 0)
            {
                Debug.Log("rldng...");
                StartCoroutine(Reload());
            }
            else if(gunData.ammoType.playerHas <= 0 )
            {
                Debug.Log("already full/ no more ammo");
            }

        }

    }

    private IEnumerator Reload()
    {     
        yield return new WaitForSeconds(gunData.reloadTime);

        if(gunData.ammoType.playerHas >= gunData.magSize)
        {
            currentAmmo = gunData.magSize;
            gunData.ammoType.playerHas -= gunData.magSize;

        }

        else if(gunData.ammoType.playerHas < gunData.magSize)
        {
            currentAmmo = gunData.ammoType.playerHas;
            gunData.ammoType.playerHas = 0;
        }

        isReloading = false;
        UpdateHUD();

        Debug.Log(gunData.gunName + " rld done.");

    }

    public void TryShoot()
    {
        if (currentAmmo <= 0f)
        {
            Debug.Log("reload " + gunData.gunName + " with 'R'");
            return;
        }

        val = Random.Range(0, 100);
        
        if (val >= gunData.jamChance)
        {
            Debug.Log("jammed");
            isJammed = true;
            StartCoroutine(UnJam());
            return;
        }

        else if (Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + (1 / gunData.fireRate);
        // e.g. if fire rate is 2 then wait between is 0.5 secs

            HandleShoot();
        }

    }

    private IEnumerator UnJam()
    {
        yield return new WaitForSeconds(gunData.jamFix);
        Debug.Log("unjammed");
        isJammed = false;

    }

    private void HandleShoot()
    {
        Debug.Log(currentAmmo + " bullets left");

        currentAmmo--;
        UpdateHUD();
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

    public void UpdateHUD()
    {
        totalAmmoText.text = gunData.ammoType.playerHas.ToString();
        currentAmmoText.text = currentAmmo.ToString();
    }

}