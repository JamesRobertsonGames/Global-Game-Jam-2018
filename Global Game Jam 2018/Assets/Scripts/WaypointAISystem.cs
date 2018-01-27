using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class WaypointAISystem : MonoBehaviour
{
	public List<GameObject> Waypoints;
	[Range(1,50)]
	public float MoveTowardsSpeed;
	[Range(0, 8)]
	public float PauseTime;
	private float StorePauseTime;
	private int CurrentWaypoint;
	private bool BackwordsIteration;
	private bool TimerOn;
	
	// Use this for initialization
	void Start ()
	{
		StorePauseTime = PauseTime;
		BackwordsIteration = false;
		TimerOn = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		MoveToWaypoint();
	}

	void MoveToWaypoint()
	{
		transform.LookAt(Waypoints[CurrentWaypoint].transform.position);
		transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentWaypoint].transform.position,MoveTowardsSpeed * Time.deltaTime);

		if (TimerOn)
		{
			PauseTime -= Time.deltaTime;

			if (PauseTime <= 0.0f)
			{
				PauseTime = StorePauseTime;
				
				if (CurrentWaypoint < Waypoints.Count - 1)
				{
					BackwordsIteration = false;
				}
				else
				{
					BackwordsIteration = true;
				}
				
				if (!BackwordsIteration)
				{
					CurrentWaypoint++;
				}
				else
				{
					CurrentWaypoint--;
				}

				TimerOn = false;
			}
			
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		TimerOn = true;
	}
}
