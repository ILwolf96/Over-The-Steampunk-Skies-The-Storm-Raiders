using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Player")]
    private GameObject playerShip; // There for detecting the ship.

    [Header("Enemy Ship")]
    [SerializeField] private GameObject enemyShip; // Enemy ship.
    public Sprite fullHealthSprite; // Sprite for full health (100%)
    public Sprite twoThirdsHealthSprite; // Sprite for two-thirds health (66%)
    public Sprite oneThirdHealthSprite; // Sprite for one-third health (33%)
    public Sprite noHealthSprite; // Sprite for no health (0%)
    public float maxRange = 30; // Max Detection Range.
    public float maxDistance = 20; // Max Distance from player.
    public float hp; // HP
    public float rotateDuration = 1f;
    public float playerDamageAmount = 1f; // Amount of damage taken from the player
    public float scaleReductionAmount = 0.1f; // Amount to reduce the scale by
    public float scaleReductionDuration = 1f; // Duration of the scale reduction
    public float pressureTickDamageAmount = 1f; // Amount of ticking damage taken when health is low
    public float pressureTickInterval = 1f; // Time interval between ticking damage
    public float movementSpeed = 3f; // Movement speed of the enemy ship
    private Vector3 direction; // Direction where to aim towards.
    private Quaternion rotation; // Attempt to look for stuff.
    private float offset = 270;
    private bool isFar = true;
    private float distance; // Distance between ships.
    private bool isScalingDown = false; // Flag to track if scaling down is in progress
    private bool isTakingPressureDamage = false; // Flag to track if pressure ticking damage is in progress

    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player"); // Finds the player tag
        if (playerShip != null)
        {
            //Debug.Log("Player Found!");
            //Debug.Log(enemyShip.transform.position - playerShip.transform.position);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (hp <= 0 && !isScalingDown)
        {
            StartScaleReduction();
            return;
        }

        direction = playerShip.transform.position - enemyShip.transform.position; // Finds direction to look at
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculates angle for the enemy ship to look at
        enemyShip.transform.DORotate(Quaternion.Euler(new Vector3(0, 0, angle + offset)).eulerAngles, rotateDuration); // Rotates the ship

        if (isFar)
        {
            enemyShip.transform.Translate(Vector3.up * Time.deltaTime * movementSpeed); // Moves the ship forward with movementSpeed
        }

        // Debug.Log(Vector2.Distance(enemyShip.transform.position, playerShip.transform.position));
        distance = Vector2.Distance(enemyShip.transform.position, playerShip.transform.position); // Checks the distance between the ship

        if (distance < maxDistance || distance > maxRange)
        {
            isFar = false;
            // Debug.Log("STOP");
        }

        if (distance > maxDistance && distance < maxRange)
            isFar = true;

        // Update enemy ship sprite based on health
        UpdateEnemySprite();

        if (hp <= 33 && !isTakingPressureDamage)
        {
            StartPressureTickDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player Shot")
        {
            Debug.Log("Enemy Shot been hit!");
            Destroy(collision.gameObject);
            TakePlayerDamage(playerDamageAmount); // Apply damage to the enemy ship from the player
            // Update enemy ship sprite based on health
            UpdateEnemySprite();
        }
    }

    private void UpdateEnemySprite()
    {
        // Determine the sprite based on health percentage
        SpriteRenderer spriteRenderer = enemyShip.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            if (hp == 100)
            {
                spriteRenderer.sprite = fullHealthSprite;
            }
            else if (hp > 66)
            {
                spriteRenderer.sprite = twoThirdsHealthSprite;
            }
            else if (hp > 33)
            {
                spriteRenderer.sprite = oneThirdHealthSprite;
            }
            else
            {
                spriteRenderer.sprite = noHealthSprite;
            }
        }
    }

    private void TakePlayerDamage(float amount)
    {
        hp -= amount;
    }

    private void StartScaleReduction()
    {
        isScalingDown = true;
        Vector3 targetScale = enemyShip.transform.localScale * scaleReductionAmount;
        enemyShip.transform.DOScale(targetScale, scaleReductionDuration).OnComplete(CompleteScaleReduction);
    }

    private void CompleteScaleReduction()
    {
        isScalingDown = false;
        Destroy(enemyShip);
    }

    private void StartPressureTickDamage()
    {
        isTakingPressureDamage = true;
        StartCoroutine(ApplyPressureTickDamage());
    }

    private IEnumerator ApplyPressureTickDamage()
    {
        while (hp > 0 && hp <= 33)
        {
            TakePlayerDamage(pressureTickDamageAmount);
            yield return new WaitForSeconds(pressureTickInterval);
        }

        isTakingPressureDamage = false;
    }
}
