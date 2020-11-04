﻿using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : Implement game controller access for getting list of enemies
// TODO : Replace GameObject with Enemy when enemy type is ready

public class Tower : MonoBehaviour {
	public float searchInterval = 1.0f;	// how often the tower will search for enemies
	public float searchRange = 5.0f;   // how far the tower will search for enemies
	public float trackingSpeed = 5.0f;
	public float cooldown = 1.0f;	// shot cooldown, seconds
	public float damage = 1.0f; // shot damage
	public Bullet bullet;
	// public GameController controller;
	private GameObject target;  // enemy currently being targeted
	private Vector3 targetLastPos = Vector3.zero; // last known position of current target
	Quaternion targetTrackRot;  // rotation of tower
	public List<GameObject> enemies;	// list of existing enemies

	void Start() {
		target = null; // no initial target
		enemies = new List<GameObject>();
		StartCoroutine("DoSearch"); // search coroutine starts on creation, loops forever
		StartCoroutine("DoShoot"); // shoot coroutine starts on creation, loops forever
		var bounds = GetComponent<SphereCollider>().bounds;
		// Expand the bounds along the Z axis
		bounds.Expand(Vector3.forward * 1000);
		var guo = new GraphUpdateObject(bounds);
		//Debug.Log("update");
		// change some settings on the object
		AstarPath.active.UpdateGraphs(guo);



	}

	// shoot at an enemy
	private void ShootAt(Vector3 targetPos) {
		Vector3 bulletPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
		Bullet shotBullet = Instantiate(bullet, bulletPos, Quaternion.identity);
		Vector3 direction = (targetPos - transform.position);
		shotBullet.Setup(direction);
	}

	// set the current target enemy
	private void SetTarget(GameObject e) {
		target = e;
	}

	// checks for enemies within searchRange
	private void ProximityCheck() {
		// if the tower has a target, check if it's still in range, and shoot if it is
		if (target) {
			if (Vector3.Distance(transform.position, target.transform.position) > searchRange) {
				target = null;
				ProximityCheck();
			}
		} else {
			Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, searchRange);
			// set target to first enemy in range
			foreach (Collider hitCollider in hitColliders) {
				if (hitCollider.tag.Equals("Enemy")) {
					print("NEW TARGET: " + hitCollider.gameObject.name);
					SetTarget(hitCollider.gameObject);
					break;
				}
			}
		}
	}

	// enemy search coroutine
	private IEnumerator DoSearch() { 
		for (;;) {	// loop forever on searchInterval
			ProximityCheck();
			yield return new WaitForSeconds(searchInterval);
		}	
	}

	// tower shoot coroutine
	private IEnumerator DoShoot() {
		for (;;) { // loop forever on cooldown
			if (target) {
				ShootAt(target.transform.position);
			}
			yield return new WaitForSeconds(cooldown);
		}
	}
}