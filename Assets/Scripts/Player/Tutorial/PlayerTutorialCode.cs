using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTutorialCode : MonoBehaviour
{
    private float speed = 5.0f;
    private float baseSpeed = 5.0f;

    public GameObject unfocusedShot;
    public GameObject focusedShot;
    public float fireRate = 0.15f;
    [HideInInspector] public float life = 3.0f;

    private float nextFireTime = 0f;
    private Rigidbody rb;
    [HideInInspector] public bool focus = false;

    private bool isInvincible = false;
    private float invincibilityDuration = 3.0f;
    private Collider playerCollider;
    private MeshRenderer meshRenderer;

    private float minX = -8.7f;
    private float maxX = 5.4f;
    private float minZ = -8.7f;
    private float maxZ = 10.5f;

    public AudioClip hitSound;


    private Renderer playerRenderer;
    private Color originalColor;
    private Color focusColor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        playerRenderer = GetComponent<Renderer>();
        originalColor = playerRenderer.material.GetColor("_Color");
        focusColor = originalColor * 1.5f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            life = 9999;
        }
    }

    void FixedUpdate()
    {
        Debug.Log(GlobalVariables.waveCounter);
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        transform.position = clampedPosition;
        RepeatableCode.HandleFocusMode(ref speed, ref focus, baseSpeed);
        RepeatableCode.HandleMovement(ref speed, ref rb);
        HandleShooting();
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            playerRenderer.material.color = focusColor;
        }
        else
        {
            playerRenderer.material.color = originalColor;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy") && !isInvincible)
        {
            RepeatableCode.TakeDamagePlayer(ref life, 1, gameObject, hitSound);
            StartCoroutine(ActivateInvincibility());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") && !isInvincible)
        {
            RepeatableCode.TakeDamagePlayer(ref life, 1, gameObject, hitSound);
            StartCoroutine(ActivateInvincibility());
        }
    }

    void HandleShooting()
    {
        if ((Input.GetKey(KeyCode.Z)) && Time.time >= nextFireTime || (Input.GetKey(KeyCode.L) && Time.time >= nextFireTime))//tire o &&Time.time e dali em diante de dentro do if para ativar o tiro infinito
        {
            GameObject selectedShot = focus ? focusedShot : unfocusedShot;
            fireRate = focus ? 0.3f : 0.15f;

            nextFireTime = Time.time + fireRate;
            Vector3 spawnPosition = transform.position + transform.forward * 2.0f;
            Instantiate(selectedShot, spawnPosition, transform.rotation);
        }
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