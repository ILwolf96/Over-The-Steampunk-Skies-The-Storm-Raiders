using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StormsEdge : MonoBehaviour
{
    public Transform centerPoint; // The center point of the circle
    public float maxRadius = 15f; // Maximum allowed radius
    public float damageRadius = 10f; // Radius for taking damage
    public float damageAmount = 1f; // Amount of damage to be inflicted
    public float speed = 1f; // Speed of rotation

    public PlayerHPBar hpBar; // Reference to the PlayerHPBar script

    private float angle = 0f; // Current angle

    private void Update()
    {
        // Calculate the new position around the circle
        float x = centerPoint.position.x + Mathf.Cos(angle) * maxRadius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * maxRadius;
        Vector3 newPosition = new Vector3(x, y, transform.position.z);

        // Move the object to the new position
        transform.position = newPosition;

        // Update the angle for the next frame
        angle += speed * Time.deltaTime;

        // Keep the angle within 0 to 360 degrees
        if (angle >= 360f)
            angle -= 360f;

        // Calculate the distance from the center point to the player's position
        float distance = Vector2.Distance(centerPoint.position, transform.position);

        // Check if the player has passed the specified radius
        if (distance > damageRadius)
        {
            // Apply damage to the player's HP
            hpBar.UpdateHP(hpBar.GetCurrentHP() - damageAmount);
        }
    }
}
