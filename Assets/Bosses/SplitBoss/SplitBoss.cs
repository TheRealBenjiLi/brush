using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBoss : Entity {

	public int splitsRemaining;

	private int t;
	private int splitFactor;

	// Use this for initialization
	void Start () {
		health = 1;
		damage = 1;
		this.rb = GetComponent<Rigidbody2D>();
		t = 0;
		splitFactor = 2;
	}

	// Update is called once per frame
	void Update () {
		IsKilled();
	}

	// Overriding default IsKilled function to include splitting
	private void IsKilled () {
		if (health <= 0) {
			if (splitsRemaining > 0) {
				for (int i = 0; i < splitFactor; i++) {
					GameObject child = Instantiate(gameObject, rb.transform.position, rb.transform.rotation);
					child.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 1);
					child.GetComponent<SplitBoss>().splitsRemaining = splitsRemaining - 1;
				}
			}
			Destroy(gameObject);
		}
	}
}
