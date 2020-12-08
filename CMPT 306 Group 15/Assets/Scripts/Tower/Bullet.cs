using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Vector3 direction;
	public GameObject impactParticle;
	public float lifetime = 2.0f;
	private Vector3 impactNormal;
	private float damage;
	public List<string> obstructions; // tags that will block and destroy the bullet
	public void Setup(Vector3 direction, float velocity, float damage) {
		direction.z = 0.0f;
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * velocity, direction.y * velocity); // add force to the bullet
		this.damage = damage;
		Destroy(gameObject, this.lifetime);	// destroy bullets if they don't hit anything in 2 seconds
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		string tag = collision.collider.tag;
		if (obstructions.Contains(tag)) {
			impactParticle = Instantiate(impactParticle, new Vector3(transform.position.x, transform.position.y, -0.3f), Quaternion.LookRotation(collision.contacts[0].normal)) as GameObject;
			Destroy(impactParticle, 1);
			Destroy(gameObject);
		}
		if (collision.collider.tag.Equals("Enemy")) {   // damage/kill enemies
			Enemy victim = collision.collider.gameObject.GetComponent<Enemy>();

			victim.Hurt(this.damage); //change this when ready

			if (victim.GetHealth() <= 0)
            {
				if (!victim.IsDead()) {
					victim.Kill(true);
					Destroy(gameObject);
				}
            }
            else
            {
				Destroy(gameObject);
            }	
		}
	}


}
