using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static PlayerData;

public class PlayerStamina : MonoBehaviour
{
	public float currentStamina = 20;

	[SerializeField] private Image stamSlider = null;
	[SerializeField] private CanvasGroup stamCanvasGroup = null;

	public bool playerSprinting = false;
    float playerSpeed;


    void Update()
	{    

        if (currentStamina <= 0)
        {
            playerSprinting = false;
            playerSpeed = walkSpeed;
        }

        if (playerSprinting == false)
		{
            if (currentStamina <= maxStamina - 1)
            {
                StartCoroutine(RegenWait());
            }

			if (currentStamina >= maxStamina)
			{
				stamCanvasGroup.alpha = 0;
                currentStamina = maxStamina;
			}
        }
    }

    public void TrySprint()
    {
        Debug.Log("try sprint");

        if (currentStamina > 0)
        {
            Sprinting();
            playerSpeed = sprintSpeed;
        }

        if (currentStamina < maxStamina)
        {
            playerSprinting = false;
            playerSpeed = walkSpeed;
            RegenWait();
        }
    }

    IEnumerator RegenWait()
    {
            yield return new WaitForSeconds(regenDelay);
            currentStamina += regenSpeed * Time.deltaTime;
            UpdateStamina(1);
    }

    public void Sprinting()
	{
			playerSprinting = true;
            currentStamina -= staminaLoss * Time.deltaTime;
			UpdateStamina(1);
		// if the player has enough stamina and they are sprinting, lose over time and execute the visual bar decrease.

	}

	void UpdateStamina(int value) //checks when i ask instead of every frame
	{
        stamSlider.fillAmount = currentStamina / maxStamina;

		if (value == 0)
		{
            stamCanvasGroup.alpha = 0;
		}

		if (value == 1)
		{
            stamCanvasGroup.alpha = 1;
		}
	}
}
