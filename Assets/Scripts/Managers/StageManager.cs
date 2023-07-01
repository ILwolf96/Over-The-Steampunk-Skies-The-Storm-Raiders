using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stage = 1;
    public int initialEnemyCount = 5;
    public int enemyCountStage2 = 10;
    public int enemyCountStage3 = 15;
    public float enemySpawnIntervalStage2 = 5f;
    public float enemySpawnIntervalStage3 = 3f;
    public GameObject enemyShipPrefab; // The enemy ship GameObject to spawn

    private int enemyShipsDestroyed = 0;

    private void Update()
    {
        // Check conditions to transition between stages
        if (stage == 1 && enemyShipsDestroyed >= initialEnemyCount)
        {
            StartStage2();
        }
        else if (stage == 2 && enemyShipsDestroyed >= enemyCountStage2)
        {
            StartStage3();
        }
    }

    private void StartStage2()
    {
        stage = 2;
        // Start spawning enemy ships at the chosen interval for stage 2
        InvokeRepeating("SpawnEnemyShip", 0f, enemySpawnIntervalStage2);
    }

    private void StartStage3()
    {
        stage = 3;
        // Start spawning enemy ships at the chosen interval for stage 3
        InvokeRepeating("SpawnEnemyShip", 0f, enemySpawnIntervalStage3);
    }

    private void SpawnEnemyShip()
    {
        // Instantiate an enemy ship GameObject at a desired position
        Instantiate(enemyShipPrefab, transform.position, Quaternion.identity);
    }

    // Call this method whenever an enemy ship is destroyed
    public void EnemyShipDestroyed()
    {
        enemyShipsDestroyed++;

        // You can perform any additional actions here when an enemy ship is destroyed,
        // such as updating the score or checking for victory conditions.
    }
}
