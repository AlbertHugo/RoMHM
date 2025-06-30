using UnityEngine;
using System.Collections;

public class UnmovableSpawner : MonoBehaviour
{
    private int numberOfEnemies = 1;
    private float spawnInterval = 1.0f;

    private Vector3 startSpawnPosition = new Vector3(-2f, 5f, 5f);
    private float horizontalSpacing = 5.0f;
    private float zSpacing = 0f;

    private bool hasStartedSpawning = false;
    private float directionManager = 0f;
    private bool isLateWave = false;

    private float isSpawning = 0;
    public GameObject unmovableEnemyPrefab;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter == 4 && GlobalVariables.spawnCount < 3&&isSpawning==0)
        {
            isSpawning += 1;
            hasStartedSpawning = false;
            startSpawnPosition = new Vector3(-2f, 5f, 7f);
            GlobalVariables.spawnCount += 1;
            OnEnable();
        }
        else if (GlobalVariables.waveCounter == 9 && GlobalVariables.spawnCount < 6&&isSpawning==1)
        {
            isSpawning += 1;
            isLateWave = true;
            hasStartedSpawning = false;
            startSpawnPosition = new Vector3(-6f, 5f, 4f);
            zSpacing = 3.0f;
            GlobalVariables.spawnCount += 1;
            OnEnable();
        }else if (GlobalVariables.waveCounter == 13&&isSpawning==2)
        {
            numberOfEnemies=2;
            horizontalSpacing = 9.0f;
            isSpawning += 1;
            isLateWave = false;
            hasStartedSpawning = false;
            startSpawnPosition = new Vector3(-6f, 5f, 9f);
            zSpacing = 0f;
            GlobalVariables.spawnCount += 1;
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

            UmmovableEnemy unmovableEnemy = enemy.GetComponent<UmmovableEnemy>();
            unmovableEnemy.enemy = enemy.transform;

            if (isLateWave)
            {
                unmovableEnemy.SetTargetPosition(new Vector3(-6f, 5f, spawnPosition.z - 3f));
                unmovableEnemy.SetShootDirection(new Vector3(100, 0f, 0f));
            }
            else if (isSpawning == 3)
            {
                unmovableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 5f));
            }
            else
            {
                unmovableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 8f));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
