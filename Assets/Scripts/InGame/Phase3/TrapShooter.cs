using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class TrapShooter : MonoBehaviour
{
    Rigidbody rb;
    private float fireRate = 3f;
    private float nextFireTime = 0.0f;

    public GameObject bullet;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;

    public Transform enemy;
    private Vector3 shootDirection = new Vector3(0f, 0f, -100f);
    private Vector3 positionPoint = new Vector3(-2f, 5f, 2f);
    public float baseSpeed = 6.0f;

    private float speed = 6.0f;
    public float life = 10.0f;

    public AudioClip enemyDeathSound;
    public AudioClip hitSound;

    public float minX = -8f;
    public float maxX = 5f;
    Vector3 direction = new Vector3(0f, 0f, -100f);

    private Vector3 moveDirection = new Vector3(100f, 0f, 0f);

    private TrapProjectile bossShot;

    void Start()
    {
        nextFireTime = Time.time + 1.0f;
        enemy.LookAt(shootDirection);
        deathExplosion.gameObject.SetActive(false);
        damageTaken.gameObject.SetActive(false);
        bossShot = bullet.GetComponent<TrapProjectile>();
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
    }


void Update()
{
    if (Time.time >= nextFireTime)
    {
        Vector3 fireDirection = Vector3.back;

        GameObject proj = Instantiate(bullet, enemy.position, Quaternion.identity);

        TrapProjectile bossProj = proj.GetComponent<TrapProjectile>();
        if (bossProj != null)
        {
            bossProj.direction = fireDirection.normalized;
        }
        enemy.LookAt(enemy.position + fireDirection);

        nextFireTime = Time.time + fireRate;
    }
}

    void FixedUpdate()
    {

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
        }else if (GlobalVariables.waveCounter >= 5)
        {
            fireRate = 5.0f;
            MoveTowardPosition();
        }
        else
        {
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
