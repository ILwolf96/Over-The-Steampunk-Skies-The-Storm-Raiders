using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider hpSlider;
    public Image hpFillImage;

    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        // Set the initial HP for the player
        currentHealth = maxHealth;

        // Set the max value of the slider
        hpSlider.maxValue = maxHealth;

        // Set the initial value of the slider
        hpSlider.value = currentHealth;

        // Set the initial color of the fill image (e.g., green)
        hpFillImage.color = Color.green;
    }

    // Call this method whenever the player's HP changes
    public void UpdateHP(float newHealth)
    {
        // Update the current HP value
        currentHealth = newHealth;

        // Clamp the current HP to ensure it doesn't go below 0 or above the max HP
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        // Update the slider value
        hpSlider.value = currentHealth;

        // Calculate the fill amount based on the current HP
        float fillAmount = currentHealth / maxHealth;

        // Update the color of the fill image based on the fill amount
        if (fillAmount > 0.5f)
        {
            hpFillImage.color = Color.green;
        }
        else if (fillAmount > 0.25f)
        {
            hpFillImage.color = Color.yellow;
        }
        else
        {
            hpFillImage.color = Color.red;
        }
    }

    // Retrieve the current HP value
    public float GetCurrentHP()
    {
        return currentHealth;
    }
}
