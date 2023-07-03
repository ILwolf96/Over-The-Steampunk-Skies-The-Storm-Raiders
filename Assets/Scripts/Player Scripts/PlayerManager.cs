using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private EnemyEnergyShot shotManager;
    public float hp = 100;

    public SpriteRenderer shipSpriteRenderer; // Reference to the SpriteRenderer component of the Enemy ship
    public Sprite fullHealthSprite; // Sprite to use when the health is 100%
    public Sprite twoThirdsHealthSprite; // Sprite to use when the health is 66%
    public Sprite oneThirdHealthSprite; // Sprite to use when the health is 33%
    public Sprite zeroHealthSprite; // Sprite to use when the health is 0%

    private void Start()
    {
        shipSpriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the Enemy ship
        UpdateShipSprite();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Shot"))
        {
            shotManager = collision.gameObject.GetComponent<EnemyEnergyShot>();
            hp -= shotManager.damage;
            Debug.Log("HP Left: " + hp);
            Destroy(collision.gameObject);

            UpdateShipSprite();
        }
    }

    private void UpdateShipSprite()
    {
        float healthPercentage = hp / 100f;

        if (healthPercentage >= 1f) // If health is 100% or higher
        {
            shipSpriteRenderer.sprite = fullHealthSprite;
        }
        else if (healthPercentage >= 0.66f) // If health is between 66% and 100%
        {
            shipSpriteRenderer.sprite = twoThirdsHealthSprite;
        }
        else if (healthPercentage >= 0.33f) // If health is between 33% and 66%
        {
            shipSpriteRenderer.sprite = oneThirdHealthSprite;
        }
        else if (healthPercentage > 0f) // If health is between 0% and 33%
        {
            shipSpriteRenderer.sprite = zeroHealthSprite;
        }
        else // If health is 0% or lower
        {
            // Set a sprite for when health is 0% or lower, or do something else if desired
        }
    }
}
