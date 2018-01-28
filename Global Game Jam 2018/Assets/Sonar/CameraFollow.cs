using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Bounds CameraBounds;

    private Transform target;
    private Vector3 idealOffset;

	// Use this for initialization
	void Start ()
    {
        target = transform.parent;
        transform.SetParent(null);
        idealOffset = transform.position - target.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 idealPosition = target.position + idealOffset;

        if (CameraBounds.Contains(idealPosition))
        {
            transform.position = idealPosition;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, CameraBounds.ClosestPoint(idealPosition), Time.deltaTime * BatMovement.Instance.velocity.sqrMagnitude);
        }
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(CameraBounds.center, CameraBounds.size);
    }
}
