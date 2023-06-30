using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Maximum health of the player
    private int currentHealth; // Current health of the player

    private void Start()
    {
        currentHealth = maxHealth; // Set the initial health to the maximum health
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Reduce the current health by the damage amount

        if (currentHealth <= 0)
        {
            Die(); // Player has no health left, call the Die function
        }
    }

    private void Die()
    {
        // Implement what should happen when the player dies
        // For example, you can show a game over screen or restart the level
        Debug.Log("Player has died");
        // Add your code here...
    }
}
