using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 30f;
    void Update()
    {
        if (gameObject.transform.position.z >= 10)
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKey(KeyCode.Return)||Input.GetKey(KeyCode.Z)||Input.GetKey(KeyCode.L)||Input.GetKey(KeyCode.X)||Input.GetKey(KeyCode.C))
        {
            scrollSpeed = 7.0f;
        }else
        {
            scrollSpeed=1.0f;
        }
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}

