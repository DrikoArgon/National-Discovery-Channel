using UnityEngine;
using System.Collections;

public class FilmingCone : MonoBehaviour {


	public GameManager gameManager;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnTriggerEnter2D(Collider2D other){

		
		if(other.gameObject.tag == "MicGuy"){
			MicGuy micGuy = other.GetComponent<MicGuy>();
			gameManager.micInFrame = true;
		}

		if(other.gameObject.tag == "SoundGuy"){
			gameManager.soundInFrame = true;
		}

		if(other.gameObject.tag == "Animal"){
			gameManager.animalInFrame = true;
			Animal animal = other.GetComponent<Animal>();
			if(animal.dangerous){
				gameManager.inDanger = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "MicGuy"){
			gameManager.micInFrame = false;
		}

		if(other.gameObject.tag == "SoundGuy"){
			gameManager.soundInFrame = false;
		}

		if(other.gameObject.tag == "Animal"){
			gameManager.animalInFrame = false;
			gameManager.inDanger = false;
		}
	}
}
