using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	private float speed;
	private float jumpMultiplier;
	private int t;
	private int prevJumpStart;
	private int maxJumpsRemaining;
	private int jumpsRemaining;

	// Use this for initialization
	public void Start () {
		health = 1;
		damage = 1;
		this.rb = GetComponent<Rigidbody2D>();

		speed = 7.0f;
		jumpMultiplier = 20.0f;
		t = 0;
		prevJumpStart = t;
		maxJumpsRemaining = 2;
		jumpsRemaining = maxJumpsRemaining;
	}

	// Update is called once per frame
	public void Update () {
		Move();
		IsKilled();
		t++;
	}

	// Handles player movement
	private void Move () {
		float x = Input.GetAxis("Horizontal") * speed;
		float y = JumpCalculate();
		rb.velocity = new Vector2(x, y);
	}

	// A helper function to help calculate jump height
	// TODO: does the jump feel good enough?
	private float JumpCalculate() {
		float y = rb.velocity.y;
		if (Input.GetButtonDown("Jump") && jumpsRemaining > 0) {
			prevJumpStart = t;
		}
		if (Input.GetButton("Jump")) {
			float jumpNormal = 1.0f - 0.1f * (t - prevJumpStart);
			if (jumpNormal > 0) {
				y = jumpNormal * jumpMultiplier;
			}
		}
		if (Input.GetButtonUp("Jump") && jumpsRemaining > 0) {
			y = 0;
			jumpsRemaining--;
		}
		return y;
	}

	// Harmful entities can call this function to deal damage
	public void TakeDamage(int dmg) {
		health -= dmg;
	}

	// Current purpose is to reset jumps
	public void OnCollisionStay2D (Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			jumpsRemaining = maxJumpsRemaining;
		}
	}
}
