using UnityEngine;
using UnityEngine.VFX;

public class Movable2Enemy : MonoBehaviour
{
    Rigidbody rb;
    private float fireRate = 2.0f;
    [HideInInspector] public float nextFireTime = 0f;

    public GameObject bullet;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;

    public Transform enemy;
    [HideInInspector] public Vector3 shootDirection = new Vector3(0f, 0f, -100f);
    private Vector3 positionPoint = new Vector3(-6f, 5f, 12f);
    public float bulletSpeed = 6.0f;
    private float baseSpeed = 6.0f;

    private float speed = 6.0f;

    private bool focus = false;
    public float life = 10.0f;

    public AudioClip enemyDeathSound;
    public AudioClip hitSound;

    private BossAttacks bossAttacks;

    public GameObject superBullet;

    private BossProjectile bossShot;

    bool gigantism = false; 
    void Start()
    {
        deathExplosion.gameObject.SetActive(false);
        damageTaken.gameObject.SetActive(false);
        bossAttacks = GetComponent<BossAttacks>();
        bossShot = superBullet.GetComponent<BossProjectile>();
        if (GlobalVariables.bossCounter == 50)
        {
            life = 30;
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("focusShot"))
        {
            RepeatableCode.TakeDamage(ref life, 2, gameObject, deathExplosion, enemy.position, enemy.rotation, damageTaken, enemyDeathSound, hitSound);
        }
        else if (collision.gameObject.CompareTag("unfocusShot"))
        {
            RepeatableCode.TakeDamage(ref life, 1, gameObject, deathExplosion, enemy.position, enemy.rotation, damageTaken, enemyDeathSound, hitSound);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (GlobalVariables.bossCounter == 50)
            {
                life -= 0;
            }
            else
            {
                GlobalVariables.score += 100;
                RepeatableCode.Die(deathExplosion, enemy.position, enemy.rotation);
                Destroy(gameObject);
                GlobalVariables.waveCounter += 1;
            }
        }
    }

    void Update()
    {
        if ((GlobalVariables.waveCounter >= 9 && GlobalVariables.waveCounter <= 13||GlobalVariables.bossCounter==50)
            && Time.time >= nextFireTime)
        {
            if (!gigantism)
            {
                enemy.localScale *= 1.5f;
                gigantism = true;
            }
            bossShot.speed = 3.0f;
            bossShot.baseSpeed = 3.0f;
            enemy.LookAt(shootDirection);
            if (GlobalVariables.bossCounter == 50)
            {
                fireRate = 2.0f;
                bossAttacks.ShootArc(superBullet, enemy, 3);
            }
            else
            {
                fireRate = 5.0f;
                bossAttacks.ShootArc(superBullet, enemy, 5);
            }
            nextFireTime = Time.time + fireRate;
        }
        else if (Time.time >= nextFireTime)
        {
            enemy.LookAt(shootDirection);
            RepeatableCode.EnemyAttack(bullet, enemy.position + enemy.forward * 0.5f, enemy.rotation,
                baseSpeed);
            nextFireTime = Time.time + fireRate;
        }
        else
        {
            enemy.LookAt(shootDirection);
        }
    }
    void FixedUpdate()
    {
        RepeatableCode.HandleFocusMode(ref bulletSpeed, ref focus, baseSpeed);
        MoveTowardPosition();
        if (positionPoint.z <= -4)
        {
            positionPoint.z += 1;
            MoveTowardPosition();
        }
    }
    void MoveTowardPosition()
    {
        Vector3 direction = (positionPoint - transform.position);

        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            speed = 0f;
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        positionPoint = targetPosition;
    }
    public void SetShootDirection(Vector3 newDirection)
    {
        shootDirection = newDirection;
    }
}
