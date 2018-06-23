using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBoss : Entity {

	public int splitsRemaining;

	private int t;
	private int lastLanded;
	private bool grounded;
	private int splitFactor;

	// Use this for initialization
	void Start () {
		health = 1;
		damage = 1;
		this.rb = GetComponent<Rigidbody2D>();
		t = 0;
		lastLanded = t;
		grounded = false;
		splitFactor = 2;
	}

	// Update is called once per frame
	void Update () {
		Move(t);
		IsKilled();
		t++;
	}

	private void Move (int t) {
		if (t > lastLanded + 200 && grounded) {
			Vector2 velo = 500f * Vector2.up;
			if (Random.value > 0.5) {
				velo += 100f * Random.value * Vector2.right;
			} else {
				velo += 100f * Random.value * Vector2.left;
			}
			rb.AddForce(velo);
			grounded = false;
		} else if (grounded) {
			rb.velocity = new Vector2(0, 0);
		}
	}

	// Overriding default IsKilled function to include splitting
	private void IsKilled () {
		if (health <= 0) {
			if (splitsRemaining > 0) {
				for (int i = 0; i < splitFactor; i++) {
					// posOffset prevents the children to balance on top of each other
					Vector3 posOffset = new Vector3(Random.value, 0, 0);
					GameObject child = Instantiate(gameObject,
						rb.transform.position + posOffset, rb.transform.rotation);
					child.transform.localScale = new Vector3(transform.localScale.x / 2,
						transform.localScale.y / 2, 1);
					child.GetComponent<SplitBoss>().splitsRemaining = splitsRemaining - 1;
				}
			}
			Destroy(gameObject);
		}
	}

	// Same logic used in Player script to calculate SplitBoss's jump resets
	public void OnCollisionEnter2D (Collision2D col) {
		Vector2 colToBoss = gameObject.GetComponent<Renderer>().bounds.center -
			col.gameObject.GetComponent<Renderer>().bounds.center;
		Vector2 colScale = new Vector2(col.gameObject.transform.lossyScale.x,
			col.gameObject.transform.lossyScale.y);
		colToBoss = new Vector2(colToBoss.x / colScale.x, colToBoss.y / colScale.y);
		if (col.gameObject.tag == "Ground" &&
			colToBoss.y > Mathf.Abs(colToBoss.x)) {
			lastLanded = t;
			grounded = true;
		}
	}
}
