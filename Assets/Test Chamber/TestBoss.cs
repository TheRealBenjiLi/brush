using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : Entity {

	public int t;
	public Vector2 direction;
	public Vector3 offset;
	private int prevBulletTime;

	public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		health = 10;
		damage = 1;
		rb = GetComponent<Rigidbody2D>();
		t = 0;
		prevBulletTime = t;
		direction = Vector2.left * 2f;
		offset = Vector3.down * 0.5f;
	}

	// Update is called once per frame
	void Update () {
		Move(t);
		Attack(t, rb);
		t++;
	}

	void Move (int t) {
		if (t % 300 == 0) {
			rb.velocity = direction;
			direction *= -1;
		}
	}

	void Attack (int t, Rigidbody2D rb) {
		if (t - prevBulletTime > 50 && Random.value > 0.95) {
			var bullet = (GameObject) Instantiate(bulletPrefab, rb.transform.position + offset, rb.transform.rotation);
			bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.forward * 6;
			Destroy(bullet, 5.0f);
			prevBulletTime = t;
		}
	}
}
