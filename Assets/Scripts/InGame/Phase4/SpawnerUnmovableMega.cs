using UnityEngine;
using System.Collections;

public class SpawnerUnmovableMega : MonoBehaviour
{
    public GameObject unmovableEnemyPrefab;
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
        if (GlobalVariables.waveCounter >= 10 && isSpawning == 0)
        {
            isSpawning+=1;
            hasStartedSpawning=false;
        }else if (GlobalVariables.waveCounter >= 14 && isSpawning == 1)
        {
            isLateWave=1;
            startSpawnPosition = new Vector3(-2f, 5f, 7f);
            isSpawning+=1;
            hasStartedSpawning=false;
        }else if (GlobalVariables.waveCounter >= 17 && isSpawning == 2)
        {
            numberOfEnemies=2;
            isLateWave=0;
            startSpawnPosition = new Vector3(-6f, 5f, 9f);
            isSpawning+=1;
            hasStartedSpawning=false;
        }else if (GlobalVariables.waveCounter >= 22 && isSpawning == 3)
        {
            numberOfEnemies=2;
            isLateWave=0;
            horizontalSpacing=9f;
            startSpawnPosition = new Vector3(-5f, 5f, 9f);
            isSpawning+=1;
            hasStartedSpawning=false;
        }else if (GlobalVariables.waveCounter >= 25 && isSpawning == 4)
        {
            numberOfEnemies=2;
            isLateWave=2;
            horizontalSpacing=7f;
            startSpawnPosition = new Vector3(-3f, 5f, 9f);
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
                restore.Init(enemyCollider, wallColliders, 3f);
            }

            MegaUnmovable unmovableEnemy = enemy.GetComponent<MegaUnmovable>();
            unmovableEnemy.enemy = enemy.transform;

            if (isLateWave == 1)
            {
                unmovableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, -2f));
            }
            else if (isLateWave == 2)
            {
                unmovableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, -4f));
            }
            else
            {
                unmovableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 6f));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
