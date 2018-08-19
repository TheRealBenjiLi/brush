using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadIndex(int index) {
		EditorSceneManager.LoadScene (index);
	}
}
