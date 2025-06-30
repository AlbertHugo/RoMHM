using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

public class Boss2Code : MonoBehaviour
{
    public GameObject sniperMinion;
    public GameObject projectilePrefab;
    public GameObject secondBullet;
    public Transform enemy;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;

    public Transform player;
    private BossAttacks bossAttacks;

    public float bossLife = 200.0f;

    [HideInInspector] public Vector3 shootDirection = new Vector3(0f, 0f, -100f);

    private float attackSpeed = 3.0f;
    private float nextAttackTime = 0.0f;

    public bool focus = false;

    public float speed = 1.0f;
    public float baseSpeed = 1.0f;

    private Vector3 moveDirection = Vector3.forward;
    private float minZ = -3.5f;
    private float maxZ = 7f;

    public AudioClip enemyDeathSound;
    public AudioClip hitSound;
    bool hasDied = false;
    private bool canSpawn = true;

    private bool isDying = false;
    private float deathScoreInterval = 0.2f;
    private float deathTimer = 0f;
    private float totalDeathTime = 1.5f;

    public AudioClip scoreSound;
    private Renderer enemyRenderer;
    public Color vengeance;

    private Coroutine warningCoroutine;

    public AudioClip warning;
    public Image warningObject;
    public Image warningObject2;
    private Sprite[] warningImage;
    private float warningFlashSpeed = 0.2f;

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
        warningImage = new Sprite[]{
        Resources.Load<Sprite>("HUD/Warning1"),//0
        Resources.Load<Sprite>("HUD/Warning2")//1
        };
        if (GlobalVariables.phaseCounter == 8)
        {
            attackSpeed = 7f;
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("focusShot"))
        {
            bossLife -= 2;
            RepeatableCode.PlaySound(hitSound, enemy.position);
            TakeDamage();
        }
        else if (collision.gameObject.CompareTag("unfocusShot"))
        {
            bossLife -= 1;
            RepeatableCode.PlaySound(hitSound, enemy.position);
            TakeDamage();
        }
        else if (collision.gameObject.CompareTag("chargedShot"))
        {

            bossLife -= 60;
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
        if (GlobalVariables.bossCounter==15&&GlobalVariables.phaseCounter==8)
        {
            bossLife = 0;
        }
        if (bossLife <= 50)
        {
            enemyRenderer.material.color = vengeance;
            speed = 6.0f;
            baseSpeed = 5.0f;
            minZ = -6.5f;
        }
        if (GlobalVariables.enemiesAlive == 4)
        {
            canSpawn = false;
        }
        else if (GlobalVariables.enemiesAlive == 0)
        {
            canSpawn = true;
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
        if (isDying == false)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        if (transform.position.z >= maxZ)
        {
            moveDirection = Vector3.back;
        }
        else if (transform.position.z <= minZ)
        {
            moveDirection = Vector3.forward;
        }
        HandleAttacks();
        RepeatableCode.HandleFocusMode(ref speed, ref focus, baseSpeed);
        if (bossLife <= 0)
        {
            RepeatableCode.PlaySound(enemyDeathSound, enemy.position);
            Die();
        }
    }
    void HandleAttacks()
    {
        if (GlobalVariables.bossCounter == 0 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootRadial(projectilePrefab, enemy, 24, 6f);
            nextAttackTime = Time.time + attackSpeed;
            GlobalVariables.bossCounter += 1;
        }
        else if (GlobalVariables.bossCounter == 1 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootSporeBurst(secondBullet, enemy, 24, 3f, 50f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed - 2f;
        }
        else if (GlobalVariables.bossCounter == 2 && Time.time >= nextAttackTime)
        {
            bossAttacks.SporeBlast(projectilePrefab, enemy, 34, 5f, 0.15f, 20f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (GlobalVariables.bossCounter == 3 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootCrossBarAttack(projectilePrefab, enemy, 5f, 0.2f, 8f, 6, 0.5f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (GlobalVariables.bossCounter == 4 && Time.time >= nextAttackTime)
        {
            if (!canSpawn)
            {
                GlobalVariables.bossCounter += 1;
                nextAttackTime = Time.time + attackSpeed;
            }
            else
            {
                bossAttacks.SniperSpawn(2, new Vector3(-6f, 5f, 12f), 8f, sniperMinion, 0.1f);
                GlobalVariables.enemiesAlive += 2;
                GlobalVariables.bossCounter += 1;
                nextAttackTime = Time.time + attackSpeed;
            }
        }
        else if (GlobalVariables.bossCounter == 5 && Time.time >= nextAttackTime)
        {
            if (!canSpawn)
            {
                GlobalVariables.bossCounter += 1;
                nextAttackTime = Time.time + attackSpeed;
            }
            else
            {
                bossAttacks.SniperSpawn(2, new Vector3(-4f, 5f, 8f), 4f, sniperMinion, 0.1f);
                GlobalVariables.enemiesAlive += 2;
                GlobalVariables.bossCounter += 1;
                nextAttackTime = Time.time + attackSpeed + 3f;
            }
        }
        else if (GlobalVariables.bossCounter == 6 && Time.time >= nextAttackTime&&GlobalVariables.phaseCounter==6)
        {
            RepeatableCode.PlaySound(warning, enemy.position);
            StartWarningFlash();
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (GlobalVariables.bossCounter == 7 && Time.time >= nextAttackTime && GlobalVariables.phaseCounter == 6)
        {
            StopWarningFlash();
            bossAttacks.StartSideColumnAttack(projectilePrefab, zStart: -10f, zEnd: 10f, countPerColumn: 12, leftX: -6f, rightX: 3f, linesPerColumn: 4, columnWidth: 3f, delayBetweenLines: 0.2f, this);
            GlobalVariables.bossCounter = 0;
            nextAttackTime = Time.time + attackSpeed+3.0f;
        }
    }
    public void Die()
    {
        if (hasDied) return;
        hasDied = true;
        isDying = true;
        deathTimer = 0f;
        if (GlobalVariables.phaseCounter == 6)
        {
            GlobalVariables.bossCounter = 15;
        }

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
    public void StartWarningFlash()
    {
            warningCoroutine = StartCoroutine(WarningFlashCoroutine());
    }

    public void StopWarningFlash()
    {
        StopCoroutine(warningCoroutine);
        warningObject.gameObject.SetActive(false);
        warningObject2.gameObject.SetActive(false);
    }

    private IEnumerator WarningFlashCoroutine()
    {
        warningObject.gameObject.SetActive(true);
        warningObject2.gameObject.SetActive(true);
        int index = 0;
        
        while (true)
        {
            warningObject.sprite = warningImage[index];
            warningObject2.sprite = warningImage[index];
            index = (index + 1) % warningImage.Length;
            yield return new WaitForSeconds(warningFlashSpeed);
        }
    }
}
