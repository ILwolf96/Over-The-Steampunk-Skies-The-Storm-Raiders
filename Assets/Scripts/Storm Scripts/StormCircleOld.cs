using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCircle : MonoBehaviour
{
    public Transform centerPoint; // The center point of the circle
    public float radius = 5f; // Radius of the circle
    public float speed = 1f; // Speed of rotation
    [SerializeField]
    private Sprite[] sprites; // Array of sprites to circle around the center point
    private float angle = 0f; // Current angle

    private void Update()
    {
        // Calculate the new position around the circle
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;
        Vector3 newPosition = new Vector3(x, y, transform.position.z);

        // Move the object to the new position
        transform.position = newPosition;

        // Update the angle for the next frame
        angle += speed * Time.deltaTime;

        // Keep the angle within 0 to 360 degrees
        if (angle >= 360f)
            angle -= 360f;
    }
}
