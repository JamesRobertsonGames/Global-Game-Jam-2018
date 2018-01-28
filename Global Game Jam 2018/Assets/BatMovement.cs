using UnityEngine;

public class BatMovement : MonoBehaviour
{
    public bool DrawGizmos = false;
    public float TimeToApex = 1.5f;
    public float FlapHeight = 5.0f;
    public float CollisionRadius = 1.0f;
    public LayerMask CollisionMask;
    public float JumpDelay = 2.0f;

    private bool isAlive = true;
    private Vector3 velocity;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isAlive)
        {
            Vector3 collision;
            if (BatCollided(out collision))
            {
                // Stick to wall?
                // ...

                // Fall down dead for now
                isAlive = false;
                SoundManager.Instance.StopThemeLoop();
                SoundManager.Instance.PlayRandomSqueak();
                SoundManager.Instance.PlayLose();
                return;
            }

            float gravity = (-2 * FlapHeight) / Mathf.Pow(TimeToApex, 2);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                float flapVelocity = Mathf.Abs(gravity) * TimeToApex;
                velocity.z = flapVelocity * 0.75f;
                velocity.y = flapVelocity;
                anim.SetTrigger("Flap");
                SoundManager.Instance.PlayRandomSqueak();
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }

            transform.Translate(velocity * Time.deltaTime, Space.Self);
        }
    }

    private bool BatCollided(out Vector3 collision)
    {
        var colliders = Physics.OverlapSphere(transform.position, CollisionRadius, CollisionMask);

        if (colliders.Length > 0)
        {
            collision = colliders[0].ClosestPoint(transform.position);
            return true;
        }

        collision = default(Vector3);
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, CollisionRadius);
    }
}
