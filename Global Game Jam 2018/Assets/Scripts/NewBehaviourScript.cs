using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	// private Transform myTransform;
	private Rigidbody myRigidbody;

	// Use this for initialization
	void Start ()
	{
		// myTransform = GetComponent<Transform>();
		myRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		var position = myRigidbody.position;
		Debug.Log(position);
		if (position.y < 0.5)
		{
			myRigidbody.velocity = new Vector3(position.x, 10, position.z);
			// myRigidbody.position = 0;
		}		
	}
}
