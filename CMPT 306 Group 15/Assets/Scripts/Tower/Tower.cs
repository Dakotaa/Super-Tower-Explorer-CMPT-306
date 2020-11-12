using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CellTile {
	public float searchInterval = 1.0f;	// how often the tower will search for enemies
	public float searchRange = 5.0f;   // how far the tower will search for enemies
	public float switchRange = 2.5f; // if the current enemy is further than this, search for a closer enemy
	public float trackingSpeed = 5.0f;
	public float cooldown = 1.0f;	// shot cooldown, seconds
	public float damage = 1.0f; // shot damage
	public float bulletVelocity = 5.0f;
	public Bullet bullet;
	// public GameController controller;
	private GameObject target;  // enemy currently being targeted
	private Vector3 targetLastPos = Vector3.zero; // last known position of current target
	Quaternion targetTrackRot;  // rotation of tower
	public List<GameObject> enemies;	// list of existing enemies

	public virtual void Start() {
		target = null; // no initial target
		enemies = new List<GameObject>();
		StartCoroutine("DoSearch"); // search coroutine starts on creation, loops forever
		StartCoroutine("DoShoot"); // shoot coroutine starts on creation, loops forever
		var bounds = GetComponent<SphereCollider>().bounds; //update the graph
		// Expand the bounds along the Z axis
		bounds.Expand(Vector3.forward * 1000);
		var guo = new GraphUpdateObject(bounds);
		//Debug.Log("update");
		// change some settings on the object
		AstarPath.active.UpdateGraphs(guo);
	}

	// shoot at an enemy
	public virtual void ShootAt(Vector3 targetPos) {
		Vector3 bulletPos = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, 0.0f);
		Bullet shotBullet = Instantiate(bullet, bulletPos, Quaternion.identity);
		Vector3 direction = (targetPos - transform.position).normalized;
		shotBullet.Setup(direction, bulletVelocity);
	}

	// set the current target enemy
	public virtual void SetTarget(GameObject e) {
		target = e;
	}

	// checks for enemies within searchRange
	public virtual void ProximityCheck() {
		// if the tower has a target, check if it's still in range, and shoot if it is
		if (target) {
			if (Vector3.Distance(transform.position, target.transform.position) > switchRange) {
				target = null;
				ProximityCheck();
			}
		} else {
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, searchRange);
			// set new target to closest enemy
			GameObject closest = null;
			float closestDistance = 999f;
			foreach (Collider2D hitCollider in hitColliders) {
				if (hitCollider.tag.Equals("Enemy")) {
					GameObject current = hitCollider.gameObject;
					float distance = Vector3.Distance(transform.position, current.transform.position);
					if (distance < closestDistance) {
						closest = current;
						closestDistance = distance;
					}
				}
			}
			SetTarget(closest);
		}
	}

	// enemy search coroutine
	public virtual IEnumerator DoSearch() { 
		for (;;) {	// loop forever on searchInterval
			ProximityCheck();
			yield return new WaitForSeconds(searchInterval);
		}	
	}

	// tower shoot coroutine
	public virtual IEnumerator DoShoot() {
		for (;;) { // loop forever on cooldown
			if (target) {
				ShootAt(target.transform.position);
			}
			yield return new WaitForSeconds(cooldown);
		}
	}
}
