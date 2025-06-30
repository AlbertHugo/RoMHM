using UnityEngine;
using UnityEngine.VFX;

public class Movable3Enemy : MonoBehaviour
{
    Rigidbody rb;
    private float fireRate = 2.0f;
    [HideInInspector] public float nextFireTime = 0f;

    public GameObject bullet;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;

    public Transform enemy;
    private Vector3 shootDirection = new Vector3(0f, 0f, -100f);
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

    public float minX = -8f;
    public float maxX = 5f;

    private Vector3 moveDirection = new Vector3(100f, 0f, 0f);
    void Start()
    {
        enemy.LookAt(shootDirection);
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
        else if (collision.gameObject.CompareTag("chargedShot"))
        {
            RepeatableCode.TakeDamage(ref life, 60, gameObject, deathExplosion, enemy.position, enemy.rotation, damageTaken, enemyDeathSound, hitSound);
        }  
    }

    void Update()
    {
            enemy.LookAt(shootDirection);
            if (Time.time >= nextFireTime)
            {
                enemy.LookAt(shootDirection);
                RepeatableCode.EnemyAttack(bullet, enemy.position + enemy.forward * 0.5f, enemy.rotation,
                    baseSpeed);
                nextFireTime = Time.time + fireRate;
            }
    }
    void FixedUpdate()
    {
        RepeatableCode.HandleFocusMode(ref bulletSpeed, ref focus, baseSpeed);
        if (GlobalVariables.waveCounter >= 9 && GlobalVariables.waveCounter <= 12)
        {
            transform.position += moveDirection.normalized * speed * Time.fixedDeltaTime;

            if (transform.position.x >= maxX || transform.position.x <= minX)
            {
                moveDirection.x *= -1;
            }
            if (transform.position.z >= 6f || transform.position.z <= -4f)
            {
                moveDirection.z *= -1;
            }
        }
        else
        {
        MoveTowardPosition();
        if (positionPoint.z <= -4)
        {
            positionPoint.z += 1;
            MoveTowardPosition();
        }   
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