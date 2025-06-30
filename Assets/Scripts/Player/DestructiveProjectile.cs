using UnityEngine;

public class DestructiveProjectile : MonoBehaviour
{
    public float speed = 15.0f;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.name == "Wall1" || collision.gameObject.layer == LayerMask.NameToLayer("enemy"))
    {
        Destroy(gameObject);
    }
}
    void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("enemy")||collision.gameObject.layer == LayerMask.NameToLayer("kamikaze"))
        {
            Destroy(gameObject);
        }
    }
}

