using UnityEngine;

public class Kami2Spawner : MonoBehaviour
{
    public GameObject kamikazePrefab;
    public GameObject megaKamikaze;
    public Transform player;

    private Vector3 spawnPosition = new Vector3(0f, 20f, 0f);
    private float spawnInterval = 3f;
    private float nextSpawnTime = 0f;

    private int maxEnemies = 1;
    private int enemiesSpawned = 0;

    private bool isActive = true;
    private bool thirdWave = false;

    private float isSpawning = 0;
    private bool megaSpawned = false;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter == 4 && GlobalVariables.spawnCount < 3 && isSpawning == 0)
        {
            isSpawning += 1;
            enemiesSpawned = 0;
            SpawnEnemy();
            GlobalVariables.spawnCount += 1;
        }
        else if (GlobalVariables.waveCounter == 9 && GlobalVariables.spawnCount < 6 && isSpawning == 1)
        {
            isSpawning += 1;
            isActive = true;
            enemiesSpawned = 0;
            maxEnemies = 2;
            thirdWave = true;
            SpawnEnemy();
            GlobalVariables.spawnCount += 1;
        }
        else if (GlobalVariables.waveCounter == 13 && GlobalVariables.spawnCount < 7 && isSpawning == 2)
        {
            enemiesSpawned = 0;
            maxEnemies = 1;
            isSpawning += 1;
            isActive = true;
            GlobalVariables.spawnCount += 1;            
        }
    }

    void Update()
    {
        if (GlobalVariables.waveCounter >= 13 && isActive)
        {
            MegaSpawnEnemy();
        }
        else if (isActive)
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

        Kamikaze2 script = enemy.GetComponent<Kamikaze2>();
        if (player != null)
        {
            script.player = player;
            script.playerScript = player.GetComponent<Player2Code>();
        }

        enemiesSpawned++;
    }

    void MegaSpawnEnemy()
    {
        if (megaSpawned) return;
        GameObject enemy = Instantiate(megaKamikaze, spawnPosition, Quaternion.identity);

        MegaKamikaze script = enemy.GetComponent<MegaKamikaze>();
        if (player != null)
        {
            script.player = player;
            script.playerScript = player.GetComponent<Player2Code>();
        }

        megaSpawned = true;
        enemiesSpawned++;
    }
}
