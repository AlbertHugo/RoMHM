using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Audio;

public class BossCode : MonoBehaviour
{
    public GameObject projectilePrefab;

    public GameObject kamikazePrefab;
    public Transform enemy;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;

    public Transform player;
    private BossAttacks bossAttacks;

    public float bossLife = 100.0f;

    [HideInInspector] public Vector3 shootDirection = new Vector3(0f, 0f, -100f);

    private float attackSpeed = 3.0f;
    private float nextAttackTime = 0.0f;

    public bool focus = false;

    public float speed = 5.0f;
    public float baseSpeed = 5.0f;

    private Vector3 moveDirection = Vector3.right;
    public float minX = -8f;
    public float maxX = 5f;

    public AudioClip enemyDeathSound;
    public AudioClip hitSound;
    bool hasDied=false;
    
    private bool isDying = false;
    private float deathScoreInterval = 0.2f;
    private float deathTimer = 0f;
    private float totalDeathTime = 1.5f;

    public AudioClip scoreSound;

    void Start()
    {
        deathExplosion.gameObject.SetActive(false);
        damageTaken.gameObject.SetActive(false);
        GlobalVariables.waveCounter = 0;
        enemy.LookAt(shootDirection);
        bossAttacks = GetComponent<BossAttacks>();
        nextAttackTime = Time.time + attackSpeed;
    }

    void OnCollisionEnter(Collision collision)
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
        if (bossLife <= 0)
        {
            RepeatableCode.PlaySound(enemyDeathSound, enemy.position);
            Die();
        }

    }
    void FixedUpdate()
    {
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
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        if (transform.position.x >= maxX)
        {
            moveDirection = Vector3.left;
        }
        else if (transform.position.x <= minX)
        {
            moveDirection = Vector3.right;
        }
        HandleAttacks();
        RepeatableCode.HandleFocusMode(ref speed, ref focus, baseSpeed);
    }
    void HandleAttacks()
    {
        if (GlobalVariables.bossCounter == 0 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootArc(projectilePrefab, enemy, 10);
            nextAttackTime = Time.time + attackSpeed;
            GlobalVariables.bossCounter += 1;
        }
        else if (GlobalVariables.bossCounter == 1 && Time.time >= nextAttackTime)
        {
            bossAttacks.MinionSpawn(kamikazePrefab, enemy.position, player);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed - 2f;
        }
        else if (GlobalVariables.bossCounter == 2 && Time.time >= nextAttackTime)
        {
            bossAttacks.MinionSpawn(kamikazePrefab, enemy.position, player);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (GlobalVariables.bossCounter == 3 && Time.time >= nextAttackTime)
        {
            bossAttacks.StartSideColumnAttack(projectilePrefab, zStart: -10f, zEnd: 9f, countPerColumn: 10, leftX: -8f, rightX: 5f, linesPerColumn: 3, columnWidth: 2f, delayBetweenLines: 0.2f, this);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (GlobalVariables.bossCounter == 4 && Time.time >= nextAttackTime)
        {
            bossAttacks.MinionSpawn(kamikazePrefab, enemy.position, player);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed - 2f;
        }
        else if (GlobalVariables.bossCounter == 5 && Time.time >= nextAttackTime)
        {
            bossAttacks.MinionSpawn(kamikazePrefab, enemy.position, player);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed - 2f;
        }
        else if (GlobalVariables.bossCounter == 6 && Time.time >= nextAttackTime)
        {
            bossAttacks.MinionSpawn(kamikazePrefab, enemy.position, player);
            GlobalVariables.bossCounter = 0;
            nextAttackTime = Time.time + attackSpeed - 1f;
        }
        if (gameObject == null)
        {
            GlobalVariables.bossCounter = 20;
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
