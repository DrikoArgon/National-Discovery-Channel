using UnityEngine;
using System.Collections;

public class FinalCableHook : MonoBehaviour {

	public void ConnectCableEnd(Rigidbody2D endCableRB){
		HingeJoint2D joint = GetComponent<HingeJoint2D>();
		joint.connectedBody = endCableRB;
		joint.anchor = Vector2.zero;
		joint.connectedAnchor =  Vector2.zero;
	}

}
