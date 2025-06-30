using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private Vector3 initialForwardDirection;

    private MovableEnemy movableEnemy;
    bool focus = false;
    float baseSpeed;

    private void Start()
    {
        movableEnemy = GetComponentInParent<MovableEnemy>();
        rb = GetComponent<Rigidbody>();
        baseSpeed = 6.0f;

        initialForwardDirection = transform.forward;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        Vector3 inputMovement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        Vector3 forwardMovement = initialForwardDirection * speed * Time.fixedDeltaTime;
        Vector3 movement = forwardMovement + inputMovement * movableEnemy.bulletSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);
        RepeatableCode.HandleFocusMode(ref speed, ref focus, baseSpeed);
        if (GlobalVariables.bossCounter == 15)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall") ||
            other.gameObject.CompareTag("Player") || 
            other.gameObject.CompareTag("unfocusShot"))
        {
            Destroy(gameObject);
        }
    }
}
