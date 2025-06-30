using UnityEngine;
using System.Collections;

public class TrapSpawner : MonoBehaviour
{
    private int numberOfEnemies = 1;
    private float spawnInterval = 1.0f;

    private Vector3 startSpawnPosition = new Vector3(-2f, 5f, 9f);
    private float horizontalSpacing = 5.0f;
    private float zSpacing = 0f;

    private bool hasStartedSpawning = false;
    private float directionManager = 0f;
    private int isLateWave = 0;

    private float isSpawning = 0;
    public GameObject trapEnemyPrefab;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter == 5 && isSpawning == 0)
        {
            isLateWave = 1;
            numberOfEnemies = 2;
            startSpawnPosition = new Vector3(-4f, 5f, 7f);
            hasStartedSpawning = false;
            isSpawning += 1;
            OnEnable();
        }
        else if (GlobalVariables.waveCounter == 9 && isSpawning == 1)
        {
            isLateWave = 0;
            numberOfEnemies = 1;
            startSpawnPosition = new Vector3(4f, 5f, 5f);
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
            if (isLateWave==2)
            {
                zSpacing = i * 4.0f;
                spawnPosition.z += zSpacing;
            }

            GameObject enemy = Instantiate(trapEnemyPrefab, spawnPosition, Quaternion.identity);
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

            TrapShooter trapShooter = enemy.GetComponent<TrapShooter>();
            trapShooter.enemy = enemy.transform;

            if (isLateWave==1)
            {
                trapShooter.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 5f));
                trapShooter.SetShootDirection(new Vector3(0, 0f, -100f));
            }
            else
            {
                trapShooter.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 9f));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
