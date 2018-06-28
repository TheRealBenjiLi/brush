using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGuyScript : Player {

	// Use this for initialization
	public void Start () {
		health = 1;
		damage = 1;
		this.rb = GetComponent<Rigidbody2D>();

		speed = 7.0f;
		jumpMultiplier = 20.0f;
		t = 0;
		prevJumpStart = t;
		maxJumpsRemaining = 1;
		jumpsRemaining = maxJumpsRemaining;
		faceDirection = true;
		attackOffsetHorizontal = Vector3.right * 1.25f;
		attackOffsetVertical = Vector3.up * 1.25f;

		isInAir = true;
		isMovingHorizontal = false;
		isAttacking = false;
		lastMoving = t;
		defaultHeadPosition = head.transform.localPosition;
	}

	// Update is called once per frame
	public void Update () {
		Move();
		Attack();
		Animate();
		IsKilled();
		t++;
	}

	// Attack hitbox appears in front of player direction when Fire1 is pushed.
	// If player aims up or down beforehand, the appropriate hitbox will appear.
	protected void Attack () {
		if (Input.GetButtonDown("Fire1")) {
			if (Input.GetAxis("Vertical") < 0.0f) {
				GameObject hitbox = Instantiate(hitboxPrefab, transform.position -
					attackOffsetVertical, transform.rotation);
				Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(),
					GetComponent<Collider2D>());
			} else if (Input.GetAxis("Vertical") > 0.0f) {
				GameObject hitbox = Instantiate(hitboxPrefab, transform.position +
					attackOffsetVertical, transform.rotation);
				Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(),
					GetComponent<Collider2D>());
			} else if (faceDirection) {
				GameObject hitbox = Instantiate(hitboxPrefab, transform.position +
					attackOffsetHorizontal, transform.rotation);
				Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(),
					GetComponent<Collider2D>());
			} else {
				GameObject hitbox = Instantiate(hitboxPrefab, transform.position -
					attackOffsetHorizontal, transform.rotation);
				Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(),
					GetComponent<Collider2D>());
			}
		}
	}
}
