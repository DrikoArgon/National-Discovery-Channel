using UnityEngine;
using System.Collections;

public class ElephantActivationArea : MonoBehaviour {


	private GameManager gameManager;
	public Elephant elephant;

	void Start(){
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "CameraGuy"){
			elephant.activateTimerUntilAnnoyed();
			gameManager.ActivateElephantProgressBar();
			gameManager.animalIsNear = true;
			gameManager.currentAnimal = elephant.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "CameraGuy"){
			elephant.deactivateActionTimer();
			gameManager.DeactivateElephantProgressBar();
			gameManager.animalIsNear = false;
			gameManager.currentAnimal = null;
		}
	}

}
