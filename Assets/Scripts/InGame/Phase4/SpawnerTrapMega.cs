using UnityEngine;
using System.Collections;

public class SpawnerTrapMega : MonoBehaviour
{
    public GameObject trapEnemyPrefab;
    private int numberOfEnemies = 1;
    private float spawnInterval = 1.0f;

    private Vector3 startSpawnPosition = new Vector3(-2f, 5f, 8f);
    private float horizontalSpacing = 9.0f;
    private float zSpacing = 0f;

    private bool hasStartedSpawning = true;
    private float directionManager = 0f;
    private int isLateWave = 0;

    private float isSpawning = 0;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter >= 22 && isSpawning == 0)
        {
            isSpawning+=1;
            hasStartedSpawning=false;
        }else if (GlobalVariables.waveCounter >= 25 && isSpawning == 1)
        {
            isLateWave=1;
            isSpawning+=1;
            hasStartedSpawning=false;
        }else if (GlobalVariables.waveCounter >= 29 && isSpawning == 2)
        {
            isLateWave=0;
            isSpawning+=1;
            hasStartedSpawning=false;
        }
    }

    void Update()
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
                restore.Init(enemyCollider, wallColliders, 3f);
            }

            MegaTrap trapEnemy = enemy.GetComponent<MegaTrap>();
            trapEnemy.enemy = enemy.transform;

            if (isLateWave == 1)
            {
                trapEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, -2f));
            }
            else if (isLateWave == 2)
            {
                trapEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 2f));
            }
            else
            {
                trapEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 7.5f));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
