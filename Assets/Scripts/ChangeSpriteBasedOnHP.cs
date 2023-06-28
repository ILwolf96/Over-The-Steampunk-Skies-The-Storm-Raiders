using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteBasedOnHP : MonoBehaviour
{
    public Sprite fullHPsprite;
    public Sprite twoThirdsHPsprite;
    public Sprite oneThirdHPsprite;
    public Sprite zeroHPsprite;

    private SpriteRenderer spriteRenderer;
    private float maxHP;
    private float currentHP;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite based on full HP
        spriteRenderer.sprite = fullHPsprite;

        // Set the maximum HP for the object (e.g., 100)
        maxHP = 100f;

        // Set the current HP for the object
        currentHP = maxHP;
    }

    // Call this method whenever the HP changes
    public void UpdateHP(float newHP)
    {
        currentHP = newHP;

        // Calculate the percentage of HP
        float percentage = currentHP / maxHP;

        // Change the sprite based on the HP percentage
        if (percentage > 0.66f)
        {
            spriteRenderer.sprite = fullHPsprite;
        }
        else if (percentage > 0.33f)
        {
            spriteRenderer.sprite = twoThirdsHPsprite;
        }
        else if (percentage > 0f)
        {
            spriteRenderer.sprite = oneThirdHPsprite;
        }
        else
        {
            spriteRenderer.sprite = zeroHPsprite;
        }
    }
}
