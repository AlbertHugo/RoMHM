using UnityEngine;
using UnityEngine.VFX;

public class MovableEnemy : MonoBehaviour
{
    Rigidbody rb;
    private float fireRate = 2.0f;
    private float nextFireTime = 0f;
    private bool canSpin = true;

    public GameObject bullet;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;

    public Transform enemy;
    [HideInInspector] public Vector3 shootDirection = new Vector3(0f, 0f, -100f);
    private Vector3 positionPoint = new Vector3(-6f, 5f, 4f);
    public float bulletSpeed = 6.0f;
    private float baseSpeed = 6.0f;

    private float speed = 6.0f;

    private bool focus = false;
    public float life = 10.0f;

    public AudioClip enemyDeathSound;
    public AudioClip hitSound;
    void Start()
    {
        deathExplosion.gameObject.SetActive(false);
        damageTaken.gameObject.SetActive(false);
        if (GlobalVariables.bossCounter > 0)
        {
            canSpin = false;
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
            GlobalVariables.enemiesAlive = GlobalVariables.enemiesAlive - 1;
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
            GlobalVariables.enemiesAlive = GlobalVariables.enemiesAlive - 1;
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

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            enemy.LookAt(shootDirection);
            RepeatableCode.EnemyAttack(bullet, enemy.position + enemy.forward * 0.5f, enemy.rotation, baseSpeed);
            nextFireTime = Time.time + fireRate;

        }
    }
    void FixedUpdate()
    {
        if (GlobalVariables.bossCounter == 15)
        {
            RepeatableCode.Die(deathExplosion, enemy.position, enemy.rotation);
            Destroy(gameObject);
        }
        if (canSpin&&(GlobalVariables.waveCounter == 13 || GlobalVariables.waveCounter == 14))
        {
            shootDirection = new Vector3(0f, 0f, 100f);
        }
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
