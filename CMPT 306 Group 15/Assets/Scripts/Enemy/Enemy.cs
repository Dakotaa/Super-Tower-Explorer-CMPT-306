using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Enemy : MainController {
	private GameControl gameControl;
	private WaveControl waveControl;
	private bool dead;
	public float EXPWorth = 1.0f;
	public int damage = -5;
	public float health = 1;
	private float maxHealth;
	bool broken;

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
		this.dead = true;
		gameControl.EnemyKilled(this, rewardEXP);
		Destroy(gameObject);
	}

	public Boolean IsDead() {
		return this.dead;
	}

	public void Hurt(float dam)
    {
		health = health - dam;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    void OnTriggerStay2D(Collider2D col)
	{
		CellGrid emptyTile = col.gameObject.GetComponentInParent<CellGrid>();
		

		if (col.CompareTag("Wall Tile") || col.CompareTag("Wood Wall Tile") || col.CompareTag("Metal Wall Tile") || col.CompareTag("Tower")) //add more tags here
		{
			
			
			if (gameObject.GetComponent<AIPath>().remainingDistance > 1.1  || gameObject.GetComponent<AIPath>().reachedEndOfPath)
			{
				var bounds = col.bounds;

				emptyTile.GetPosAtCoord(col.gameObject.transform.position);
				int x = emptyTile.GetPosAtCoord(col.gameObject.transform.position)[0];
				int y = emptyTile.GetPosAtCoord(col.gameObject.transform.position)[1];

				//add more couroutines for different tags (maybe if statements?)
				if (col.CompareTag("Wall Tile")) {
					StartCoroutine(StoneWall());
				} else if (col.CompareTag("Wood Wall Tile")) {
					StartCoroutine(WoodWall());
				} else if (col.CompareTag("Metal Wall Tile")) {
					StartCoroutine(MetalWall());
				}
				

                if (broken)
                {
					//Debug.Log("Breaking");
					Destroy(col.gameObject);

					emptyTile.CreateTile(x, y, emptyTile.emptyTile);


					//var bounds = GetComponent<CircleCollider2D>().bounds;

					//Expand the bounds along the Z axis
					bounds.Expand(Vector3.forward * 2000);
					//bounds.Expand(0.6f);

					var guo = new GraphUpdateObject(bounds);
					AstarPath.active.UpdateGraphs(guo);

				}

			}
		}
		

    }

	IEnumerator WoodWall() {
		yield return new WaitForSeconds(2);
		broken = true;
	}

	IEnumerator StoneWall()
	{
		yield return new WaitForSeconds(3);
		broken = true;
	}

	IEnumerator MetalWall() {
		yield return new WaitForSeconds(5);
		broken = true;
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
