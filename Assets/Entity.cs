using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	public int health;
	public int damage;
	public Rigidbody2D rb;

	private void isKilled() {
		if (health <= 0) {
			// Remove object
		}
	}
}
