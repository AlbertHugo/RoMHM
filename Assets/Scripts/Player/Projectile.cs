using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20.0f;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall1" || collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}

