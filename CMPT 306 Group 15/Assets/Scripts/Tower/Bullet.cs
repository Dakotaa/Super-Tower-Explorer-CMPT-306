using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float velocity = 5.0f;
	private Vector3 direction;	
	public void Setup(Vector3 direction) {
		this.direction = direction;
		Destroy(gameObject, 2);	// destroy bullets if they don't hit anything in 2 seconds
	}

	private void Update() {
		transform.position += direction * velocity * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag.Equals("Tower")) { // don't destroy when hitting a tower. Will be improved.
			return;
		}
		if (collision.collider.tag.Equals("Enemy")) {	// damage/kill enemies
			Enemy victim = collision.collider.gameObject.GetComponent<Enemy>();
			victim.Kill();
		}
	}


}
