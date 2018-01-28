using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballFirerer : MonoBehaviour
{
	public GameObject Player;
	public GameObject Mage;
	public GameObject Fireball;
	private GameObject tempFireball;

	private bool TravellingFireball;
	public float FireballTimer;
	private float FireballTimelimit; 
	public float MinDistance;
	public float FireballSpeed;

    public Vector3 Offset;

	private Vector3 shooting_player_position;
	
	// Use this for initialization
	void Start ()
	{
		FireballTimelimit = FireballTimer;
		TravellingFireball = false;
	}

	void FireballTravel(Vector3 local_shooting_player_position)
	{
		tempFireball.transform.position =
			Vector3.MoveTowards(tempFireball.transform.position, shooting_player_position, FireballSpeed * Time.deltaTime);
	}

	void FireFireball()
	{
		tempFireball = GameObject.Instantiate(Fireball, Vector3.zero, Quaternion.identity);
        tempFireball.transform.position = Mage.transform.position;

		// May do some clever leading shot stuff with this soon
		shooting_player_position = Player.transform.position;

		TravellingFireball = true;
	}

	void ShootingFireball()
	{
		FireballTravel(shooting_player_position);

		FireballTimer -= Time.deltaTime;

		if (tempFireball.transform.position == shooting_player_position)
		{
			Destroy(tempFireball);
		}
		
		if (FireballTimer <= 0.0f)
		{
			FireballTimer = FireballTimelimit;
			TravellingFireball = false;
			Destroy(tempFireball);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		float Distance = Vector3.Distance(Mage.transform.position, Player.transform.position);

		if (Distance < MinDistance)
		{
			if (!TravellingFireball)
			{
				if (FireballTimer == FireballTimelimit)
					FireFireball();
			}
		}

		if (TravellingFireball)
		{
			ShootingFireball();
		}
	}
}
