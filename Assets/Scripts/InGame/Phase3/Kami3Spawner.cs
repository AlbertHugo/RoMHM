using UnityEngine;

public class Kami3Spawner : MonoBehaviour
{
    public GameObject kamikazePrefab;
    public Transform player;

    private Vector3 spawnPosition = new Vector3(0f, 20f, 0f);
    private float spawnInterval = 3f;
    private float nextSpawnTime = 0f;

    private int maxEnemies = 1;
    private int enemiesSpawned = 0;

    private bool isActive = false;
    private bool thirdWave = false;

    private float isSpawning = 0;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter == 5 && isSpawning == 0)
        {
            isSpawning = 1;
            enemiesSpawned = 0;
            isActive=true;
            GlobalVariables.spawnCount += 1;
        }else if (GlobalVariables.waveCounter == 9 && isSpawning == 1)
        {
            isSpawning = 2;
            enemiesSpawned = 0;
            maxEnemies = 2;
            isActive =true;
            GlobalVariables.spawnCount += 1;
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
        }

        if (enemiesSpawned >= maxEnemies)
        {
            isActive = false;
        }
    }

    void SpawnEnemy()
    {
        if (thirdWave)
        {
            spawnPosition = new Vector3(Random.Range(-13f, 0f), 20f, Random.Range(-18f, 11f));
        }

        GameObject enemy = Instantiate(kamikazePrefab, spawnPosition, Quaternion.identity);

        Kamikaze3 script = enemy.GetComponent<Kamikaze3>();
        if (player != null)
        {
            script.player = player;
            script.playerScript = player.GetComponent<Player3Code>();
        }

        enemiesSpawned++;
    }
}
