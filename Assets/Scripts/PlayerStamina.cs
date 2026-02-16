using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
	[Header("Stamina Stats")]
	public float playerStamina = 100f;

	[SerializeField] private float maxStamina = 100f;
	[SerializeField] private float staminaLoss = 10f;
	[SerializeField] private float staminaRegen = 10f;
    [SerializeField] private float staminaDelay = 1f;


    [SerializeField] private int walkSpeed = 4;
	[SerializeField] private int sprintSpeed = 8;

	[SerializeField] private Image stamSlider = null;
	[SerializeField] private CanvasGroup stamCanvasGroup = null;


	public bool hasRegenerated = true;
	public bool playerSprinting = false;
	private PlayerController playerController;


	private void Start()
	{
		playerController = GetComponent<PlayerController>();
	}


	private void Update()
	{
		if (playerSprinting == false)
		{
			if(playerStamina <= maxStamina - 0.1)
			{
				playerStamina += staminaRegen * Time.deltaTime;
// WAIT BEFORE REFILLING
				UpdateStamina(1);
				//increase stamina amount if less than full
            }

			if (playerStamina >= maxStamina - 0.1)
			{
				stamCanvasGroup.alpha = 0;
			}

		}
	}

	public void Sprinting()
	{
		if (hasRegenerated)
		{
			playerSprinting = true;
			playerStamina -= staminaLoss * Time.deltaTime;
			UpdateStamina(1);
		// if the player has enough stamina and they are sprinting, lose over time and execute the visual bar decrease.

        }
	}

	void UpdateStamina(int value) //checks when i ask instead of every frame
	{
        stamSlider.fillAmount = playerStamina / maxStamina;

		if (value == 0)
		{
            stamCanvasGroup.alpha = 0;
		}
		else
		{
            stamCanvasGroup.alpha = 1;
		}
	}
}
