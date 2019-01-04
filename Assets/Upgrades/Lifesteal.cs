using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifesteal : Upgrade {

	public int id;
	public int characterid;
	public string name;
	public string description;

	public Lifesteal () {
		id = 0;
		characterid = 1;
		name = "Lifesteal";
		description = "Dealing damage heals a portion of your health.";
	}

	public void ApplyUpgrade (Player player) {
		SwordGuyScript p = (SwordGuyScript) player;
		p.lifesteal = true;
	}
}
