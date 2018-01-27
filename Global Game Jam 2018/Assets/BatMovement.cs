using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public bool DrawGizmos = false;
    public float TimeToApex = 1.0f;
    public float CollisionRadius = 1.0f;

    public bool IsAlive { get; private set; }

    private void Update()
    {
        if (IsAlive)
        {
            Vector3 collision;
            if (BatCollided(out collision))
            {
                // Stick to wall?
                // ...

                // Fall down dead for now
                IsAlive = false;
            }


        }
    }

    private bool BatCollided(out Vector3 collision)
    {
        Physics.OverlapSphere(transform.position, CollisionRadius);

        collision = default(Vector3);
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, CollisionRadius);
    }
}
