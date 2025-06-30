using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Player3Code : MonoBehaviour
{
    [HideInInspector] public float speed = 5.0f;
   [HideInInspector] public float baseSpeed = 5.0f;

    [HideInInspector] public Renderer playerRenderer;
    private Color originalColor;
    private Color shieldColor;
    private Color focusColor;

    public GameObject unfocusedShot;
    public GameObject focusedShot;
    public GameObject destructiveShot;
    public GameObject chargedShot;

    public GameObject box;
    public float fireRate = 0.15f;
    [HideInInspector] public float life = 3.0f;

    private float nextFireTime = 0f;
    private float switchTime = 0f;
    private Rigidbody rb;
    [HideInInspector] public bool focus = false;

    private bool isInvincible = false;
    private float invincibilityDuration = 3.0f;
    private MeshRenderer meshRenderer;

    private bool canShield = true;
    private bool isHealable = true;
    private bool hasShield = false;
    public GameObject shieldPrefab;
    public GameObject boss;

    private float minX = -8.7f;
    private float maxX = 5.4f;
    private float minZ = -8.7f;
    private float maxZ = 10.5f;

    [HideInInspector] public bool isDestructive = true;
    public AudioClip hitSound;
    public AudioClip powerUp;

    public AudioClip charging;
    private bool isCharged=false;
    private float chargeRate=0f;
    public VisualEffect chargeOn;
    private bool chargingOn = false;
    private float chargeCount = 0f;
    private bool countingUp = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        playerRenderer = GetComponent<Renderer>();
        playerRenderer.material.SetColor("_Color", Color.white);
        originalColor = playerRenderer.material.GetColor("_Color");
        shieldColor = new Color(0.2f, 0.6f, 1f, 1f);
        chargeOn.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift) ||
            Input.GetKeyUp(KeyCode.L) || Input.GetKeyUp(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
        {
            if (countingUp)
            {
                nextFireTime = 0f;
            }
            countingUp = false;
            isCharged = false;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            life = 9999;
        }
    }
    void FixedUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        transform.position = clampedPosition;
        RepeatableCode.HandleFocusMode(ref speed, ref focus, baseSpeed);
        RepeatableCode.HandleMovement(ref speed, ref rb);
        HandleBulletMode();
        HandleShooting();

        if (GlobalVariables.waveCounter % 7 == 0 && isHealable == true && life < 3 && GlobalVariables.waveCounter > 1)
        {
            isHealable = false;
            HandleHeal();
        }
        else if (GlobalVariables.waveCounter == 20 && GlobalVariables.phaseCounter == 1)
        {
            BossFight();
        }
        else if (GlobalVariables.waveCounter % 5 == 0 && GlobalVariables.waveCounter % 7 != 0)
        {
            isHealable = true;
        }
        if (GlobalVariables.waveCounter % 5 == 0 && !hasShield && GlobalVariables.waveCounter > 1 && canShield)
        {
            HandleShield();
        }
        if (GlobalVariables.waveCounter % 4 == 0 && GlobalVariables.waveCounter % 5 != 0)
        {
            canShield = true;
        }
        if (boss == null)
        {
            GlobalVariables.bossCounter=50;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (hasShield)
            {
                focusColor = shieldColor * 1.5f;
                playerRenderer.material.color = focusColor;
            }
            else
            {
                focusColor = originalColor * 1.5f;
                playerRenderer.material.color = focusColor;
            }
        }
        else
        {

            if (hasShield)
            {
                playerRenderer.material.color = shieldColor;
            }
            else
            {
                playerRenderer.material.color = originalColor;
            }
        }
        if (chargingOn)
        {
            if(chargeRate==2)
            {
                RepeatableCode.PlaySound(charging, gameObject.transform.position);
            }
            if (countingUp)
            {
                if (Time.time >= chargeCount)
                {
                    chargeRate += 1;
                    chargeCount = Time.time + 1f;
                }
            }
            chargeOn.gameObject.SetActive(true);
            VisualEffect vfxTrail = GameObject.Instantiate(chargeOn, gameObject.transform.position, Quaternion.identity);
            vfxTrail.Play();
            GameObject.Destroy(vfxTrail.gameObject, 0.1f*chargeRate-0.1f);
            chargeOn.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            if (hasShield && !isInvincible)
            {
                playerRenderer.material.color = originalColor;
                StartCoroutine(ActivateInvincibility());
                RepeatableCode.PlaySound(hitSound, gameObject.transform.position);
                hasShield = false;
            }
            else if (!isInvincible)
            {
                RepeatableCode.TakeDamagePlayer(ref life, 1, gameObject, hitSound);
                StartCoroutine(ActivateInvincibility());
            }
        }
        else if (collision.gameObject.CompareTag("shield"))
        {
            RepeatableCode.PlaySound(powerUp, gameObject.transform.position);
            hasShield = true;
            Destroy(collision.gameObject);
            playerRenderer.material.color = shieldColor;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            if (hasShield && !isInvincible)
            {
                playerRenderer.material.color = originalColor;
                StartCoroutine(ActivateInvincibility());
                RepeatableCode.PlaySound(hitSound, gameObject.transform.position);
                hasShield = false;
            }
            else if (!isInvincible)
            {
                RepeatableCode.TakeDamagePlayer(ref life, 1, gameObject, hitSound);
                StartCoroutine(ActivateInvincibility());
            }
        }
        else if (other.gameObject.CompareTag("heal") && life <= 3)
        {
            RepeatableCode.PlaySound(powerUp, gameObject.transform.position);
            life += 1;
        }
    }

    void HandleShooting()
    {
        if (Input.GetKey(KeyCode.Z) && Time.time >= nextFireTime || (Input.GetKey(KeyCode.L) && Time.time >= nextFireTime))
        {
            GameObject selectedShot;
            if (focus == true)
            {
                if (isDestructive)
                {
                    selectedShot = chargedShot;
                    fireRate = 6.0f;
                    countingUp = true;
                }
                else
                {
                    selectedShot = focusedShot;
                    fireRate = 0.3f;
                }
            }
            else
            {
                if (isDestructive == false)
                {
                    selectedShot = unfocusedShot;
                    fireRate = 0.12f;
                }
                else
                {
                    selectedShot = destructiveShot;
                    fireRate = 0.5f;
                }
            }
            if (selectedShot == chargedShot)
            {
                if (isCharged == false)
                {
                    nextFireTime = Time.time + (fireRate-chargeRate);
                    isCharged = true;  
                    chargingOn = true;
                }else if (Time.time >= nextFireTime)
                {
                    Vector3 spawnPosition = transform.position + transform.forward * 1.0f;
                    Instantiate(chargedShot, spawnPosition, transform.rotation);
                    chargeRate=0f;
                    isCharged=false;
                    chargingOn=false;
                    nextFireTime = 0f;
                }
            }
            else
            {
                Vector3 spawnPosition = transform.position + transform.forward * 1.0f;
                Instantiate(selectedShot, spawnPosition, transform.rotation);
                nextFireTime = Time.time + fireRate;
                isCharged = false;
            }
        }
    }
    void HandleHeal()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-7f, 5f), 5f, Random.Range(-7f, -2f));
        Instantiate(box, spawnPosition, transform.rotation);
    }

    void HandleShield()
    {
        canShield = false;
        Vector3 spawnPosition = new Vector3(Random.Range(-7f, 5f), 5f, Random.Range(-7f, -2f));
        Instantiate(shieldPrefab, spawnPosition, transform.rotation);
    }
    void HandleBulletMode()
    {
        if (Input.GetKey(KeyCode.K) && Time.time >= switchTime || Input.GetKey(KeyCode.X) && Time.time >= switchTime)
        {
            if (isDestructive == true)
            {
                isDestructive = false;
            }
            else
            {
                isDestructive = true;
            }
            switchTime = Time.time + 0.3f;
        }
    }

    void BossFight()
    {
        GlobalVariables.currentScore = GlobalVariables.score;
        SceneManager.LoadScene("Boss1");
    }

    private System.Collections.IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        Collider playerCollider = GetComponent<Collider>();
        Collider[] enemyColliders = FindObjectsOfType<Collider>();

        foreach (var enemyCollider in enemyColliders)
        {
            if (enemyCollider != null && enemyCollider.CompareTag("enemy"))
            {
                Physics.IgnoreCollision(playerCollider, enemyCollider, true);
            }
        }

        float blinkInterval = 0.2f;
        float elapsed = 0f;

        while (elapsed < invincibilityDuration)
        {
            if (meshRenderer != null)
                meshRenderer.enabled = !meshRenderer.enabled;

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        if (meshRenderer != null)
            meshRenderer.enabled = true;
        foreach (var enemyCollider in enemyColliders)
        {
            if (enemyCollider != null && enemyCollider.CompareTag("enemy"))
            {
                Physics.IgnoreCollision(playerCollider, enemyCollider, false);
            }
        }

        isInvincible = false;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}