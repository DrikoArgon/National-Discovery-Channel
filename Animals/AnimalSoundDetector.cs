using UnityEngine;
using System.Collections;

public class AnimalSoundDetector : MonoBehaviour {

	private GameManager gameManager;
	public Animal animal;

	void Start(){
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "SoundGuy"){
			if(animal == Animal.Elephant){

			} else if (animal == Animal.Zebra){

			} else if(animal == Animal.Cheetah){

			}
			gameManager.soundGuyNear = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "SoundGuy"){
			if(animal == Animal.Elephant){

			} else if (animal == Animal.Zebra){

			} else if(animal == Animal.Cheetah){

			}
			gameManager.soundGuyNear= false;
		}
	}

	public enum Animal{
		Elephant,
		Zebra,
		Cheetah
	}
}
