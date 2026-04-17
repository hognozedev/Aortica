using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public class PlayerStats : MonoBehaviour
{
	[Header("Player Health")]
	public float currentHealth = 100;
	public float maxHealth = 100;


    [Header("Stamina")]
	public float currentStamina = 20;
	[SerializeField] private float maxStamina = 20;
	[SerializeField] private float staminaLoss = 10;
	[SerializeField] private float regenSpeed = 10;
    [SerializeField] private float regenDelay = 2;

	[SerializeField] private Image stamSlider = null;
	[SerializeField] private CanvasGroup stamCanvasGroup = null;

	public bool hasRegenerated = true;
	public bool playerSprinting = false;


	private void Update()
	{
		if (playerSprinting == false)
		{

            if (currentStamina <= maxStamina -.2)
            {
                StartCoroutine(RegenWait());
            }

			if (currentStamina >= maxStamina -.2)
			{
				stamCanvasGroup.alpha = 0;
                currentStamina = maxStamina;
			}

        }
    }

    IEnumerator RegenWait()
    {
        yield return new WaitForSeconds(regenDelay);
        currentStamina += regenSpeed * Time.time;
        UpdateStamina(1);
    }

    public void Sprinting()
	{
		if (hasRegenerated)
		{
			playerSprinting = true;
            currentStamina -= staminaLoss * Time.deltaTime;
			UpdateStamina(1);
		// if the player has enough stamina and they are sprinting, lose over time and execute the visual bar decrease.

        }
	}

	void UpdateStamina(int value) //checks when i ask instead of every frame
	{
        stamSlider.fillAmount = currentStamina / maxStamina;

		if (value == 0)
		{
            stamCanvasGroup.alpha = 0;
		}
		else if (value >= 1)
		{
            stamCanvasGroup.alpha = 1;
		}
	}
}
