using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Player")]
    private GameObject playerShip; //there for detecting the ship.
    [Header("Enemy Ship")]
    [SerializeField] private GameObject enemyShip; //Enemy ship.
    public float maxRange; //Max Detection Range.
    public float maxDistance; //Max Distance from player.
    public float hp; //HP
    public float rotateDuration = 1f;
    private Vector3 direction; //Direction where to aim towards.
    private Quaternion rotation; //attempt to look for stuff
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player"); // finds the player tag
        if (playerShip != null)
        {
            Debug.Log("Player Found!");
            Debug.Log(enemyShip.transform.position-playerShip.transform.position);

        }
    }

    // Update is called once per frame
    private void Update()
    {
        direction = playerShip.transform.position - enemyShip.transform.position; //finds direction to look at
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //calculates angle for the enemy ship to look at.
        enemyShip.transform.DORotate(Quaternion.Euler(new Vector3(0, 0, angle + offset)).eulerAngles, rotateDuration); //Rotates the ship.
    }
}
