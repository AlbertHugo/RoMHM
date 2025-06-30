using UnityEngine;

public class TrapActiveProjectile : MonoBehaviour
{
    public Transform self;
    [HideInInspector] public float speed = 6f;
    [HideInInspector] public Vector3 direction = new Vector3(0f, 0f, -1f);

    [HideInInspector] public float baseSpeed = 6.0f;

    Rigidbody rb;

    public void SetSpeed(float s) => speed = baseSpeed = s;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 inputMovement = new Vector3(moveHorizontal, 0f, 0f);
        Vector3 forwardMovement = direction * speed;
        Vector3 totalMovement = (forwardMovement + inputMovement * speed) * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + totalMovement);
        if (GlobalVariables.bossCounter == 15)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("unfocusShot"))
        {
            Destroy(gameObject);
        }
    }
}
