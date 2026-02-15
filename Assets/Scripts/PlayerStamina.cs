using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
	[Header("Stamina Stats")]
	public float playerStamina = 100f;

	[SerializeField] private float maxStamina = 100f;
	[SerializeField] private float staminaLoss = 0.5f;
	[SerializeField] private float staminaRegen = 0.5f;

	[SerializeField] private int walkSpeed = 4;
	[SerializeField] private int sprintSpeed = 8;

	[SerializeField] private Image stamBarUI = null;
	[SerializeField] private CanvasGroup stamSlider = null;


	public bool hasRegenerated = true;
	public bool playerSprinting = false;

	private PlayerController playerController;


	private void Start()
	{
		playerController = GetComponent<PlayerController>();
	}


	private void Update()
	{
		if (playerSprinting)
		{
			if(playerStamina <= maxStamina - 0.01)
			{
				playerStamina += staminaRegen * Time.deltaTime;
				UpdateStamina(1);
				//increase stamina amount if less than full

				if (playerStamina >= maxStamina)
				{
					stamSlider.alpha = 0;
					hasRegenerated = true;
                // reset slider and back to walk speed

                }

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

			if (playerStamina <= 0)
			{
				hasRegenerated = false;
				stamSlider.alpha = 0;

            //if stamina runs out just ensure the slider is empty

            }
        }
	}


	void UpdateStamina(int value) //checks when i ask instead of every frame
	{
		stamBarUI.fillAmount = playerStamina / maxStamina;

		if (value == 0)
		{
			stamSlider.alpha = 0;
		}
		else
		{
			stamSlider.alpha = 1;
		}
	}
}
