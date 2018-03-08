using UnityEngine;
using System.Collections;

public class Zebra : Animal {

	private Animator animator;
	[SerializeField]
	private DirectionToFlee directionToFlee;
	private float frightenedTime = 3f;
	private bool justFled;
	private float elapsedTime;
	private GameManager gameManager;

	public bool continueBehaviour;

	// Use this for initialization
	void Start () {
		elapsedTime = 0;
		justFled = false;
		continueBehaviour = false;
		animator = GetComponent<Animator>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		directionToFlee = DirectionToFlee.None;
	}

	// Update is called once per frame
	void Update () {

		if(irritated){
			elapsedTime += Time.deltaTime;

			if(elapsedTime >= frightenedTime){
				elapsedTime = 0;
				irritated = false;
				justFled = true;
			}
		}

	

		if(justFled && gameManager.zebraActive){
			justFled = false;
			continueBehaviour = true;
			DefineDirectionToFlee();
			Startled();
			irritatedExpression.gameObject.SetActive(true);
			StartCoroutine(WaitIrritatedExpression());

		}else if(justFled && !gameManager.zebraActive){
			justFled = false;
			continueBehaviour = false;
			HeadDown();
			calm = true;
			annoyed = false;
			irritated = false;
		}
	}

	void FixedUpdate(){
		if(irritated){
			if(directionToFlee == DirectionToFlee.Left){
				GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
			} else if(directionToFlee == DirectionToFlee.Right){
				GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
			} else if(directionToFlee == DirectionToFlee.Down){
				GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
			} else if(directionToFlee == DirectionToFlee.Up){
				GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
			}
		}
	}

	IEnumerator TimerUntilAnnoyed(){
		yield return new WaitForSeconds(annoyedTime);
		HeadUp();
		calm = false;
		annoyed = true;
		StartCoroutine(TimerUntilIrritated());

		annoyedExpression.gameObject.SetActive(true);

	}

	void DefineDirectionToFlee(){
		cameraGuy = GameObject.Find("Camera Guy");
		soundGuy = GameObject.Find("Sound Guy");
		micGuy = GameObject.Find("Mic Guy");

		float distance1 = Vector3.Distance(cameraGuy.transform.position,transform.position);
		float distance2 = Vector3.Distance(soundGuy.transform.position,transform.position);
		float distance3 = Vector3.Distance(micGuy.transform.position,transform.position);

		float distanceAfterMovement1Up = Vector3.Distance(cameraGuy.transform.position,transform.position + Vector3.up * speed * Time.deltaTime);
		float distanceAfterMovement2Up = Vector3.Distance(soundGuy.transform.position,transform.position + Vector3.up * speed * Time.deltaTime);
		float distanceAfterMovement3Up = Vector3.Distance(micGuy.transform.position,transform.position + Vector3.up * speed * Time.deltaTime);

		float distanceAfterMovement1Right = Vector3.Distance(cameraGuy.transform.position,transform.position + Vector3.right * speed * Time.deltaTime);
		float distanceAfterMovement2Right = Vector3.Distance(soundGuy.transform.position,transform.position + Vector3.right * speed * Time.deltaTime);
		float distanceAfterMovement3Right = Vector3.Distance(micGuy.transform.position,transform.position + Vector3.right * speed * Time.deltaTime);

		float distanceAfterMovement1Left = Vector3.Distance(cameraGuy.transform.position,transform.position + Vector3.left * speed * Time.deltaTime);
		float distanceAfterMovement2Left = Vector3.Distance(soundGuy.transform.position,transform.position + Vector3.left * speed * Time.deltaTime);
		float distanceAfterMovement3Left = Vector3.Distance(micGuy.transform.position,transform.position + Vector3.left * speed * Time.deltaTime);

		float distanceAfterMovement1Down = Vector3.Distance(cameraGuy.transform.position,transform.position + Vector3.down * speed * Time.deltaTime);
		float distanceAfterMovement2Down = Vector3.Distance(soundGuy.transform.position,transform.position + Vector3.down * speed * Time.deltaTime);
		float distanceAfterMovement3Down = Vector3.Distance(micGuy.transform.position,transform.position + Vector3.down * speed * Time.deltaTime);

		if(distanceAfterMovement1Up > distance1 && distanceAfterMovement2Up > distance2 && distanceAfterMovement3Up > distance3 ){ //Can go up
			directionToFlee = DirectionToFlee.Up;

		} else if(distanceAfterMovement1Right > distance1 && distanceAfterMovement2Right > distance2 && distanceAfterMovement3Right > distance3 ){
			directionToFlee = DirectionToFlee.Right;

		} else if(distanceAfterMovement1Left > distance1 && distanceAfterMovement2Left > distance2 && distanceAfterMovement3Left > distance3 ){
			directionToFlee = DirectionToFlee.Left;
		} else if(distanceAfterMovement1Down > distance1 && distanceAfterMovement2Down > distance2 && distanceAfterMovement3Down > distance3 ){
			directionToFlee = DirectionToFlee.Down;
		} else {
			int random = Random.Range(0,3);

			if(random == 0){
				directionToFlee = DirectionToFlee.Up;
			} else if(random == 1){
				directionToFlee = DirectionToFlee.Right;
			}  else if(random == 2){
				directionToFlee = DirectionToFlee.Left;
			}  else {
				directionToFlee = DirectionToFlee.Down;
			} 
		} 

		if(directionToFlee == DirectionToFlee.Left){
			GetComponent<SpriteRenderer>().flipX = true;
		} else if(directionToFlee == DirectionToFlee.Right){
			GetComponent<SpriteRenderer>().flipX = false;
		}
	}

