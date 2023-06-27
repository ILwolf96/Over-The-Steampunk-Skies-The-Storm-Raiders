using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Slider hpSlider;
    public Image hpFillImage;

    public float maxHP = 100f;
    private float currentHP;

    private void Start()
    {
        // Set the initial HP for the player
        currentHP = maxHP;

        // Set the max value of the slider
        hpSlider.maxValue = maxHP;

        // Set the initial value of the slider
        hpSlider.value = currentHP;

        // Set the initial color of the fill image (e.g., green)
        hpFillImage.color = Color.green;
    }

    // Call this method whenever the player's HP changes
    public void UpdateHP(float newHP)
    {
        // Update the current HP value
        currentHP = newHP;

        // Clamp the current HP to ensure it doesn't go below 0 or above the max HP
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        // Update the slider value
        hpSlider.value = currentHP;

        // Calculate the fill amount based on the current HP
        float fillAmount = currentHP / maxHP;

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
        return currentHP;
    }
}
