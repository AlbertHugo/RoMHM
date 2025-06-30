using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

public class Boss3Code : MonoBehaviour
{
    public GameObject kleber;
    public GameObject kleberBar;
    private bool spawnedKleber=false;
    public GameObject projectilePrefab;
    public GameObject basicShot;
    public GameObject unmovableShot;
    public GameObject garance;
    public GameObject garanceBar;
    private bool spawnedGarance = false;
    public Transform enemy;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;

    public Transform player;
    private BossAttacks bossAttacks;

    public float bossLife = 400.0f;

    [HideInInspector] public Vector3 shootDirection = new Vector3(0f, 0f, -100f);

    private float attackSpeed = 7.0f;
    private float nextAttackTime = 0.0f;
    public bool focus = false;

    public AudioClip enemyDeathSound;
    public AudioClip hitSound;
    bool hasDied = false;

    private bool isDying = false;
    private float deathScoreInterval = 0.2f;
    private float deathTimer = 0f;
    private float totalDeathTime = 1.5f;

    public AudioClip scoreSound;
    private Renderer enemyRenderer;
    public Color vengeance;


    private Vector3 centerPoint = new Vector3(-2.39f, 5f, -0.15f);
    private float radius = 3.5f;                  
    private float angularSpeed = 1f;            // Velocidade
    private float angle = 0f;
    private float fakeLife = 10f;
    void Start()
    {
        deathExplosion.gameObject.SetActive(false);
        damageTaken.gameObject.SetActive(false);
        GlobalVariables.waveCounter = 0;
        enemy.LookAt(shootDirection);
        bossAttacks = GetComponent<BossAttacks>();
        nextAttackTime = Time.time + attackSpeed;
        enemyRenderer = GetComponent<Renderer>();
        enemyRenderer.material.SetColor("_Color", Color.white);
        vengeance = new Color(1f, 0f, 0f, 1f);
        kleber.SetActive(false);
        garance.SetActive(false);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("focusShot"))
        {
            fakeLife -= 2;
            bossLife -= 2;
            RepeatableCode.PlaySound(hitSound, enemy.position);
            TakeDamage();
        }
        else if (collision.gameObject.CompareTag("unfocusShot"))
        {
            bossLife -= 1;
            fakeLife -= 1;
            RepeatableCode.PlaySound(hitSound, enemy.position);
            TakeDamage();
        }
        else if (collision.gameObject.CompareTag("chargedShot"))
        {
            bossLife -= 60;
            fakeLife -= 60;
            RepeatableCode.PlaySound(hitSound, enemy.position);
            TakeDamage();
        }
        if (bossLife <= 0)
        {
            RepeatableCode.PlaySound(enemyDeathSound, enemy.position);
            Die();
        }

    }
    void FixedUpdate()
    {
        Debug.Log(fakeLife);
        if (fakeLife <= -10)
        {
            GlobalVariables.waveCounter += 5;
            fakeLife = 10;
        }
        else if (fakeLife <= 0)
        {
            GlobalVariables.waveCounter += 1;
            fakeLife = 10;
        }
        if (bossLife <= 300&&spawnedKleber==false)
        {
            kleber.SetActive(true);
            kleberBar.SetActive(true);
            spawnedKleber = true;
            GlobalVariables.dialogueCounter = 1;
            Time.timeScale = 0f;
        }
        else if(bossLife <= 200 && spawnedGarance == false)
        {
            garance.SetActive(true);
            garanceBar.SetActive(true);
            spawnedGarance = true;
            GlobalVariables.dialogueCounter = 2;
            Time.timeScale = 0f;
        }
        if (isDying)
        {
            deathTimer += Time.deltaTime;

            if (deathTimer >= deathScoreInterval)
            {
                
                RepeatableCode.PlaySound(scoreSound, enemy.position);
                GlobalVariables.score += 1000;
                deathTimer = 0f;
            }

            if (deathTimer >= totalDeathTime)
            {
                isDying = false;
            }
        }
        if (GlobalVariables.bossCounter != 15)
        {
            angle += angularSpeed * Time.deltaTime;

            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            transform.position = new Vector3(centerPoint.x + x, transform.position.y, centerPoint.z + z);
        }
        HandleAttacks();
    }
    void HandleAttacks()
    {
        if (GlobalVariables.bossCounter == 0 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootFastTrap(basicShot, enemy, 6, 18, 0.5f);
            nextAttackTime = Time.time + attackSpeed;
            GlobalVariables.bossCounter += 1;
        }
        else if (GlobalVariables.bossCounter == 1 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootFocusTrap(projectilePrefab, enemy, 6, 1f, 6, 0.8f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (GlobalVariables.bossCounter == 2 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootRepelBurst(unmovableShot, enemy, 12, 6f, 90f, 3f, 50f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed+2.0f;
        }
        else if (GlobalVariables.bossCounter == 3 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootCharged(projectilePrefab, enemy, 15, 0.1f, 6f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (GlobalVariables.bossCounter == 4 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootFastTrap(basicShot, enemy, 6, 18, 0.5f);
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (GlobalVariables.bossCounter == 5 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootFocusTrap(projectilePrefab, enemy, 6, 1f, 6, 0.8f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (GlobalVariables.bossCounter == 6 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootRepelBurst(unmovableShot, enemy, 12, 6f, 180f, 10f, 50f);
            GlobalVariables.bossCounter = 0;
            nextAttackTime = Time.time + attackSpeed;
        }
    }
    public void Die()
    {
        if (hasDied) return;
        hasDied = true;
        isDying = true;
        deathTimer = 0f;

        GlobalVariables.bossCounter = 15;

        deathExplosion.gameObject.SetActive(true);
        if (deathExplosion != null)
        {
            VisualEffect vfx = Instantiate(deathExplosion, transform.position, Quaternion.identity);
            vfx.Play();
            Destroy(vfx.gameObject, 2f);
        }

        Destroy(gameObject, totalDeathTime + 1f);
    }

    public void TakeDamage()
    {
        damageTaken.gameObject.SetActive(true);
        VisualEffect vfxDamage = Instantiate(damageTaken, transform.position, Quaternion.identity);
        vfxDamage.Play();
        Destroy(vfxDamage.gameObject, 0.6f);
        damageTaken.gameObject.SetActive(false);
    }
}
