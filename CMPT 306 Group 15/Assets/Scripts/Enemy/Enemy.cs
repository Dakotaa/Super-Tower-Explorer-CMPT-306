using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MainController {
	private GameControl gameControl;
	private WaveControl waveControl;
	private bool isDead;
	public float EXPWorth = 1.0f;
	public int damage = -5;
	public float health = 1;
	private float maxHealth;
	

	private void Start() {
		maxHealth = health;
		gameControl = GameControl.instance;
		waveControl = WaveControl.instance;
	}

	void Update() {

		ReachedDestination(); //check if destination has been reached 
    }

	public float GetEXPWorth() {
		return this.EXPWorth;
	}

	public float GetHealth()
    {
		//Debug.Log("My Health is: " + health);
		return health;
    }

	public float GetMaxHealth()
    {
		return maxHealth;
    }

	public void Kill(bool rewardEXP) {
		this.isDead = true;
		gameControl.EnemyKilled(this, rewardEXP);
		Destroy(gameObject);
	}

	public void Hurt(float dam)
    {
		health = health - dam;
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		CellGrid emptyTile = col.gameObject.GetComponentInParent<CellGrid>();
		

		if (col.CompareTag("Wall Tile") || col.CompareTag("Tower"))
		{
			Debug.Log("GameObject1 collided with " + col.name);
			Debug.Log(gameObject.GetComponent<AIPath>().remainingDistance);
			if (gameObject.GetComponent<AIPath>().remainingDistance > 1.1  || gameObject.GetComponent<AIPath>().reachedEndOfPath)
			{
				
				Debug.Log("I am Stopped");
				emptyTile.GetPosAtCoord(col.gameObject.transform.position);
				int x = emptyTile.GetPosAtCoord(col.gameObject.transform.position)[0];
				int y = emptyTile.GetPosAtCoord(col.gameObject.transform.position)[1];

				Destroy(col.gameObject);

				emptyTile.CreateTile(x,y ,emptyTile.emptyTile);
				

				var bounds = GetComponent<CircleCollider2D>().bounds;

				//Expand the bounds along the Z axis
				bounds.Expand(Vector3.forward * 2000);
				bounds.Expand(1f);
				
				var guo = new GraphUpdateObject(bounds);
				//guo.addPenalty = 15000;
				//Debug.Log("update");
				//Change some settings on the object
				AstarPath.active.UpdateGraphs(guo);
			}
		}
		

    }

	public void ReachedDestination()
    {
		if (gameObject.GetComponent<AIPath>().reachedDestination)
		{
			gameControl.ChangeHealth(damage);

			Kill(false);
		}
	}

	

}
