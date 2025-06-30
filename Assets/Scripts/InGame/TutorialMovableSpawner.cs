using UnityEngine;

public class TutorialMovableSpawner : MonoBehaviour
{
    public GameObject movableEnemyPrefab;
    private Vector3 spawnPosition = new Vector3(-1f, 5f, 13f);
    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter == 8)
        {
            GlobalVariables.bossCounter = 50;
            Spawn();
            GlobalVariables.waveCounter +=1;
        }
    }
    private void Spawn()
    {
        GameObject enemy = Instantiate(movableEnemyPrefab, spawnPosition, Quaternion.identity);
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

            Movable2Enemy movableEnemy = enemy.GetComponent<Movable2Enemy>();
            movableEnemy.enemy = enemy.transform;
            movableEnemy.SetTargetPosition(new Vector3(spawnPosition.x, 5f, 4f));
        }
    }
}
