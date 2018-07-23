using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game {

	public static Game currentGame;
	public int externalCurrency;
	// The list of upgrades contain indices
	public List<int> upgrades;

	public Game () {
		externalCurrency = 0;
		upgrades = new List<int>();
	}
}
