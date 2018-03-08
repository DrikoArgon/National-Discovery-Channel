using UnityEngine;
using System.Collections;

public class MicGuy : MonoBehaviour {

	public float normalSpeed;
	public float actionSpeed;
	public float speed;

	//Controls
	public KeyCode moveUpKey;
	public KeyCode moveRightKey;
	public KeyCode moveLeftKey;
	public KeyCode moveDownKey;
	public KeyCode actionKey;

	public string moveHorizontalGamepadAxis;
	public string moveVerticalGamepadAxis;
	public string actionGamepadButton1;
	public string actionGamepadButton2;
	public string actionGamepadButton3;
	public string actionGamepadButton4;

	public GameObject cameraGuy;

	//Player Direction
	public PlayerDirection direction;
	public PlayerDirection inicialDirection;

	//Components
	private Animator animator;

	public bool doingAction;
	public bool tooFar;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		doingAction = false;
		speed = normalSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (actionKey) || Input.GetButtonDown (actionGamepadButton1) || Input.GetButtonDown (actionGamepadButton2) || Input.GetButtonDown (actionGamepadButton3) || Input.GetButtonDown (actionGamepadButton4)){
			DoAction();
		} 

		if(Input.GetKeyUp (actionKey) || Input.GetButtonUp (actionGamepadButton1) || Input.GetButtonUp (actionGamepadButton2) || Input.GetButtonUp (actionGamepadButton3) || Input.GetButtonUp (actionGamepadButton4)){
			CancelAction();
		} 

	}

	void FixedUpdate () {

		float distanceToCameraGuy = Vector3.Distance(cameraGuy.transform.position,transform.position);

		if(distanceToCameraGuy > 3f){
			tooFar = true;
		} else {
			tooFar = false;
		} 

		if (Input.GetKey (moveUpKey) || (Input.GetAxis (moveVerticalGamepadAxis) >= 0.5f)) {
			if(tooFar){
				float distanceAfterMovement = Vector3.Distance(cameraGuy.transform.position, transform.position + Vector3.up * speed * Time.deltaTime);
				if(distanceAfterMovement < distanceToCameraGuy){
					MoveUp();
				}
			} else{
				MoveUp ();
			}
		} else if (Input.GetKey (moveRightKey) || (Input.GetAxis (moveHorizontalGamepadAxis) >= 0.5f)) {
			if(tooFar){
				float distanceAfterMovement = Vector3.Distance(cameraGuy.transform.position, transform.position + Vector3.right * speed * Time.deltaTime);
				if(distanceAfterMovement < distanceToCameraGuy){
					MoveRight ();
				}
			} else{
				MoveRight ();
			}

		} else if (Input.GetKey (moveLeftKey) || (Input.GetAxis (moveHorizontalGamepadAxis) <= -0.5f)) {
			if(tooFar){
				float distanceAfterMovement = Vector3.Distance(cameraGuy.transform.position, transform.position + Vector3.left * speed * Time.deltaTime);
				if(distanceAfterMovement < distanceToCameraGuy){
					MoveLeft ();
				}
			} else{
				MoveLeft ();
			}
		} else if (Input.GetKey (moveDownKey) || (Input.GetAxis (moveVerticalGamepadAxis) <= -0.5f)) {
			if(tooFar){
				float distanceAfterMovement = Vector3.Distance(cameraGuy.transform.position, transform.position + Vector3.down * speed * Time.deltaTime);
				if(distanceAfterMovement < distanceToCameraGuy){
					MoveDown ();
				}
			} else{
				MoveDown ();
			}

		}  else{
			Idle();
		}   



	}

	public void MoveUp(){
		GetComponent<Rigidbody2D> ().transform.position += Vector3.up * speed * Time.deltaTime;
		animator.SetBool ("moving", true);
		animator.SetInteger ("direction", 1);
		direction = PlayerDirection.Up;
	}

	// função moveRight
	//
	// Move o jogador para direita e aciona as animações respectivas
	//   
	public void MoveRight(){
		GetComponent<Rigidbody2D> ().transform.position += Vector3.right * speed * Time.deltaTime;
		animator.SetBool ("moving", true);
		animator.SetInteger ("direction", 2);
		direction = PlayerDirection.Right;
	}

	// função moveDown
	//
	// Move o jogador para baixo e aciona as animações respectivas
	//   
	public void MoveDown(){
		GetComponent<Rigidbody2D> ().transform.position += Vector3.down * speed * Time.deltaTime;
		animator.SetBool ("moving", true);
		animator.SetInteger ("direction", 3);
		direction = PlayerDirection.Down;
	}

	// função moveLeft
	//
	// Move o jogador para esquerda e aciona as animações respectivas
	//   
	public void MoveLeft(){
		GetComponent<Rigidbody2D> ().transform.position += Vector3.left * speed * Time.deltaTime;
		animator.SetBool ("moving", true);
		animator.SetInteger ("direction", 4);
		direction = PlayerDirection.Left;
	}

	void Idle(){
		animator.SetBool ("moving", false);
	}

	void DoAction(){

		doingAction = true;
		animator.SetBool("action",true);
		speed = actionSpeed;
	}

	void CancelAction(){
		doingAction = false;
		animator.SetBool("action",false);
		speed = normalSpeed;
	}

}

