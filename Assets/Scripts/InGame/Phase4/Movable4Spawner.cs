using UnityEngine;
using System.Collections;

public class Movable4Spawner : MonoBehaviour
{
    public GameObject movableEnemyPrefab;
    private int numberOfEnemies = 2;
    private float spawnInterval = 1.0f;

    private Vector3 startSpawnPosition = new Vector3(-6f, 5f, 9f);
    private float horizontalSpacing = 9.0f;
    private float zSpacing = 0f;

    private bool hasStartedSpawning = false;
    private float directionManager = 0f;
    private int isLateWave = 0;

    private float isSpawning = 0;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter >= 3 && isSpawning == 0)
        {
            isLateWave = 1;
            numberOfEnemies = 1;
            startSpawnPosition = new Vector3(-2f, 5f, 7f);
            hasStartedSpawning = false;
            isSpawning += 1;
            OnEnable();
        }else if (GlobalVariables.waveCounter >= 6 && isSpawning == 1)
        {
            isLateWave = 1;
            numberOfEnemies = 2;
            zSpacing=-3.0f;
            horizontalSpacing=6.0f;
            startSpawnPosition = new Vector3(-5f, 5f, 3f);
            hasStartedSpawning = false;
            isSpawning += 1;
            OnEnable();
        }else if (GlobalVariables.waveCounter >= 10 && isSpawning == 2)
        {
            isLateWave = 0;
            numberOfEnemies = 2;
            zSpacing=-3.0f;
            horizontalSpacing=8.0f;
            startSpawnPosition = new Vector3(-5f, 5f, 9f);
            hasStartedSpawning = false;
            isSpawning += 1;
            OnEnable();
        }else if (GlobalVariables.waveCounter >= 14 && isSpawning == 3)
        {
            isLateWave = 1;
            numberOfEnemies = 2;
            zSpacing=-3.0f;
            horizontalSpacing=7.0f;
            startSpawnPosition = new Vector3(-4f, 5f, 0f);
            hasStartedSpawning = false;
            isSpawning += 1;
            OnEnable();
        }else if (GlobalVariables.waveCounter >= 17 && isSpawning == 4)
        {
            isLateWave = 1;
            numberOfEnemies = 1;
            startSpawnPosition = new Vector3(-2f, 5f, 7f);
            hasStartedSpawning = false;
            isSpawning += 1;
            OnEnable();
        }else if (GlobalVariables.waveCounter >= 22 && isSpawning == 5)
        {
            isLateWave = 1;
            numberOfEnemies = 2;
            zSpacing=-3.0f;
            horizontalSpacing=6.0f;
            startSpawnPosition = new Vector3(-5f, 5f, 3f);
            hasStartedSpawning = false;
            isSpawning += 1;
            OnEnable();
        }else if (GlobalVariables.waveCounter >= 29 && isSpawning == 6)
        {
            isLateWave = 0;
            numberOfEnemies = 2;
            zSpacing=-3.0f;
            horizontalSpacing=9.0f;
            startSpawnPosition = new Vector3(-6f, 5f, 9f);
            hasStartedSpawning = false;
            isSpawning += 1;
            OnEnable();
        }
    }

    void OnEnable()
    {
        if (!hasStartedSpawning)
        {
            StartCoroutine(SpawnEnemies());
            hasStartedSpawning = true;
        }
    }

    IEnumerator SpawnEnemies()
    {
        directionManager = 0;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = startSpawnPosition + new Vector3(horizontalSpacing * i, 0f, 0f);
            if (isLateWave==1)
            {
                zSpacing = i * 4.0f;
                spawnPosition.z += zSpacing;
            }

            GameObject enemy = Instantiate(movableEnemyPrefab, spawnPosition, Quaternion.identity);
            directionManager += 1;

            Collider enemyCollider = enemy.GetComponent<Collider>();
            if (enemyCollider != null)
            {
                GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
                Collider[] wallColliders = new Collider[walls.Length];
                for (int j = 0; j < walls.Length; j++)
                {
                    wallColliders[j] = walls[j].GetComponent<Collider>();
                    if (wallColliders[j] != null)
                    {
                        Physics.IgnoreCollision(enemyCollider, wallColliders[j], true);
                    }
                }
                RestoreWallCollision restore = enemy.AddComponent<RestoreWallCollision>();
                restore.Init(enemyCollider, wallColliders, 3f);
            }

            MegaMovable movableEnemy = enemy.GetComponent<MegaMovable>();
            movableEnemy.enemy = enemy.transform;

            if (isLateWave == 1)
            {
                movableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, spawnPosition.z));
            }
            else if (isLateWave == 2)
            {
                movableEnemy.SetTargetPosition(new Vector3(-2f, 5f, 8f));
            }
            else
            {
                movableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 2f));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
