using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Cable : MonoBehaviour {

	public Rigidbody2D hookMic;
	public Rigidbody2D hookSound;
	public GameObject cableLinkPrefab;
	public FinalCableHook finalMicHook;
	public FinalCableHook finalSoundHook;
	public LineRenderer lineRendererMicCable;
	public LineRenderer lineRendererSoundCable;

	public List<Transform> linkPointsMic;
	public List<Transform> linkPointsSound;

	public int links = 7;

	private bool finishedBuildingCable;

	// Use this for initialization
	void Start () {
		GenerateCable();
	}

	void Update(){
		if(finishedBuildingCable){
			for(int i = 0; i < linkPointsMic.Count; i++){
				lineRendererMicCable.SetPosition(i,linkPointsMic[i].position);
			}

			for(int i = 0; i < linkPointsSound.Count; i++){
				lineRendererSoundCable.SetPosition(i,linkPointsSound[i].position);
			}

		}	
	}

	void GenerateCable(){
	//Create microfone cable
		Rigidbody2D previousRB = hookMic;
		for(int i = 0; i < links; i++){
			GameObject link = (GameObject)Instantiate(cableLinkPrefab,transform);
			HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
			joint.connectedBody = previousRB;
			linkPointsMic.Add(link.transform);

			if(i < links - 1){
				previousRB = link.GetComponent<Rigidbody2D>();
			} else {
				finalMicHook.ConnectCableEnd(link.GetComponent<Rigidbody2D>());
			}

		}
		linkPointsMic.Add(finalMicHook.transform);
		lineRendererMicCable.SetVertexCount(linkPointsMic.Count);

	//Create sound cable
		Rigidbody2D previousRB2 = hookSound;
		for(int i = 0; i < links; i++){
			GameObject link = (GameObject)Instantiate(cableLinkPrefab,transform);
			HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
			joint.connectedBody = previousRB2;
			linkPointsSound.Add(link.transform);

			if(i < links - 1){
				previousRB2 = link.GetComponent<Rigidbody2D>();
			} else {
				finalSoundHook.ConnectCableEnd(link.GetComponent<Rigidbody2D>());
			}

		}
		linkPointsSound.Add(finalSoundHook.transform);
		lineRendererSoundCable.SetVertexCount(linkPointsSound.Count);

		finishedBuildingCable = true;

	}

	public void UpdateLineRenderer(int index, bool isMicJoint){
		if(isMicJoint){
			linkPointsMic.RemoveAt(index);
		} else {
			linkPointsSound.RemoveAt(index);
		}

	}

}
