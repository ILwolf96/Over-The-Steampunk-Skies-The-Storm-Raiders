using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FiringPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite pressedSprite;
    public Sprite releasedSprite;

    public GameObject player;
    public GameObject playerEnergyShotPrefab;
    public float shotSpeed = 10f;
    public float cooldownTime = 0.5f;

    private Image image;
    private bool isPressed;
    private float cooldownTimer;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        // Update the cooldown timer
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        image.sprite = pressedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        image.sprite = releasedSprite;
    }

    private void FixedUpdate()
    {
        if (isPressed && cooldownTimer <= 0f)
        {
            // Trigger the firing action
            Fire();
        }
    }

    private void Fire()
    {
        // Get the position and rotation of the player's ship
        Vector3 playerPosition = player.transform.position;
        Quaternion playerRotation = player.transform.rotation;

        // Instantiate the PlayerEnergyShot prefab at the player's position and rotation
        GameObject shot = Instantiate(playerEnergyShotPrefab, playerPosition, playerRotation);

        // Attach Rigidbody2D component to the shot object
        Rigidbody2D shotRigidbody = shot.GetComponent<Rigidbody2D>();
        if (shotRigidbody == null)
        {
            shotRigidbody = shot.AddComponent<Rigidbody2D>();
        }

        // Set the velocity of the shot
        shotRigidbody.velocity = shot.transform.up * shotSpeed;

        // Disable gravity for the shot
        shotRigidbody.gravityScale = 0f;

        // Start the cooldown timer
        cooldownTimer = cooldownTime;
    }
}
