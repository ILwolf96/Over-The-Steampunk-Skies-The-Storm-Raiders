using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stage = 1; // Current stage
    public int initialEnemyCount = 5; // Initial number of enemies in stage 1
    public int enemyCountStage2 = 10; // Number of enemies required to transition to stage 2
    public int enemyCountStage3 = 15; // Number of enemies required to transition to stage 3
    public float enemySpawnTimeStage1 = 2f; // Time interval between enemy spawns in stage 1
    public float enemySpawnTimeStage2 = 5f; // Time interval between enemy spawns in stage 2
    public float enemySpawnTimeStage3 = 3f; // Time interval between enemy spawns in stage 3
    public GameObject enemyShipPrefab; // The enemy ship GameObject to spawn
    public GameObject[] spawners; // Array of spawner GameObjects (size: 8)
    public int maxEnemyCapStage1 = 10; // Maximum number of enemies allowed in stage 1
    public int maxEnemyCapStage2 = 15; // Maximum number of enemies allowed in stage 2
    public int maxEnemyCapStage3 = 20; // Maximum number of enemies allowed in stage 3
    public int shipsToSpawnStage1 = 1; // Number of ships to spawn in stage 1
    public int shipsToSpawnStage2 = 2; // Number of ships to spawn in stage 2
    public int shipsToSpawnStage3 = 3; // Number of ships to spawn in stage 3

    private int enemyShipsDestroyed = 0; // Number of enemy ships destroyed
    private int currentEnemyCount = 0; // Current number of enemies in the scene

    private bool hasReachedLevelTwo = false;
    private bool hasReachedLevelThree = false;

    private void Start()
    {
        // Check if the initial stage is 1 to start spawning immediately
        if (stage == 1)
        {
            SpawnEnemyShipsStage1(); // Spawn initial enemy ships for stage 1
        }
    }

    private void Update()
    {
        // Check conditions to transition between stages
        if (stage == 1 &&  currentEnemyCount==0&&!hasReachedLevelTwo)
        {
            hasReachedLevelTwo=true;
            enemyShipsDestroyed = 0;
            StartStage2(); // Transition to stage 2
            Debug.Log("Spawning Stage 2");
        }
        else if (stage == 2 && currentEnemyCount==0&&!hasReachedLevelThree)
        {
            hasReachedLevelThree=true;
            enemyShipsDestroyed = 0;
            StartStage3(); // Transition to stage 3
            Debug.Log("Spawning Stage 3");
            
            
        }
        if(stage==3&&currentEnemyCount==0)
        {
            GameObject[] allObjects = Object.FindObjectsOfType<GameObject>(); // We have 0 time and I have no idea how to solve things right now, Sorry for this+the full forach.
            foreach(GameObject obj in allObjects)
            {
                if(obj.transform.name.Equals("Enemey Ship(Clone)"))
                    Destroy(obj);
            }
            stage++;
        }
    }

    private void StartStage2()
    {
        stage = 2; // Update current stage to stage 2
        SpawnEnemyShips(maxEnemyCapStage2, shipsToSpawnStage2); // Start spawning enemy ships for stage 2
    }

    private void StartStage3()
    {
        stage = 3; // Update current stage to stage 3
        SpawnEnemyShips(maxEnemyCapStage3, shipsToSpawnStage3); // Start spawning enemy ships for stage 3
    }

    private void SpawnEnemyShipsStage1()
    {
        SpawnEnemyShips(maxEnemyCapStage1, shipsToSpawnStage1);
    }

    private void SpawnEnemyShipsStage2()
    {
        SpawnEnemyShips(maxEnemyCapStage2, shipsToSpawnStage2);
    }

    private void SpawnEnemyShipsStage3()
    {
        SpawnEnemyShips(maxEnemyCapStage3, shipsToSpawnStage3);
    }

    private void SpawnEnemyShips(int maxEnemyCap, int shipsToSpawn)
    {
        // Check if the remaining enemy count exceeds the maximum cap

            for (; shipsToSpawn>0; shipsToSpawn--)
            {
                int randomIndex = Random.Range(0, spawners.Length); // Select a random spawner from the array
                GameObject spawner = spawners[randomIndex];

                // Get the position of the selected spawner
                Vector3 spawnPosition = spawner.transform.position;

                GameObject EnemyShip = Instantiate(enemyShipPrefab, spawnPosition, Quaternion.identity); // Instantiate an enemy ship at the selected spawner position
                EnemyShip.GetComponent<EnemyManager>().stageManager = this;
            currentEnemyCount++; // Increment the current enemy count
            Debug.Log("Ships left to spawn: " + shipsToSpawn);
               // Debug.Log("Enemy ship spawned from spawner: " + spawner.name +" Count: " + currentEnemyCount); // Debug log indicating the spawn location
            }
    }

    public void EnemyShipDestroyed()
    {
        enemyShipsDestroyed++; // Increment the count of destroyed enemy ships
        currentEnemyCount--; // Decrement the current enemy count
        Debug.Log(enemyShipsDestroyed);
        // You can perform any additional actions here when an enemy ship is destroyed,
        // such as updating the score or checking for victory conditions.
    }
}
