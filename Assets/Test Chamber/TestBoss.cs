using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : Entity {

	private int t;
	private Vector2 direction;
	private Vector3 offset;
	private int prevBulletTime;

	public GameObject bulletPrefab;

	// Use this for initialization
	public void Start () {
		this.health = 10;
		this.damage = 1;
		this.rb = GetComponent<Rigidbody2D>();
		t = 0;
		prevBulletTime = t;
		direction = Vector2.left * 2f;
		offset = Vector3.down * 1f;
	}

	// Update is called once per frame
	public void Update () {
		Move(t);
		Attack(t, rb);
		IsKilled();
		t++;
	}

	// Movement for the boss
	private void Move (int t) {
		if (t % 300 == 0) {
			rb.velocity = direction;
			direction *= -1;
		}
	}

	// Random-ish attack pattern
	private void Attack (int t, Rigidbody2D rb) {
		if (t - prevBulletTime > 50 && Random.value > 0.95) {
			Instantiate(bulletPrefab, rb.transform.position + offset, rb.transform.rotation);
			prevBulletTime = t;
		}
	}
}
