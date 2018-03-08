using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool filming;
	public bool micInFrame;
	public bool micGuyTalking;
	public bool soundGuyNear;
	public bool soundInFrame;
	public bool animalInFrame;
	public bool inDanger;
	public float score;
	public float multiplyer;
	public int pointBase;

	public bool animalIsNear;
	public Text scoreLabel;
	public Text scoreMultiplyerLabel;

	//TV
	public Image terrainImage;
	public Image elephantImage;
	public Image micGuyImage;
	public Image soundGuyImage;
	public Image dangerImage;
	public Image cryingImage;

	//Stickers
	public Image elephantSticker;
	public Image cheetahSticker;
	public Image zebraSticker;

	public Sprite completeElephantSprite;
	public Sprite completeZebraSprite;
	public Sprite completeCheetahSprite;

	//ProgressBar Fillings
	public Image elephantProgressBarFilling;
	public Image cheetahProgressBarFilling;
	public Image zebraProgressBarFilling;

	//Elephant Progress Bar
	public Image progressElephantBarInfo;
	public Image progressElephantBar;
	public float amountOfElephantComplete;

	//Cheetah Progress Bar
	public Image progressCheetahBarInfo;
	public Image progressCheetahBar;
	public float amountOfCheetahComplete;

	//Zebra Progress Bar
	public Image progressZebraBarInfo;
	public Image progressZebraBar;
	public float amountOfZebraComplete;

	//States
	public bool elephantComplete;
	public bool cheetahComplete;
	public bool zebraComplete;

	private float elapsedTime;

	public MicGuy micGuy;
	public SoundGuy soundGuy;

	public bool elephantActive;
	public bool cheetahActive;
	public bool zebraActive;

	public GameObject currentAnimal;

	public SoundManager soundManager;
	public AudioSource presenterAudioSource;

	// Use this for initialization
	void Start () {
		score = 0;
		multiplyer = 0;
		elapsedTime = 0;
		elephantComplete = false;
		cheetahComplete = false;
		zebraComplete = false;
		presenterAudioSource = GameObject.Find("PresenterAudioSource").GetComponent<AudioSource>();
		PopupScoreController.Initialize();
	}
	
	// Update is called once per frame
	void Update () {

		elapsedTime += Time.deltaTime;

		if(animalIsNear){
			if(filming && animalInFrame){

				if(elapsedTime >= 1f){

					elapsedTime = 0f;

					multiplyer = 1f;

					if(micInFrame && micGuy.doingAction){

						multiplyer += 1.5f;
					}

					if(soundGuyNear && soundGuy.doingAction){
						multiplyer += 1;
					}

					if(inDanger){
						multiplyer *= 2f;
					}

					if(soundInFrame){
						multiplyer = 0;
					}

//					if(0 < multiplyer && multiplyer <= 2.5f){
//						soundManager.PlayPointLow();
//					} else if( 2.5f < multiplyer && multiplyer < 7f){
//						soundManager.PlayPointMed();
//					} else if( multiplyer >= 7f){
//						soundManager.PlayPointHigh();
//					}

					if(elephantActive && !elephantComplete){
						amountOfElephantComplete += 0.017f;
						elephantProgressBarFilling.rectTransform.localScale = new Vector3(amountOfElephantComplete,1,1);
						PopupScoreController.CreatePopupScore("+" + (pointBase * multiplyer).ToString(),currentAnimal.transform);
						score += pointBase * multiplyer;
					}

					if(cheetahActive && !cheetahComplete){
						amountOfCheetahComplete += 0.017f;
						cheetahProgressBarFilling.rectTransform.localScale = new Vector3(amountOfCheetahComplete,1,1);
						PopupScoreController.CreatePopupScore("+" + (pointBase * multiplyer).ToString(),currentAnimal.transform);
						score += pointBase * multiplyer;
					}

					if(zebraActive && !zebraComplete){
						amountOfZebraComplete += 0.017f;
						zebraProgressBarFilling.rectTransform.localScale = new Vector3(amountOfZebraComplete,1,1);
						PopupScoreController.CreatePopupScore("+" + (pointBase * multiplyer).ToString(),currentAnimal.transform);
						score += pointBase * multiplyer;
					}

				}

			} else{
				score += 0;
			}

		} else{
			multiplyer = 0;
		}

		if(amountOfElephantComplete >= 1){
			elephantComplete = true;
			elephantSticker.sprite = completeElephantSprite;
			Win();
		}

		if(amountOfCheetahComplete >= 1){
			cheetahComplete = true;
			cheetahSticker.sprite = completeCheetahSprite;
		}

		if(amountOfZebraComplete >= 1){
			zebraComplete = true;
			zebraSticker.sprite = completeZebraSprite;
		}

		if(filming){

			terrainImage.gameObject.SetActive(true);

			if(animalInFrame){
				if(elephantActive){
					elephantImage.gameObject.SetActive(true);
				}
			} else{
				elephantImage.gameObject.SetActive(false);
			}

			if(micInFrame && micGuy.doingAction){

				if(!presenterAudioSource.isPlaying){
					presenterAudioSource.Play();
				}

				micGuyImage.gameObject.SetActive(true);
			} else {
				if(presenterAudioSource.isPlaying){
					presenterAudioSource.Stop();
				}
				micGuyImage.gameObject.SetActive(false);
			}

			if(soundInFrame){
				soundGuyImage.gameObject.SetActive(true);
			} else {
				soundGuyImage.gameObject.SetActive(false);
			}

			if(inDanger){
				dangerImage.gameObject.SetActive(true);
			} else {
				dangerImage.gameObject.SetActive(false);
			}

			if(inDanger && micInFrame && micGuy.doingAction){
				cryingImage.gameObject.SetActive(true);
			} else {
				cryingImage.gameObject.SetActive(false);
			}

		} else{
			terrainImage.gameObject.SetActive(false);
			elephantImage.gameObject.SetActive(false);
			micGuyImage.gameObject.SetActive(false);
			soundGuyImage.gameObject.SetActive(false);
			dangerImage.gameObject.SetActive(false);
			cryingImage.gameObject.SetActive(false);
			presenterAudioSource.Stop();

		}

		scoreMultiplyerLabel.text = multiplyer.ToString();
		scoreLabel.text = score.ToString();
	}

	public void ActivateElephantProgressBar(){
		elephantActive = true;
		elephantProgressBarFilling.gameObject.SetActive(true);
		progressElephantBar.gameObject.SetActive(true);
		progressElephantBarInfo.gameObject.SetActive(true);
	}

	public void DeactivateElephantProgressBar(){
		elephantActive = false;
		elephantProgressBarFilling.gameObject.SetActive(false);
		progressElephantBar.gameObject.SetActive(false);
		progressElephantBarInfo.gameObject.SetActive(false);
	}

