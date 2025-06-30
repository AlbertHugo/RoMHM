using UnityEngine;

public class TrapProjectile : MonoBehaviour
{
    public Transform self;
    private BossAttacks bossAttacks;
    private float speed = 3f;
    [HideInInspector] public Vector3 direction = new Vector3(0f, 0f, -1f);

    [HideInInspector] public float baseSpeed = 3.0f;

    public GameObject projectilePrefab;

    Rigidbody rb;
    private float explosionSpeed;
    public void SetSpeed(float s) => speed = baseSpeed = s;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        bossAttacks = GetComponent<BossAttacks>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 inputMovement = new Vector3(moveHorizontal, 0f, 0f);
        Vector3 forwardMovement = direction * speed;
        Vector3 totalMovement = (forwardMovement + inputMovement * speed) * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + totalMovement);
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            ShootTrap(projectilePrefab, self, 8, 2f);
            Destroy(gameObject);
        }
        if (GlobalVariables.bossCounter == 15)
        {
            Destroy(gameObject);
        }
    }
    public void ActivateTrap()
    {
        ShootTrap(projectilePrefab, self, 6, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("unfocusShot"))
        {
            Destroy(gameObject);
        }
    }
    private void ShootTrap(GameObject projectile, Transform firePoint, int projectileCount, float trapSpeed)
    {
        float angleStep = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            float radians = angle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, Mathf.Cos(radians));

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

            TrapActiveProjectile projScript = proj.GetComponent<TrapActiveProjectile>();
            if (projScript != null)
            {
                projScript.direction = direction.normalized;
                projScript.SetSpeed(trapSpeed);
            }
        }
}
}
