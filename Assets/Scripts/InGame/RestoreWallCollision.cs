using UnityEngine;

public class RestoreWallCollision : MonoBehaviour
{
    private Collider enemyCollider;
    private Collider[] wallColliders;
    private float delay;

    public void Init(Collider enemy, Collider[] walls, float time)
    {
        enemyCollider = enemy;
        wallColliders = walls;
        delay = time;
        Invoke(nameof(Restore), delay);
    }

    void Restore()
    {
        if (enemyCollider != null && wallColliders != null)
        {
            foreach (var wallCollider in wallColliders)
            {
                if (wallCollider != null)
                {
                    Physics.IgnoreCollision(enemyCollider, wallCollider, false);
                }
            }
        }
        Destroy(this);
    }
}
