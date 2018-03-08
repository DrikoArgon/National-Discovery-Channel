using UnityEngine;
using System.Collections;

public class PopupScoreController : MonoBehaviour {

	private static PopupScore popupScorePrefab;
	private static GameObject canvas;

	public static void Initialize(){
		canvas = GameObject.Find("Canvas");
		if(!popupScorePrefab){
			popupScorePrefab = Resources.Load<PopupScore>("UI Prefabs/Popup Score Parent");
		}
	}

	public static void CreatePopupScore(string text, Transform location){
		PopupScore instance = Instantiate(popupScorePrefab);

		instance.transform.SetParent(canvas.transform,false);
		instance.SetText(text);
		instance.transform.position = location.position;



	}

}
