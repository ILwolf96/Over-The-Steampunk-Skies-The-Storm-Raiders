using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FiringPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button")]
    public Sprite pressedSprite;
    public Sprite releasedSprite;

    public GameObject player;
    public GameObject playerEnergyShotPrefab;
    public float shotSpeed = 10f;
    public float cooldownTime = 0.5f;

    private Image image;
    private bool isPressed;
    private float cooldownTimer;
    [Header("Borders")]
    [SerializeField] private GameObject lBorder;
    [SerializeField] private GameObject rBorder;
    [SerializeField] private GameObject tBorder;
    [SerializeField] private GameObject bBorder;

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
        if (((Input.GetTouch(0).position.x < rBorder.transform.position.x) && (Input.GetTouch(0).position.x > lBorder.transform.position.x)) &&
               ((Input.GetTouch(0).position.y < tBorder.transform.position.y) && (Input.GetTouch(0).position.y > bBorder.transform.position.y))) //checks if the press happens between the 4 borders.
        {
            isPressed = true;
            image.sprite = pressedSprite;
        }
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
