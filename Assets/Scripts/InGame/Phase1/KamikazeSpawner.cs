using UnityEngine;

public class KamikazeSpawner : MonoBehaviour
{
    public GameObject kamikazePrefab;
    public Transform player;
    private Vector3 spawnPosition = new Vector3(0f, 20f, 0f);

    private float spawnInterval = 3f;
    private float nextSpawnTime = 0f;

    private int maxEnemies = 1;
    private int enemiesSpawned = 0;

    private bool isActive = true;
    private bool thirdWave = false;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter == 3)
        {
            spawnInterval = 3f;
            maxEnemies = 2;
            spawnPosition = new Vector3(0f, 10f, 5f);
            enemiesSpawned = 0;
            isActive = true;
        }
        else if (GlobalVariables.waveCounter == 9)
        {
            spawnInterval = 2f;
            maxEnemies = 3;
            enemiesSpawned = 0;
            isActive = true;
            thirdWave = true;
        }
    }

    void Update()
    {
        if (isActive)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnInterval;
            }

            if (enemiesSpawned >= maxEnemies)
            {
                isActive = false;
            }
        }
    }

    void SpawnEnemy()
    {
        if (thirdWave)
        {
            spawnPosition = new Vector3(Random.Range(-11f, -9f), 10f, Random.Range(-9f, 11f));
        }

        GameObject enemy = Instantiate(kamikazePrefab, spawnPosition, Quaternion.identity);

        KamikazeEnemy script = enemy.GetComponent<KamikazeEnemy>();

        script.player = player;
        script.playerScript = player.GetComponent<PlayerCode>();

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
            restore.Init(enemyCollider, wallColliders, 2f);
        }

        enemiesSpawned++;
    }
}
