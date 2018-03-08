using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TotalScoreText : MonoBehaviour {

	private Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		text.text = PlayerPrefs.GetFloat("Score").ToString();
	}
	

}
