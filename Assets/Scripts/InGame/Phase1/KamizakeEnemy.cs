using UnityEngine;
using UnityEngine.VFX;

public class KamikazeEnemy : MonoBehaviour
{
    public Transform player;
    public Transform kamiSelf;
    public float speed = 4.0f;
    public float baseSpeed = 4.0f;
    public PlayerCode playerScript;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;
    [HideInInspector] public float life = 10.0f;

    private Rigidbody rb;
    private bool focus = false;
    private Renderer enemyRenderer;
    private Color originalColor;
    private Color darkenedColor;

    public AudioClip enemyDeathSound;
    public AudioClip hitSound;

    public LayerMask kamikazeLayer;
    float avoidanceRadius = 1f;
    float avoidanceStrength = 0.3f;

    void Start()
    {
        deathExplosion.gameObject.SetActive(false);
        damageTaken.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        enemyRenderer = GetComponent<Renderer>();
        enemyRenderer.material.SetColor("_Color", Color.white);
        originalColor = enemyRenderer.material.color;
        darkenedColor = originalColor * 0.5f;
    }

    void FixedUpdate()
    {
        RepeatableCode.HandleFocusMode(ref speed, ref focus, baseSpeed);
        RepeatableCode.HandleMovement(ref speed, ref rb);
        MoveTowardPlayer();
        HandleDarkeningEffect();

        if (GlobalVariables.bossCounter == 15)
        {
            RepeatableCode.Die(deathExplosion, kamiSelf.position, kamiSelf.rotation);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("focusShot"))
        {
            RepeatableCode.TakeDamage(ref life, 2, gameObject, deathExplosion, kamiSelf.position, kamiSelf.rotation, damageTaken, enemyDeathSound, hitSound);
        }
        else if (collision.gameObject.CompareTag("unfocusShot"))
        {
            RepeatableCode.TakeDamage(ref life, 1, gameObject, deathExplosion, kamiSelf.position, kamiSelf.rotation, damageTaken, enemyDeathSound, hitSound);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            RepeatableCode.Die(deathExplosion, kamiSelf.position, kamiSelf.rotation);
            Destroy(gameObject);
            GlobalVariables.waveCounter += 1;
        }
    }

    void MoveTowardPlayer()
    {
        if (player == null || playerScript == null) return;

        Vector3 direction;
        if (playerScript.IsInvincible())
        {
            direction = (player.position * -1 - transform.position).normalized;
        }
        else
        {
            direction = (player.position - transform.position).normalized;
        }
        Collider[] others = Physics.OverlapSphere(transform.position, avoidanceRadius, kamikazeLayer);
        foreach (Collider other in others)
        {
            if (other.gameObject != gameObject)
            {
                Vector3 away = (transform.position - other.transform.position).normalized;
                direction += away * avoidanceStrength;
            }
        }

        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void HandleDarkeningEffect()
    {
        if (enemyRenderer == null) return;

        float y = transform.position.y;

        if (y > 3.7f && y < 6.2f)
        {
            enemyRenderer.material.color = originalColor;
        }
        else
        {
            enemyRenderer.material.color = darkenedColor;
        }
    }
}
