using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	protected int health;
	protected int damage;
	protected Rigidbody2D rb;

	// Deletes the GameObject if its health is too low
	protected void IsKilled() {
		if (health <= 0) {
			Destroy(gameObject);
		}
	}
}
