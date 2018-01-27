using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour {

	public GameObject target;
	Vector3 offset, playerPrevPos, playerMoveDir;
	float distance;

	void Start() {
		offset = transform.position - target.transform.position;
		distance = offset.magnitude;
		playerPrevPos = target.transform.position;
	}

	void LateUpdate() {
		playerMoveDir = target.transform.position - playerPrevPos;
		if (playerMoveDir != Vector3.zero) {
			playerMoveDir.Normalize ();
			transform.position = target.transform.position - playerMoveDir * distance;
			//transform.position.y += 5f;
			transform.LookAt (target.transform.position);
			playerPrevPos = target.transform.position;
		}
	}
}
