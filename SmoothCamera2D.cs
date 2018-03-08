
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (target)
		{
//			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(new Vector3(target.position.x,target.position.y,target.position.z));
//			Vector3 delta = new Vector3(target.position.x,target.position.y,target.position.z) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
//			Vector3 destination = new Vector3(transform.position.x,target.position.y,transform.position.z) + delta;
//			transform.position = Vector3.SmoothDamp( new Vector3(transform.position.x,target.position.y,transform.position.z), destination, ref velocity, dampTime);
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position -  GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); 
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

		}

	}
}