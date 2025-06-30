using UnityEngine;
using System.Collections;

public class Unmovable2Spawner : MonoBehaviour
{
    private int numberOfEnemies = 2;
    private float spawnInterval = 1.0f;

    private Vector3 startSpawnPosition = new Vector3(-4f, 5f, 7f);
    private float horizontalSpacing = 5.0f;
    private float zSpacing = 0f;

    private bool hasStartedSpawning = false;
    private float directionManager = 0f;
    private bool isLateWave = false;

    private float isSpawning = 0;
    public GameObject unmovableEnemyPrefab;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter == 5 && isSpawning == 0)
        {
            startSpawnPosition = new Vector3(-2f, 5f, 9f);
            numberOfEnemies = 1;
            hasStartedSpawning = false;
            OnEnable();
            isSpawning = 1;
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
            if (isLateWave)
            {
                zSpacing = i * 4.0f;
                spawnPosition.z += zSpacing;
            }

            GameObject enemy = Instantiate(unmovableEnemyPrefab, spawnPosition, Quaternion.identity);
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
                restore.Init(enemyCollider, wallColliders, 0.5f);
            }

            Unmovable2Enemy unmovableEnemy = enemy.GetComponent<Unmovable2Enemy>();
            unmovableEnemy.enemy = enemy.transform;

            if (isLateWave)
            {
                unmovableEnemy.SetTargetPosition(new Vector3(-6f, 5f, spawnPosition.z-3f));
                unmovableEnemy.SetShootDirection(new Vector3(100, 0f, 0f));
            }
            else
            {
                unmovableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 5f));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
