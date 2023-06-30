using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public float movementSpeed;           // Movement speed of the enemy ship
    public float rotationSpeed;           // Rotation speed of the enemy ship
    public float attackDamage;            // Damage inflicted by the enemy ship's attack
    public float attackCooldown;          // Cooldown duration between attacks
    public float attackRange;             // Maximum distance for the enemy ship to attack the player
    public float searchInterval;          // Interval between player search attempts
    public float searchRange;             // Maximum distance for the enemy ship to search and find the player
    public float momentumDecayRate;       // Rate at which momentum decreases

    private Transform playerShip;         // Reference to the player ship's transform
    private float attackTimer;            // Timer to track the attack cooldown
    private float searchTimer;            // Timer to track the player search interval
    private float currentMomentum;        // Current momentum of the enemy ship

    private void Start()
    {
        // Start searching for the player immediately
        SearchForPlayer();
    }

    private void Update()
    {
        // Move towards the player ship
        MoveTowardsPlayer();

        // Rotate towards the player ship
        RotateTowardsPlayer();

        // Attack the player ship
        AttackPlayer();
    }

    private void SearchForPlayer()
    {
        // Find the player ship within the search range by tag and store its transform
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerShip = collider.transform;
                break;
            }
        }

        // Reset the search timer to the specified interval
        searchTimer = searchInterval;
    }

    private void MoveTowardsPlayer()
    {
        // Check if the player ship is found, within attack range, and within search range
        if (playerShip != null && Vector3.Distance(transform.position, playerShip.position) <= attackRange &&
            Vector3.Distance(transform.position, playerShip.position) <= searchRange)
        {
            // Calculate the movement direction
            Vector3 direction = playerShip.position - transform.position;
            direction.Normalize();

            // Calculate the movement speed with momentum
            float currentMovementSpeed = movementSpeed * currentMomentum;

            // Move the enemy ship towards the player ship
            transform.Translate(direction * currentMovementSpeed * Time.deltaTime, Space.World);

            // Decrease the current momentum based on the decay rate
            currentMomentum -= momentumDecayRate * Time.deltaTime;
            currentMomentum = Mathf.Clamp01(currentMomentum);
        }
    }

    private void RotateTowardsPlayer()
    {
        // Check if the player ship is found, within attack range, and within search range
        if (playerShip != null && Vector3.Distance(transform.position, playerShip.position) <= attackRange &&
            Vector3.Distance(transform.position, playerShip.position) <= searchRange)
        {
            // Rotate the enemy ship towards the player ship
            Vector3 direction = playerShip.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void AttackPlayer()
    {
        // Check if the player ship is found and within attack range
        if (playerShip != null && Vector3.Distance(transform.position, playerShip.position) <= attackRange)
        {
            // Check if the attack cooldown is over
            if (attackTimer <= 0f)
            {
                // Perform the attack logic here
                // ... Implement your attack logic ...

                // Reset the attack timer to the specified cooldown duration
                attackTimer = attackCooldown;
            }
            else
            {
                // Reduce the attack timer by the elapsed time
                attackTimer -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        // Check if the player ship is not found
        if (playerShip == null)
        {
            // Reduce the search timer by the elapsed time
            searchTimer -= Time.fixedDeltaTime;

            // Check if the search timer is finished
            if (searchTimer <= 0f)
            {
                // Search for the player ship
                SearchForPlayer();
            }
        }
    }

    public void ApplyMomentum(float momentum)
    {
        // Apply momentum to the enemy ship
        currentMomentum += momentum;
        currentMomentum = Mathf.Clamp01(currentMomentum);
    }
}
