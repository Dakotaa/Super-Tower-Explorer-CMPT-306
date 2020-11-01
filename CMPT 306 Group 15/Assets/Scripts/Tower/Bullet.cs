using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float velocity = 50.0f;
	private Vector3 direction;	
	public void Setup(Vector3 direction) {
		this.direction = direction;
	}

	private void Update() {
		transform.position += direction * velocity * Time.deltaTime;
	}

	private void OnCollisionEnter(Collision collision) {
		Destroy(gameObject);
	}


}
