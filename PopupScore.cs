using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupScore : MonoBehaviour {

	public Animator animator;
	private Text scoreText;

	void Awake(){
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		Destroy(this.gameObject,clipInfo[0].clip.length);
		scoreText = animator.gameObject.GetComponent<Text>(); 
	}

	public void SetText(string text){
		scoreText.text = text;
	}
}