//	public void ActivateCheetahProgressBar(){
//			cheetahActive = true;
	//   	cheetahProgressBarFilling.gameObject.SetActive(true);
	//		progressCheetahBar.gameObject.SetActive(true);
	//		progressCheetahBarInfo.gameObject.SetActive(true);
//	}
//
//	public void DeactivateCheetahProgressBar(){
	//		cheetahActive = false;
	//		cheetahProgressBarFilling.gameObject.SetActive(false);
	//		progressCheetahBar.gameObject.SetActive(false);
	//		progressCheetahBarInfo.gameObject.SetActive(false);
//	}

	public void ActivateZebraProgressBar(){
  			zebraActive = true;
			zebraProgressBarFilling.gameObject.SetActive(true);
			progressZebraBar.gameObject.SetActive(true);
			progressZebraBarInfo.gameObject.SetActive(true);
	}

	public void DeactivateZebraProgressBar(){
			zebraActive = false;
			zebraProgressBarFilling.gameObject.SetActive(false);
			progressZebraBar.gameObject.SetActive(false);
			progressZebraBarInfo.gameObject.SetActive(false);
	}

	public void Win(){
		PlayerPrefs.SetFloat("Score",score);
		SceneManager.LoadScene(4);
	}

	public void GameOver(){
		SceneManager.LoadScene(2);
	}
}
