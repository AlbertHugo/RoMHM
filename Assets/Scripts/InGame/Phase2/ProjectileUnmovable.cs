using UnityEngine;

public class ProjectileUnmovable : MonoBehaviour
{
    private float speed = 2.0f;
    public Vector3 direction = new Vector3(0f, 0f, -1f);
    private float baseSpeed = 2.0f;

    private Rigidbody rb;
    private bool focus = false;

    // Curvatura
    private bool shouldCurve = false;
    private float curveTimer = 0f;
    private float curveDelay = 1.0f;
    private float curveStrength = 1.0f;
    private int curveDir = 0; // -1 para esquerda, 1 para direita

    public void SetSpeed(float s) => speed = baseSpeed = s;

    public void EnableDelayedCurve(int direction, float delay, float strength)
    {
        curveDelay = delay;
        curveStrength = strength;
        curveDir = direction;
        shouldCurve = true;
        curveTimer = 0f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GlobalVariables.bossCounter == 15&&GlobalVariables.phaseCounter>=6)
        {
            Destroy(gameObject);
        }
        curveTimer += Time.fixedDeltaTime;

        if (shouldCurve && curveTimer >= curveDelay)
        {
            Vector3 curve = new Vector3(curveDir * curveStrength, 0f, 0f);
            direction += curve * Time.fixedDeltaTime;
        }

        Vector3 move = direction.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        RepeatableCode.HandleFocusMode(ref speed, ref focus, baseSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall") || other.CompareTag("Player") || other.CompareTag("unfocusShot"))
        {
            Destroy(gameObject);
        }
    }
}