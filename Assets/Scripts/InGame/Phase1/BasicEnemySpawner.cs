using UnityEngine;
using System.Collections;

public class BasicEnemySpawner : MonoBehaviour
{
    public GameObject movableEnemyPrefab;
    private int numberOfEnemies = 2;
    private float spawnInterval = 1.0f;

    private Vector3 startSpawnPosition = new Vector3(-6f, 5f, 12f);
    private float horizontalSpacing = 8.0f;
    private float zSpacing = 0f;

    private bool hasStartedSpawning = false;
    private float directionManager = 0f;
    private bool isLateWave = false;
    private bool isSecondWave = false;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter == 3)
        {
            GlobalVariables.waveCounter += 1;
            horizontalSpacing = 3.5f;
            numberOfEnemies = 3;
            hasStartedSpawning = false;
            isLateWave = false;
            isSecondWave = true;
            OnEnable();
        }
        else if (GlobalVariables.waveCounter == 9)
        {
            isSecondWave= false;
            startSpawnPosition = new Vector3(-5f, 5f, -10f);
            GlobalVariables.waveCounter += 1;
            horizontalSpacing = 2.5f;
            numberOfEnemies = 4;
            hasStartedSpawning = false;
            isLateWave = true;
            OnEnable();
        }
        else if (GlobalVariables.waveCounter == 13)
        {
            isSecondWave = false;
            startSpawnPosition = new Vector3(-5f, 5f, -10f);
            isLateWave = true;
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

            GameObject enemy = Instantiate(movableEnemyPrefab, spawnPosition, Quaternion.identity);
            directionManager+=1;

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

            MovableEnemy movableEnemy = enemy.GetComponent<MovableEnemy>();
            movableEnemy.enemy = enemy.transform;

            if (isLateWave)
            {
                movableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, spawnPosition.z + 2f));
            }
            else
            {
                movableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 4f));
                SetShootDirection(movableEnemy);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SetShootDirection(MovableEnemy movableEnemy)
    {
        switch (directionManager)
        {
            case 1:
                movableEnemy.SetShootDirection(new Vector3(0f, 0f, -1f) * 100f);
                break;
            case 2:
                if (isSecondWave)
                {
                    movableEnemy.SetShootDirection(new Vector3(1f, 0f, -1f) * 100f);
                }
                break;
            case 3:
            case 4:
            case 5:
                movableEnemy.SetShootDirection(new Vector3(0f, 0f, -1f) * 100f);
                break;
            case 6:
                movableEnemy.SetShootDirection(new Vector3(1f, 0f, -1f) * 100f);
                break;
            case 7:
                movableEnemy.SetShootDirection(new Vector3(1f, 0f, 1f) * 100f);
                break;
            case 8:
                movableEnemy.SetShootDirection(new Vector3(1f, 0f, 1f) * 100f);
                break;
            case 9:
                movableEnemy.SetShootDirection(new Vector3(1f, 0f, 1f) * 100f);
                break;
            case 10:
                movableEnemy.SetShootDirection(new Vector3(-1f, 0f, -1f) * 100f);
                break;
        }
    }
}
