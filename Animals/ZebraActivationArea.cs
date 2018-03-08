using UnityEngine;
using System.Collections;

public class ZebraActivationArea : MonoBehaviour {

	private GameManager gameManager;
	public Zebra zebra;

	void Start(){
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "CameraGuy"){
			zebra.activateTimerUntilAnnoyed();
			gameManager.ActivateZebraProgressBar();
			gameManager.animalIsNear = true;
			gameManager.currentAnimal = zebra.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "CameraGuy"){
			zebra.deactivateActionTimer();
			gameManager.DeactivateZebraProgressBar();
			gameManager.animalIsNear = false;
			gameManager.currentAnimal = null;
		}
	}

}
