using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormEdge : MonoBehaviour
{
    [SerializeField] private float outEdge = 10f;  // Distance the player can't go past
    [SerializeField] private float innerEdge = 5f; // Distance at which the player starts taking damage
    [SerializeField] private int stormDamage = 10; // Amount of damage the player takes from the storm
    [SerializeField] private float stormTickDamage = 1f; // Time interval between storm damage ticks

    private GameObject player; // Reference to the player object

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has a tag "Player"
    }

    private void Update()
    {
        // Get the distance between the player and the map Border
        float distanceToBorder = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToBorder > outEdge)
        {
            // Player has gone past the outer edge, restrict their movement
            Vector3 directionToBorder = (transform.position - player.transform.position).normalized;
            player.transform.position = transform.position - directionToBorder * outEdge;
        }
        else if (distanceToBorder > innerEdge)
        {
            // Player is inside the outer edge but outside the inner edge, start damaging
            DamagePlayerOverTime();
        }
    }

    private void DamagePlayerOverTime()
    {
        // Damage the player with storm damage at regular intervals
        StartCoroutine(DealStormDamage());
    }

    private System.Collections.IEnumerator DealStormDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(stormTickDamage);

            // Damage the player's health by stormDamage
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // Assuming the player has a health script attached
            playerHealth.TakeDamage(stormDamage);
        }
    }
}
