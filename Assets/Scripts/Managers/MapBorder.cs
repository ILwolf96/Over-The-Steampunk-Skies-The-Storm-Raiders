using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorder : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public PlayerHealthBar playerHealthBar;

    public float outEdge = 10f;
    public float innerEdge = 5f;
    public float stormDamage = 10f;
    public float stormTickDamage = 1f;

    private bool isInStorm = false;

    private void Update()
    {
        if (playerRigidbody != null && playerHealthBar != null)
        {
            // Calculate the distance from the player to the storm's center
            float distanceToCenter = Vector2.Distance(transform.position, playerRigidbody.position);

            if (distanceToCenter > outEdge)
            {
                // Player is outside the outer edge, restrict their movement
                Vector2 directionToCenter = ((Vector2)transform.position - playerRigidbody.position).normalized;
                Vector2 newPosition = (Vector2)transform.position - directionToCenter * outEdge;
                playerRigidbody.MovePosition(newPosition);
            }
            else if (distanceToCenter > innerEdge)
            {
                // Player is between the inner and outer edges, apply storm damage
                if (!isInStorm)
                {
                    ApplyStormDamage();
                    isInStorm = true;
                }
            }
            else
            {
                // Player is inside the safe zone, stop applying storm damage
                if (isInStorm)
                {
                    StopStormDamage();
                    isInStorm = false;
                }
            }
        }
    }

    private void ApplyStormDamage()
    {
        float currentHealth = playerHealthBar.GetCurrentHP();
        playerHealthBar.UpdateHP(currentHealth - stormDamage);
        Debug.Log("Player takes " + stormDamage + " storm damage. Current HP: " + (currentHealth - stormDamage));
    }

    private void StopStormDamage()
    {
        // Stop applying storm damage (if needed)
    }
}
