using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TheStorm : MonoBehaviour
{
    public Transform centerPoint; // The center point of the circle
    public float maxRadius = 10f; // Maximum allowed radius
    public float damageRadius = 15f; // Radius for taking damage
    public float damageAmount = 10f; // Amount of damage to be inflicted
    public float rotationSpeed = 30f; // Speed of rotation (degrees per second)
    public float spriteRadius = 2f; // Distance of each sprite from the center point

    public PlayerHPBar hpBar; // Reference to the PlayerHPBar script
    public List<Sprite> sprites; // List of sprites to display

    private float angle = 0f; // Current angle

    private void Start()
    {
        // Create child objects and set sprite images
        CreateChildObjects();
    }

    private void CreateChildObjects()
    {
        int spriteCount = sprites.Count;
        float angleIncrement = 360f / spriteCount;

        for (int i = 0; i < spriteCount; i++)
        {
            // Calculate the angle for the current sprite
            float angle = i * angleIncrement;

            // Calculate the position based on the angle, radius, and sprite radius
            float x = centerPoint.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * maxRadius;
            float y = centerPoint.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * maxRadius;
            Vector3 position = new Vector3(x, y, transform.position.z);

            // Create a new child object for each sprite
            GameObject childObject = new GameObject("Sprite" + i);
            childObject.transform.parent = transform;
            childObject.transform.localPosition = position;

            // Add a sprite renderer component and set the sprite image
            SpriteRenderer spriteRenderer = childObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[i];
        }
    }

    private void Update()
    {
        // Calculate the rotation increment based on the rotation speed and time
        float rotationIncrement = rotationSpeed * Time.deltaTime;

        // Update the angle based on the rotation increment
        angle += rotationIncrement;

        // Keep the angle within 0 to 360 degrees
        if (angle >= 360f)
            angle -= 360f;

        // Calculate the new position around the circle
        float x = centerPoint.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * maxRadius;
        float y = centerPoint.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * maxRadius;
        Vector3 newPosition = new Vector3(x, y, transform.position.z);

        // Move the object to the new position
        transform.position = newPosition;

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
