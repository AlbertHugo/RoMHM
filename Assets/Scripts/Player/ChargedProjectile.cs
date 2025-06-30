using UnityEngine;

public class ChargedProjectile : MonoBehaviour
{
    private float speed = 10.0f;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall1")
        {
            Destroy(gameObject);
        }
    }
}

