using UnityEngine;
using UnityEngine.VFX;

public class MegaKamikaze : MonoBehaviour
{
    public Transform player;
    public Transform kamiSelf;
    public float speed = 4.0f;
    public float baseSpeed = 4.0f;
    public Player2Code playerScript;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;
    public VisualEffect trail;
    public Color vengeance;
    public float life = 30.0f;

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

    private float attackSpeed = 7.0f;
    private float nextAttackTime = 0.0f;

    public GameObject kamikazePrefab;

    void Start()
    {
        kamiSelf.localScale *= 1.5f;
        deathExplosion.gameObject.SetActive(false);
        damageTaken.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        enemyRenderer = GetComponent<Renderer>();
        enemyRenderer.material.SetColor("_Color", Color.white);
        originalColor = enemyRenderer.material.color;
        darkenedColor = originalColor * 0.5f;
        vengeance = new Color(1f, 0f, 0f, 1f);
        trail.gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        RepeatableCode.HandleFocusMode(ref speed, ref focus, baseSpeed);
        MoveTowardPlayer();
        HandleDarkeningEffect();
        trail.gameObject.SetActive(true);
        VisualEffect vfxTrail = GameObject.Instantiate(trail, kamiSelf.position, Quaternion.identity);
        vfxTrail.Play();
        GameObject.Destroy(vfxTrail.gameObject, 2f);
        trail.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            SpawnEnemy();
            nextAttackTime = Time.time + attackSpeed;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("focusShot"))
        {
            life -= 2;

            RepeatableCode.PlaySound(hitSound, kamiSelf.position);

            damageTaken.gameObject.SetActive(true);
            VisualEffect vfxDamage = GameObject.Instantiate(damageTaken, kamiSelf.position, Quaternion.identity);
            vfxDamage.Play();
            GameObject.Destroy(vfxDamage.gameObject, 0.5f);
            damageTaken.gameObject.SetActive(false);
                if (life <= 0)
                {
                    GlobalVariables.bossCounter = 15;
                    RepeatableCode.Die(deathExplosion, kamiSelf.position, kamiSelf.rotation);
                    RepeatableCode.PlaySound(enemyDeathSound, kamiSelf.position);
                    GameObject.Destroy(gameObject);
                    GlobalVariables.score += 500;
                    GlobalVariables.waveCounter += 1;
                }
        }
        else if (collision.gameObject.CompareTag("unfocusShot"))
        {
            life -= 1;

            RepeatableCode.PlaySound(hitSound, kamiSelf.position);

            damageTaken.gameObject.SetActive(true);
            VisualEffect vfxDamage = GameObject.Instantiate(damageTaken, kamiSelf.position, Quaternion.identity);
            vfxDamage.Play();
            GameObject.Destroy(vfxDamage.gameObject, 0.5f);
            damageTaken.gameObject.SetActive(false);
                if (life <= 0)
                {
                    GlobalVariables.bossCounter = 15;
                    RepeatableCode.Die(deathExplosion, kamiSelf.position, kamiSelf.rotation);
                    RepeatableCode.PlaySound(enemyDeathSound, kamiSelf.position);
                    GameObject.Destroy(gameObject);
                    GlobalVariables.score += 500;
                    GlobalVariables.waveCounter += 1;
                }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            life -= 5;

            RepeatableCode.PlaySound(hitSound, kamiSelf.position);

            damageTaken.gameObject.SetActive(true);
            VisualEffect vfxDamage = GameObject.Instantiate(damageTaken, kamiSelf.position, Quaternion.identity);
            vfxDamage.Play();
            GameObject.Destroy(vfxDamage.gameObject, 0.5f);
            damageTaken.gameObject.SetActive(false);
                if (life <= 0)
                {
                    GlobalVariables.bossCounter = 15;
                    RepeatableCode.Die(deathExplosion, kamiSelf.position, kamiSelf.rotation);
                    RepeatableCode.PlaySound(enemyDeathSound, kamiSelf.position);
                    GameObject.Destroy(gameObject);
                    GlobalVariables.waveCounter += 1;
                }
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
    void SpawnEnemy()
    {

        GameObject enemy = Instantiate(kamikazePrefab, kamiSelf.position, Quaternion.identity);

        Kamikaze2 script = enemy.GetComponent<Kamikaze2>();
        if (player != null)
        {
            script.player = player;
            script.playerScript = player.GetComponent<Player2Code>();
        }
    }
}