	public void activateTimerUntilAnnoyed(){
		continueBehaviour = true;
		StartCoroutine(TimerUntilAnnoyed());
	}

	public void deactivateActionTimer(){
		continueBehaviour = false;
		StopCoroutine(TimerUntilAnnoyed());
		StopCoroutine(WaitIrritatedExpression());
	}

	IEnumerator TimerUntilIrritated(){
		if(continueBehaviour){
			yield return new WaitForSeconds(irritationTime);

			DefineDirectionToFlee();

			irritatedExpression.gameObject.SetActive(true);
			StartCoroutine(WaitIrritatedExpression());

		}
	}

	IEnumerator WaitIrritatedExpression(){
		yield return new WaitForSeconds(2f);
		Running();
		annoyed = false;
		irritated = true;
	}

	void HeadDown(){
		animator.SetBool("irritated",false);
		animator.SetBool("headDown",true);
	}

	void Idle(){
		animator.SetBool("headDown",false);
		animator.SetBool("idle",true);
	}

	void HeadUp(){
		animator.SetBool("headUp",true);
		animator.SetBool("idle",false);
	}

	void Annoyed(){
		animator.SetBool("annoyed",true);
		animator.SetBool("headUp",false);
	}

	void Startled(){
		animator.SetBool("irritated",false);
		animator.SetBool("startled",true);
		animator.SetBool("annoyed",false);
	}

	void Running(){
		animator.SetBool("irritated",true);
		animator.SetBool("startled",false);
	}

	void OnCollisionEnter2D(Collision2D other){

		if(irritated){
			if(other.gameObject.tag == "CameraGuy" || other.gameObject.tag == "SoundGuy" || other.gameObject.tag == "MicGuy"){
				gameManager.GameOver();
			}
		}

		if(other.gameObject.tag == "Scenario"){

				if(directionToFlee == DirectionToFlee.Up){
			
					directionToFlee = DirectionToFlee.Down;
					

				} else if(directionToFlee == DirectionToFlee.Down){
			
					directionToFlee = DirectionToFlee.Up;
					

				} else if(directionToFlee == DirectionToFlee.Left){
				
					directionToFlee = DirectionToFlee.Right;
			

				} else if(directionToFlee == DirectionToFlee.Right){

					directionToFlee = DirectionToFlee.Left;
					
				}

			}
		
	}



	private enum DirectionToFlee{
		Up,
		Right,
		Left,
		Down,
		None
	}
}
