using UnityEngine;

public class StormsRotation : MonoBehaviour
{
    [SerializeField] private Transform objectToRotateAround;
    [SerializeField] private float distanceFromObject = 1f;
    [SerializeField] private float rotationSpeed = 100f;

    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private float[] rotationDegrees;
    [SerializeField] private float startingRotation = 0f;

    private void Start()
    {
        // Initialize the starting rotation for each sprite
        for (int i = 0; i < rotationDegrees.Length; i++)
        {
            rotationDegrees[i] += startingRotation;
        }
    }

    private void Update()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i] != null)
            {
                float angle = rotationDegrees[i] + Time.time * rotationSpeed;
                Vector3 offset = Quaternion.Euler(0f, 0f, angle) * (Vector3.right * distanceFromObject);
                sprites[i].transform.position = objectToRotateAround.position + offset;

                sprites[i].transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
