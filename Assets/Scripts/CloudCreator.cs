using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCreator : MonoBehaviour
{
    public GameObject spritePrefab;
    public float minRadius = 3f;
    public float maxRadius = 7f;
    public int minSprites = 1;
    public int maxSprites = 5;
    public float minLifetime = 3f;
    public float maxLifetime = 10f;
    public float fadeSpeed = 1f;
    public float minMoveSpeed = 1f;
    public float maxMoveSpeed = 5f;

    private List<GameObject> sprites = new List<GameObject>();

    private void Start()
    {
        CreateSprites();
    }

    private void CreateSprites()
    {
        int numSprites = Random.Range(minSprites, maxSprites + 1);
        for (int i = 0; i < numSprites; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle.normalized * Random.Range(minRadius, maxRadius);
            GameObject sprite = Instantiate(spritePrefab, transform.position + new Vector3(randomPos.x, randomPos.y, 0f), Quaternion.identity);
            sprites.Add(sprite);

            StartCoroutine(FadeSprite(sprite));
            StartCoroutine(DestroySprite(sprite, Random.Range(minLifetime, maxLifetime)));

            float moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            StartCoroutine(MoveSprite(sprite, moveSpeed));
        }
    }

    private IEnumerator FadeSprite(GameObject sprite)
    {
        SpriteRenderer spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, t);
            yield return null;
        }
    }

    private IEnumerator DestroySprite(GameObject sprite, float delay)
    {
        yield return new WaitForSeconds(delay);
        sprites.Remove(sprite);
        Destroy(sprite);
    }

    private IEnumerator MoveSprite(GameObject sprite, float moveSpeed)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        while (sprite != null)
        {
            Vector3 newPosition = sprite.transform.position + new Vector3(randomDirection.x, randomDirection.y, 0f) * moveSpeed * Time.deltaTime;
            sprite.transform.position = newPosition;
            yield return null;
        }
    }
}
