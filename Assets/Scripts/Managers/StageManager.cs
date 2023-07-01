using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stage = 1; // Current stage
    public int initialEnemyCount = 5; // Initial number of enemies in stage 1
    public int enemyCountStage2 = 10; // Number of enemies in stage 2
    public int enemyCountStage3 = 15; // Number of enemies in stage 3
    public float enemySpawnIntervalStage2 = 5f; // Time interval between enemy spawns in stage 2
    public float enemySpawnIntervalStage3 = 3f; // Time interval between enemy spawns in stage 3
    public GameObject enemyShipPrefab; // The enemy ship GameObject to spawn
    public GameObject[] spawners; // Array of spawner GameObjects (size: 8)

    private int enemyShipsDestroyed = 0; // Number of enemy ships destroyed
    private int maxEnemyCount = 0; // Maximum number of enemies allowed for the current stage

    private void Start()
    {
        // Check if the initial stage is 1 to start spawning immediately
        if (stage == 1)
        {
            maxEnemyCount = initialEnemyCount; // Set the maximum enemy count for stage 1
            InvokeRepeating("SpawnEnemyShip", 0f, enemySpawnIntervalStage2); // Start spawning enemy ships
        }
    }

    private void Update()
    {
        // Check conditions to transition between stages
        if (stage == 1 && enemyShipsDestroyed >= initialEnemyCount)
        {
            StartStage2(); // Transition to stage 2
        }
        else if (stage == 2 && enemyShipsDestroyed >= enemyCountStage2)
        {
            StartStage3(); // Transition to stage 3
        }
    }

    private void StartStage2()
    {
        stage = 2; // Update current stage to stage 2
        maxEnemyCount = enemyCountStage2; // Set the maximum enemy count for stage 2
        CancelInvoke("SpawnEnemyShip"); // Cancel the previous spawn invocations
        InvokeRepeating("SpawnEnemyShip", 0f, enemySpawnIntervalStage2); // Start spawning enemy ships for stage 2
    }

    private void StartStage3()
    {
        stage = 3; // Update current stage to stage 3
        maxEnemyCount = enemyCountStage3; // Set the maximum enemy count for stage 3
        CancelInvoke("SpawnEnemyShip"); // Cancel the previous spawn invocations
        InvokeRepeating("SpawnEnemyShip", 0f, enemySpawnIntervalStage3); // Start spawning enemy ships for stage 3
    }

    private void SpawnEnemyShip()
    {
        // Check if the maximum enemy count for the current stage has been reached
        if (enemyShipsDestroyed >= maxEnemyCount)
        {
            CancelInvoke("SpawnEnemyShip"); // Maximum count reached, stop spawning ships
            return;
        }

        int randomIndex = Random.Range(0, spawners.Length); // Select a random spawner from the array
        GameObject spawner = spawners[randomIndex];

        Instantiate(enemyShipPrefab, spawner.transform.position, Quaternion.identity); // Instantiate an enemy ship at the selected spawner position
    }

    public void EnemyShipDestroyed()
    {
        enemyShipsDestroyed++; // Increment the count of destroyed enemy ships

        // You can perform any additional actions here when an enemy ship is destroyed,
        // such as updating the score or checking for victory conditions.
    }
}
