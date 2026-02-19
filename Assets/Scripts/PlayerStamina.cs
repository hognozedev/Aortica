using System.Collections;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public class PlayerStamina : MonoBehaviour
{
	[Header("Stamina Stats")]

	public float playerStamina = 20f;
	[SerializeField] private float maxStamina = 20f;
	[SerializeField] private float staminaLoss = 10f;
	[SerializeField] private float regenSpeed = 10f;
    //[SerializeField] private float regenDelay = 2f;

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
            if (playerStamina <= maxStamina - 0.1)
            {
				playerStamina += regenSpeed * Time.deltaTime;
				UpdateStamina(1);

				//StartCoroutine(RegenWait());

            }

            if (playerStamina >= maxStamina - 0.1)
			{
				stamCanvasGroup.alpha = 0;
			}

        }

        //IEnumerator RegenWait()
        //{
        //    yield return new WaitForSeconds(regenDelay);
        //}
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
