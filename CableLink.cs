using UnityEngine;
using System.Collections;

public class CableLink : MonoBehaviour {

	public Cable cable;
	public int index;
	public bool isMicJoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "CableCutter"){
			print("bateu no cabo");
			Destroy(gameObject);
			cable.UpdateLineRenderer(index,isMicJoint);
		}
	}
}
