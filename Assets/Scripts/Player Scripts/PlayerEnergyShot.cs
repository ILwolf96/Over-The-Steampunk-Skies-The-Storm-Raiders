using UnityEngine;

public class PlayerEnergyShot : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 10;

    private Vector3 initialPosition;
    private float timer;
    private bool isMoving;

    private void Start()
    {
        initialPosition = transform.position;
        timer = lifetime;
        isMoving = false;
    }

    private void Update()
    {
        if (isMoving)
        {
            // Move the shot forward
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            // Decrease the lifetime timer
            timer -= Time.deltaTime;

            // Destroy the shot if the timer reaches 0
            if (timer <= 0f)
            {
                DestroyShot();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for collision with the game objects that can be damaged
        // Deal damage and destroy the shot if necessary
        DestroyShot();
    }

    private void DestroyShot()
    {
        // Logic to destroy the shot
        Destroy(gameObject);
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    private void OnDestroy()
    {
        // Reset the position of the shot if it hasn't been destroyed
        if (!isMoving)
        {
            transform.position = initialPosition;
        }
    }
}
