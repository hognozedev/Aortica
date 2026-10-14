using UnityEngine;
using static PlayerData;

public class MeleeSpawn : MonoBehaviour, IInteractable
{
    public PlayerController playerController;
    [SerializeField] private GameObject interactPrompt = null;
    public WeaponHandler handler;
    public ItemData itemData;

    public void Interact()
    {
        //var newPrefab = Instantiate(prefab, spawnPos.transform.position, Quaternion.identity);
        //newPrefab.transform.parent = weaponHolder.transform;

        //handler.AddWeaponToHand();

        OnGunTaken?.Invoke(itemData);
        Destroy(gameObject);
    }

    public void OnFocusGained()
    {
        interactPrompt.gameObject.SetActive(true);
    }

    public void OnFocusLost()
    {
        interactPrompt.gameObject.SetActive(false);
    }
}