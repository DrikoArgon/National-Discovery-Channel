using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip cameraBeepClip;
	public AudioClip presenterTalking;
	public AudioClip pointSoundLow;
	public AudioClip pointSoundMed;
	public AudioClip pointSoundHigh;

	private AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}


	public void PlayCameraBeep(){
		audioSource.PlayOneShot(cameraBeepClip);
	}

	public void PlayPointLow(){
		audioSource.PlayOneShot(pointSoundLow);
	}

	public void PlayPointMed(){
		audioSource.PlayOneShot(pointSoundMed);
	}

	public void PlayPointHigh(){
		audioSource.PlayOneShot(pointSoundHigh);
	}

	public void StartPresenterTalking(){
		audioSource.Play();
	}

	public void StopPresenterTalking(){
		audioSource.Stop();
	}

	public bool IsPresenterTalking(){
		return audioSource.isPlaying;
	}
}
