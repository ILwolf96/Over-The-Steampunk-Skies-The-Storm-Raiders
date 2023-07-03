using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnergyShot : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 10;

    private Vector3 initialPosition;
    private float timer;
    [SerializeField] private bool isSpawner = false; //Check if its the spawner one or not.
    // Yes this is essentially duplicating the work but its easier than just reworking the script right now due to time constraints.
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        timer = lifetime;
        if (transform.parent == null)
            isSpawner = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawner)
        {
            // Move the shot forward
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            // Decrease the lifetime timer
            timer -= Time.deltaTime;
            //Debug.Log("Time Left:"+timer);
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
    private void OnDestroy()
    {
        // Reset the position of the shot if it hasn't been destroyed
        if (!isSpawner)
        {
            transform.position = initialPosition;
        }
    }
}
