using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	public GameObject hitboxPrefab;

	protected float speed;
	protected float jumpMultiplier;
	protected int t;
	protected int prevJumpStart;               // Used to calculate jump arcs
	protected int maxJumpsRemaining;           // Maximum jumps for the player
	protected int jumpsRemaining;              // Counter to track jumps remaining
	protected bool faceDirection;              // Left is false, right is true
	protected Vector3 attackOffsetHorizontal;  // Offset is for facing right
	protected Vector3 attackOffsetVertical;    // Offset is for facing up

	// Handles player movement
	protected void Move () {
		float direction = Input.GetAxis("Horizontal");
		float x = direction * speed;
		UpdateDirection(direction);
		float y = JumpCalculate();
		rb.velocity = new Vector2(x, y);
	}

	// Handles the direction the player is facing
	protected void UpdateDirection (float direction) {
		if (direction > 0) {
			faceDirection = true;
		} else if (direction < 0) {
			faceDirection = false;
		}
	}

	// A helper function to help calculate jump height
	// TODO: does the jump feel good enough?
	protected float JumpCalculate () {
		float y = rb.velocity.y;
		if (Input.GetButtonDown("Jump") && jumpsRemaining > 0) {
			prevJumpStart = t;
			jumpsRemaining--;
		}
		if (Input.GetButton("Jump")) {
			float jumpNormal = 1.0f - 0.1f * (t - prevJumpStart);
			if (jumpNormal > 0) {
				y = jumpNormal * jumpMultiplier;
			}
		}
		if (Input.GetButtonUp("Jump")) {
			y = 0;
		}
		return y;
	}

	// Current purpose is to reset jumps when contacting the top of a
	// "Ground" object or "Platform" object. If connecting with a platform object
	// from anywhere aside from the top, ignore collisions
	// TODO: is there a better way to construct colToPlayer and colScale?
	public void OnCollisionEnter2D (Collision2D col) {
		Vector2 colToPlayer = gameObject.GetComponent<Renderer>().bounds.center -
			col.gameObject.GetComponent<Renderer>().bounds.center;
		Vector2 colScale = new Vector2(col.gameObject.transform.lossyScale.x,
			col.gameObject.transform.lossyScale.y);
		colToPlayer = new Vector2(colToPlayer.x / colScale.x,
			colToPlayer.y / colScale.y);
		if ((col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform") &&
			colToPlayer.y > Mathf.Abs(colToPlayer.x)) {
			jumpsRemaining = maxJumpsRemaining;
		}
	}
}
