using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Audio;

public class Kamikaze3 : MonoBehaviour
{
    public Transform player;
    public Transform kamiSelf;
    public float speed = 4.0f;
    public float baseSpeed = 4.0f;
    public Player3Code playerScript;
    public VisualEffect deathExplosion;
    public VisualEffect damageTaken;
    public VisualEffect trail;
    public Color vengeance;
    public float life = 10.0f;

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
        vengeance = new Color(1f, 0f, 0f, 1f);
        trail.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        RepeatableCode.HandleFocusMode(ref speed, ref focus, baseSpeed);
        RepeatableCode.HandleMovement(ref speed, ref rb);
        MoveTowardPlayer();
        HandleDarkeningEffect();
        if (GlobalVariables.phaseCounter == 7)
        {
            speed = 4.0f;
            baseSpeed = 4.0f;
            if (GlobalVariables.waveCounter >= 9 && GlobalVariables.waveCounter < 13 && speed < 7)
            {
                speed = GlobalVariables.waveCounter - 6;
                baseSpeed = GlobalVariables.waveCounter - 6;
                trail.gameObject.SetActive(true);
                VisualEffect vfxTrail = GameObject.Instantiate(trail, kamiSelf.position, Quaternion.identity);
                vfxTrail.Play();
                GameObject.Destroy(vfxTrail.gameObject, (GlobalVariables.waveCounter - 5) / 4);
                trail.gameObject.SetActive(false);
                vengeance.r = (GlobalVariables.waveCounter - 5) / 4;
                darkenedColor.r = (GlobalVariables.waveCounter - 5) / 4;
            }
        }

        if (GlobalVariables.bossCounter == 15)
        {
            RepeatableCode.Die(deathExplosion, kamiSelf.position, kamiSelf.rotation);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (GlobalVariables.waveCounter < 13||GlobalVariables.phaseCounter==8||GlobalVariables.phaseCounter==9)
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
                GlobalVariables.revengeCount += 1;
            }
            else if (collision.gameObject.CompareTag("chargedShot"))
            {
                RepeatableCode.TakeDamage(ref life, 60, gameObject, deathExplosion, kamiSelf.position, kamiSelf.rotation, damageTaken, enemyDeathSound, hitSound);
            }
        }
        else
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
                    RepeatableCode.Die(deathExplosion, kamiSelf.position, kamiSelf.rotation);
                    RepeatableCode.PlaySound(enemyDeathSound, kamiSelf.position);
                    Destroy(gameObject);
                    GlobalVariables.score += 100;
                    GlobalVariables.revengeCount += 1;
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
                    RepeatableCode.Die(deathExplosion, kamiSelf.position, kamiSelf.rotation);
                    RepeatableCode.PlaySound(enemyDeathSound, kamiSelf.position);
                    GameObject.Destroy(gameObject);
                    GlobalVariables.score += 100;
                    GlobalVariables.revengeCount += 1;
                }
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                RepeatableCode.Die(deathExplosion, kamiSelf.position, kamiSelf.rotation);
                Destroy(gameObject);
                GlobalVariables.revengeCount += 1;
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
            if (GlobalVariables.waveCounter >= 9&&GlobalVariables.waveCounter<=12&&GlobalVariables.phaseCounter==7)
            {
                enemyRenderer.material.color = vengeance;
            }
            else
            {
                enemyRenderer.material.color = originalColor;
            }
        }
        else
        {
            enemyRenderer.material.color = darkenedColor;
        }
    }
}