using UnityEngine;

public class Paralax2 : MonoBehaviour
{
    public float speed = 5f;
    private float resetZ = 160f;
    private float startZ = -160f;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z >= resetZ)
        {
            Vector3 pos = transform.position;
            pos.z = startZ;
            transform.position = pos;
        }
    }
}
