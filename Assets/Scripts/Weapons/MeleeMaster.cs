using UnityEngine;

public class MeleeMaster : MonoBehaviour
{
    [Header("Refs")]
    public Transform gunHolder;
    public Transform cameraRef;
    public Animator meleeAnim;

    [Header("Vars")]
    public float meleeDistance, meleeDelay, meleeSpeed;
    public LayerMask meleeLayer;

    //prvs
    private int meleeDamage;
    private bool isAttacking;


    public void CheckMelee()
    {
        /*
        foreach (Transform gun in gunHolder)
        {
            if (gun.gameObject.activeInHierarchy)
            {
                GameObject currentGun = gun.gameObject;
                Debug.Log(currentGun.name);
                Attack();
            }

        //if a gun is out and active, update it as the current one being held.

        }
        */

    }

    public void Attack(bool canGunMelee, int meleeDmg)
    {
        if(!isAttacking && canGunMelee)
        {
            meleeDamage = meleeDmg;

            meleeAnim.SetTrigger("meleeTest");
            isAttacking = true;

            Invoke(nameof(ResetAttack), meleeSpeed);
            Invoke(nameof(AttackCast), meleeDelay);

        }

    }

    public void ResetAttack()
    {
        isAttacking = false;

    }

    public void AttackCast()
    {
        if (Physics.Raycast(cameraRef.transform.position, cameraRef.transform.forward, out RaycastHit hit, meleeDistance, meleeLayer))
        {
            if (hit.collider.gameObject.TryGetComponent<WaifAI>(out WaifAI wEnemy)) wEnemy.TakeDamage(meleeDamage, hit.collider);
            if (hit.collider.gameObject.TryGetComponent<VivisectorAI>(out VivisectorAI vEnemy)) vEnemy.TakeDamage(meleeDamage, hit.collider);


            HitTarget(hit.point);
        }

    }

    void HitTarget(Vector3 pos)
    {
        //play any sound/ visual effects/ decal here

    }

}