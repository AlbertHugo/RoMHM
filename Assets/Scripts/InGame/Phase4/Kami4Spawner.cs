using UnityEngine;

public class Kami4Spawner : MonoBehaviour
{
    public GameObject kamikazePrefab;
    public GameObject megaKamikaze;
    public Transform player;

    private Vector3 spawnPosition = new Vector3(0f, 20f, 0f);
    private float spawnInterval = 3f;
    private float nextSpawnTime = 0f;

    private int maxEnemies = 1;
    private int enemiesSpawned = 0;

    private bool isActive = false;
    private bool thirdWave = false;

    private float isSpawning = 0;
    private bool megaSpawned = false;

    void FixedUpdate()
    {
        if (GlobalVariables.waveCounter >= 3 && isSpawning == 0)
        {
            isSpawning += 1;
            enemiesSpawned = 0;
            isActive=true;
            GlobalVariables.spawnCount += 1;
        }
    }

    void Update()
    {
        if (isActive)
        {
            if (Time.time >= nextSpawnTime)
            {
                MegaSpawnEnemy();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }

        if (enemiesSpawned >= maxEnemies)
        {
            isActive = false;
        }
    }

    void MegaSpawnEnemy()
    {
        GameObject enemy = Instantiate(megaKamikaze, spawnPosition, Quaternion.identity);

        Mega2Kamikaze script = enemy.GetComponent<Mega2Kamikaze>();
        if (player != null)
        {
            script.player = player;
            script.playerScript = player.GetComponent<Player3Code>();
        }
        enemiesSpawned++;
    }
}