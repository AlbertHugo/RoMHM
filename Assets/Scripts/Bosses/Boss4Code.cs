using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

public class Boss4Code : MonoBehaviour
{
    public GameObject kamikaze;
    public GameObject projectilePrefab;
    public GameObject basicShot;
    public GameObject unmovableShot;
    public Transform enemy;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;

    public Transform player;
    private BossAttacks bossAttacks;

    private Vector3 shootDirection = new Vector3(0f, 0f, 100f);

    private float attackSpeed = 5.0f;
    private float nextAttackTime = 0.0f;
    
    public Color vengeance;

    //outras coisas do boss
    public bool focus = false;

    public float bossLife = 1000.0f;
    private float fakeLife = 120f;
    public AudioClip enemyDeathSound;
    public AudioClip hitSound;
    bool hasDied = false;

    private bool isDying = false;
    private float deathScoreInterval = 0.2f;
    private float deathTimer = 0f;
    private float totalDeathTime = 1.5f;

    public AudioClip scoreSound;
    private Renderer enemyRenderer;
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
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("focusShot"))
        {
            bossLife -= 2;
            fakeLife-=2;
            RepeatableCode.PlaySound(hitSound, enemy.position);
            TakeDamage();
        }
        else if (collision.gameObject.CompareTag("unfocusShot"))
        {
            bossLife -= 1;
            fakeLife-=1;
            RepeatableCode.PlaySound(hitSound, enemy.position);
            TakeDamage();
        }
        else if (collision.gameObject.CompareTag("chargedShot"))
        {
            bossLife -= 60;
            fakeLife-=60;
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
        if (fakeLife <= 0)
        {
            GlobalVariables.waveCounter += 1;
            fakeLife = 600;
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
        HandleAttacks();
    }
    void HandleAttacks()
    {
        if (GlobalVariables.bossCounter == 0 && Time.time >= nextAttackTime)
        {
            bossAttacks.KamikazeSpawn(kamikaze, enemy.position, player);
            nextAttackTime = Time.time + attackSpeed-4f;
            GlobalVariables.bossCounter += 1;
        }
        else if (GlobalVariables.bossCounter == 1 && Time.time >= nextAttackTime)
        {
            bossAttacks.KamikazeSpawn(kamikaze, enemy.position, player);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed+2f;
        }
        else if (GlobalVariables.bossCounter == 2 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootNormalRadial(basicShot, enemy, 24, 5f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed-4f;
        }
        else if (GlobalVariables.bossCounter == 3 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootRadial(unmovableShot, enemy, 24, 4f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed-4.0f;
        }
        else if (GlobalVariables.bossCounter == 4 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootTrapRadial(projectilePrefab, enemy, 24, 3f);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed+2f;
        }
        else if (GlobalVariables.bossCounter == 5 && Time.time >= nextAttackTime)
        {
            bossAttacks.StartSideColumnAttack(unmovableShot, zStart: -10f, zEnd: 10f, countPerColumn: 10, leftX: -8f, rightX: 5f, linesPerColumn: 2, columnWidth: 3f, delayBetweenLines: 0.1f, this);
            GlobalVariables.bossCounter += 1;
            nextAttackTime = Time.time + attackSpeed-4f;
        }
        else if (GlobalVariables.bossCounter == 6 && Time.time >= nextAttackTime)
        {
            bossAttacks.StartSideColumnAttack(basicShot, zStart: -10f, zEnd: 9.5f, countPerColumn: 8, leftX: -7f, rightX: 4.5f, linesPerColumn: 2, columnWidth: 3f, delayBetweenLines: 0.1f, this);
            GlobalVariables.bossCounter +=1;
            nextAttackTime = Time.time + attackSpeed-4f;
        }else if (GlobalVariables.bossCounter == 7 && Time.time >= nextAttackTime)
        {
            bossAttacks.StartSideColumnAttack(projectilePrefab, zStart: -10f, zEnd: 9f, countPerColumn: 6, leftX: -6f, rightX: 4f, linesPerColumn: 2, columnWidth: 3f, delayBetweenLines: 0.1f, this);
            GlobalVariables.bossCounter +=1;
            nextAttackTime = Time.time + attackSpeed+2f;
        }else if (GlobalVariables.bossCounter == 8 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootArc(basicShot, enemy, 10);
            GlobalVariables.bossCounter +=1;
            nextAttackTime = Time.time + attackSpeed-4f;
        }else if (GlobalVariables.bossCounter == 9 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootUnmovableArc(unmovableShot, enemy, 10);
            GlobalVariables.bossCounter +=1;
            nextAttackTime = Time.time + attackSpeed-4f;
        }else if (GlobalVariables.bossCounter == 10 && Time.time >= nextAttackTime)
        {
            bossAttacks.ShootTrapArc(projectilePrefab, enemy, 10);
            GlobalVariables.bossCounter +=1;
            nextAttackTime = Time.time + attackSpeed+2f;
        }else if (GlobalVariables.bossCounter == 11 && Time.time >= nextAttackTime)
        {
            bossAttacks.ApplyPlayerDebuff(player, 5f, 2.5f, enemyRenderer, vengeance);
            GlobalVariables.bossCounter +=1;
            nextAttackTime = Time.time + attackSpeed+1f;
        }else if (GlobalVariables.bossCounter == 12 && Time.time >= nextAttackTime)
        {
            bossAttacks.SideColumnAttack(projectilePrefab, zStart: -5f, zEnd: 5f, countPerColumn: 3, leftX: -8f, rightX: 5f, linesPerColumn: 2, columnWidth: 3f, delayBetweenLines: 0.5f, this);
            GlobalVariables.bossCounter +=1;
            nextAttackTime = Time.time + attackSpeed+2f;
        }else if (GlobalVariables.bossCounter == 13 && Time.time >= nextAttackTime)
        {
            bossAttacks.SpeedControl(player, 5f, 8f, enemyRenderer, vengeance);
            GlobalVariables.bossCounter = 0;
            nextAttackTime = Time.time + attackSpeed+1f;
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
