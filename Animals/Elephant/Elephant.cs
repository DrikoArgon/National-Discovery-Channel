using UnityEngine;
using System.Collections;

public class Elephant : Animal {

	private Animator animator;
	private Vector3 targetDirection;
	private float irritatedTime = 3f;
	private float elapsedTime;
	private GameManager gameManager;
	private GameObject target; 
	private bool alreadyIrritated;

	// Use this for initialization
	void Start () {
		elapsedTime = 0;
		alreadyIrritated = false;
		animator = GetComponent<Animator>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

		if(irritated){
			elapsedTime += Time.deltaTime;

			if(elapsedTime >= irritatedTime){
				elapsedTime = 0;
				irritated = false;
				alreadyIrritated = true;
			}
		}

		if(irritated){
			GetComponent<Rigidbody2D> ().transform.position += Vector3.Normalize(targetDirection) * speed * Time.deltaTime;
		}

		if(alreadyIrritated && gameManager.elephantActive){
	

			DefineTarget();

			irritatedExpression.gameObject.SetActive(true);
			StartCoroutine(WaitIrritatedExpression());

		}
	}

	public void activateTimerUntilAnnoyed(){
		if(!alreadyIrritated){
			StartCoroutine(TimerUntilAnnoyed());
		}
	}

	public void deactivateActionTimer(){
		StopCoroutine(TimerUntilAnnoyed());
		StopCoroutine(TimerUntilIrritated());
	}

	void DefineTarget(){

			cameraGuy = GameObject.Find("Camera Guy");
			soundGuy = GameObject.Find("Sound Guy");
			micGuy = GameObject.Find("Mic Guy");

			float distance1 = Vector3.Distance(cameraGuy.transform.position,transform.position);
			float distance2 = Vector3.Distance(soundGuy.transform.position,transform.position);
			float distance3 = Vector3.Distance(micGuy.transform.position,transform.position);


			if(distance1 < distance2 && distance1 < distance3){
				target = cameraGuy;
			} 

			if(distance2 < distance1 && distance2 < distance3){
				target = soundGuy;
			} 

			if(distance3 < distance1 && distance3 < distance2){
				target = micGuy;
			}

			if(target.transform.position.x < transform.position.x){
				GetComponent<SpriteRenderer>().flipX = true;
			} else {
				GetComponent<SpriteRenderer>().flipX = false;
			}
			targetDirection = target.transform.position - transform.position;

	}	

	IEnumerator TimerUntilAnnoyed(){
		yield return new WaitForSeconds(annoyedTime);
		calm = false;
		annoyed = true;
		StartCoroutine(TimerUntilIrritated());

		annoyedExpression.gameObject.SetActive(true);

	}

	IEnumerator TimerUntilIrritated(){
		yield return new WaitForSeconds(irritationTime);

		DefineTarget();

		irritatedExpression.gameObject.SetActive(true);
		StartCoroutine(WaitIrritatedExpression());

	}

	IEnumerator WaitIrritatedExpression(){
		yield return new WaitForSeconds(2f);
		animator.SetBool("calm",false);
		animator.SetBool("irritated",true);
		gameManager.inDanger = true;
		annoyed = false;
		irritated = true;
	}


	void OnCollisionEnter2D(Collision2D other){
		if(irritated){
			if(other.gameObject.tag == "CameraGuy" || other.gameObject.tag == "SoundGuy" || other.gameObject.tag == "MicGuy"){
				gameManager.GameOver();
			}
		}
		
	}
}
