using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	public GameObject hitboxPrefab;

	private float speed;
	private float jumpMultiplier;
	private int t;
	private int prevJumpStart;
	private int maxJumpsRemaining;
	private int jumpsRemaining;
	private bool faceDirection;   // Left is false, right is true
	private Vector3 attackOffset;  // Offset is for facing right

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
		faceDirection = true;
		attackOffset = Vector3.right * 1.25f;
	}

	// Update is called once per frame
	public void Update () {
		Move();
		Attack();
		IsKilled();
		t++;
	}

	// Handles player movement
	private void Move () {
		float direction = Input.GetAxis("Horizontal");
		float x = direction * speed;
		UpdateDirection(direction);
		float y = JumpCalculate();
		rb.velocity = new Vector2(x, y);
	}

	// Attack hitbox appears when Fire1 is pushed
	private void Attack () {
		if (Input.GetButtonDown("Fire1")) {
			if (faceDirection) {
				GameObject hitbox = Instantiate(hitboxPrefab, rb.transform.position + attackOffset, rb.transform.rotation);
				Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			} else {
				GameObject hitbox = Instantiate(hitboxPrefab, rb.transform.position - attackOffset, rb.transform.rotation);
				Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			}
		}
	}

	// Handles the direction the player is facing
	private void UpdateDirection (float direction) {
		if (direction > 0) {
			faceDirection = true;
		} else if (direction < 0) {
			faceDirection = false;
		}
	}

	// A helper function to help calculate jump height
	// TODO: does the jump feel good enough?
	private float JumpCalculate () {
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

	// Current purpose is to reset jumps
	public void OnCollisionStay2D (Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			jumpsRemaining = maxJumpsRemaining;
		}
	}
}
