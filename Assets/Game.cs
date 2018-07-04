using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game {

	public static Game currentGame;
	public int externalCurrency;

	public Game () {
		externalCurrency = 0;
	}
}
