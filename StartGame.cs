using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public string startGamepadButton1;
	public string startGamepadButton2;
	public string startGamepadButton3;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(startGamepadButton1) || Input.GetButtonDown(startGamepadButton2) || Input.GetButtonDown(startGamepadButton3) || Input.GetKeyDown(KeyCode.Return)){
			SceneManager.LoadScene(1);
		}
	}
}
