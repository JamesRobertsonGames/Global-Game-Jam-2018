using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    public AudioSource squeak1;
    public AudioSource squeak2;

    System.Random rnd = new System.Random();

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
                return;
            }

            float gravity = (-2 * FlapHeight) / Mathf.Pow(TimeToApex, 2);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                float flapVelocity = Mathf.Abs(gravity) * TimeToApex;
                velocity.z = flapVelocity * 0.75f;
                velocity.y = flapVelocity;
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
    
    private void OnCollisionEnter(Collision other)
    {
        
        if (rnd.Next(1,2) == 1)
        {
            squeak1.Play();          
        }
        else
        {
            squeak2.Play();
        }
        

        var collider_object = other.gameObject.tag;
        if (collider_object == "Win"){
            SceneManager.LoadScene("GameWin");
        }
        if (collider_object == "Lose")
        {
            SceneManager.LoadScene("GameLose");
        }
    }
}
