using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorder : MonoBehaviour
{
    public PlayerHealthBar playerHealthBar; // Reference to the PlayerHealthBar component

    public float outEdge = 10f; // Distance the player can't go past
    public float innerEdge = 5f; // Distance at which the player starts taking damage

    public float stormDamage = 10f; // Amount of damage the player takes from the storm
    public float stormTickDamage = 1f; // Time interval between storm damage ticks

    private Transform playerTransform;
    private bool isInsideStorm = false; // Flag indicating whether the player is inside the storm or not

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerHealthBar == null)
        {
            Debug.LogError("PlayerHealthBar component not found. Make sure to assign the reference in the inspector.");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("Player object not found. Make sure the player has the 'Player' tag assigned.");
        }
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        Vector2 playerPosition2D = playerTransform.position;

        float distanceToStorm = Vector2.Distance(playerPosition2D, transform.position);

        if (distanceToStorm > outEdge)
        {
            Vector2 direction = playerPosition2D - (Vector2)transform.position;
            Vector2 playerPosition = (Vector2)transform.position + (direction.normalized * outEdge);
            playerTransform.position = playerPosition;
        }

        if (distanceToStorm <= outEdge)
        {
            if (distanceToStorm > innerEdge)
            {
                if (!isInsideStorm)
                {
                    isInsideStorm = true;
                    StartStormDamage();
                }
            }
            else
            {
                if (isInsideStorm)
                {
                    isInsideStorm = false;
                    StopStormDamage();
                }
            }
        }
        else
        {
            if (isInsideStorm)
            {
                isInsideStorm = false;
                StopStormDamage();
            }
        }
    }

    private void StartStormDamage()
    {
        InvokeRepeating("ApplyStormDamage", stormTickDamage, stormTickDamage);
    }

    private void StopStormDamage()
    {
        CancelInvoke("ApplyStormDamage");
    }

    private void ApplyStormDamage()
    {
        if (playerTransform != null && playerHealthBar != null)
        {
            float currentHealth = playerHealthBar.GetCurrentHP();
            playerHealthBar.UpdateHP(currentHealth - stormDamage);
            Debug.Log("Player takes " + stormDamage + " storm damage. Current HP: " + (currentHealth - stormDamage));
        }
    }

}