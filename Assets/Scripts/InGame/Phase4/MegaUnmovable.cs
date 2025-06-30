using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class MegaUnmovable : MonoBehaviour
{
    Rigidbody rb;
    private float fireRate = 5f;
    private float nextFireTime = 0.0f;

    public GameObject bullet;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;

    public Transform enemy;
    private Vector3 shootDirection = new Vector3(0f, 0f, -100f);
    private Vector3 positionPoint = new Vector3(-2f, 5f, 2f);
    private float bulletSpeed = 6.0f;
    public float baseSpeed = 6.0f;

    private float speed = 6.0f;
    private bool focus = false;
    public float life = 30.0f;

    public AudioClip enemyDeathSound;
    public AudioClip hitSound;

    public float minX = -8f;
    public float maxX = 5f;
    Vector3 direction = new Vector3(0f, 0f, -100f);

    private Vector3 moveDirection = new Vector3(0f, 0f, -100f);

    private ProjectileUnmovable bossShot;

    private BossAttacks bossAttacks;

    bool gigantism = false;

    void Start()
    {
        nextFireTime = Time.time + 2.0f;
        enemy.LookAt(shootDirection);
        deathExplosion.gameObject.SetActive(false);
        damageTaken.gameObject.SetActive(false);
        bossShot = bullet.GetComponent<ProjectileUnmovable>();
        bossAttacks = GetComponent<BossAttacks>();
    }

    private void OnTriggerEnter(Collider collision)
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
            GlobalVariables.score += 100;
            RepeatableCode.Die(deathExplosion, enemy.position, enemy.rotation);
            Destroy(gameObject);
            GlobalVariables.waveCounter += 1;
        }
        else if (collision.gameObject.CompareTag("chargedShot"))
        {
            RepeatableCode.TakeDamage(ref life, 60, gameObject, deathExplosion, enemy.position, enemy.rotation, damageTaken, enemyDeathSound, hitSound);
        }  
    }

    void OnCollisionEnter(Collision collision)
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
            GlobalVariables.score += 100;
            RepeatableCode.Die(deathExplosion, enemy.position, enemy.rotation);
            Destroy(gameObject);
            GlobalVariables.waveCounter += 1;
        }
        else if (collision.gameObject.CompareTag("chargedShot"))
        {
            RepeatableCode.TakeDamage(ref life, 60, gameObject, deathExplosion, enemy.position, enemy.rotation, damageTaken, enemyDeathSound, hitSound);
        }
    }


void Update()
{
    if (Time.time >= nextFireTime)
    {
                bossAttacks.ShootRadial(bullet, enemy, 24, 2f);
                nextFireTime = Time.time + fireRate;
    }
}

    void FixedUpdate()
    {
        if(GlobalVariables.bossCounter==15)
        {
            RepeatableCode.Die(deathExplosion, enemy.position, enemy.rotation);
            Destroy(gameObject);
        }
        enemy.LookAt(shootDirection);
        RepeatableCode.HandleFocusMode(ref bulletSpeed, ref focus, baseSpeed);
            if (!gigantism)
            {
                enemy.localScale *= 1.5f;
                gigantism = true;
            }
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
