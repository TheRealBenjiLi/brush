using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

	public static Game savedGame;

	public static void Save () {
		savedGame = Game.currentGame;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Path.Combine(Application.persistentDataPath, "saveFile.br"));
		bf.Serialize(file, SaveLoad.savedGame);
		file.Close();
	}

	public static void Load () {
		if (File.Exists(Path.Combine(Application.persistentDataPath, "saveFile.br"))) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "saveFile.br"), FileMode.Open);
			SaveLoad.savedGame = (Game)bf.Deserialize(file);
			file.Close();
		}
	}

}
