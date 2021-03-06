﻿using UnityEngine;
using System.Linq;

public class BatMovement : SingletonMonoBehaviour<BatMovement>
{
    public bool DrawGizmos = false;
    public float TimeToApex = 1.5f;
    public float FlapHeight = 5.0f;
    public float CollisionRadius = 1.0f;
    public LayerMask CollisionMask;
    public float JumpDelay = 2.0f;
    public bool isAlive = true;
    public float BounceBackForce = 5.0f;
    public Bounds CollisionBox;

    public Vector3 velocity;
    private Animator anim;

    public delegate void BatCollisionListener(Vector3 point, GameObject other);
    public event BatCollisionListener OnBatCollided;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isAlive)
        {
            Vector3 collision;
            GameObject other;
            if (BatCollided(out collision, out other))
            {
                if (OnBatCollided != null)
                {
                    OnBatCollided.Invoke(collision, other);
                }
                return;
            }
            else
            {
                float gravity = (-2 * FlapHeight) / Mathf.Pow(TimeToApex, 2);

                if (Input.GetButtonDown("Flap"))
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
            }

            transform.Translate((velocity) * Time.deltaTime, Space.Self);
        }
    }

    private bool BatCollided(out Vector3 collision, out GameObject other)
    {
        var colliders = Physics.OverlapSphere(transform.position, CollisionRadius, CollisionMask);

        if (colliders.Length > 0)
        {
            collision = colliders[0].ClosestPoint(transform.position);
            other = colliders[0].gameObject;
            return true;
        }

        collision = default(Vector3);
        other = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, CollisionRadius);
    }
}
