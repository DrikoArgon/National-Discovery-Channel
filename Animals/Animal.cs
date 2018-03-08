using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour {

	public float speed;
	public bool dangerous;
	public float irritationTime;
	public float annoyedTime;

	public bool calm;
	public bool annoyed;
	public bool irritated;

	public GameObject annoyedExpression;
	public GameObject irritatedExpression;

	protected GameObject cameraGuy;
	protected GameObject micGuy;
	protected GameObject soundGuy;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
