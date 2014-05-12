using UnityEngine;
using System.Collections;

public class ChangeSceneScript : MonoBehaviour {
	public string levelToLoad;
	// Use this for initialization
	void Start () {
		Debug.Log("loading level");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("space")) {
			Application.LoadLevel(levelToLoad);
		}
	}
}
