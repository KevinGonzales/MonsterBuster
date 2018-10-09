using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : GoogleMobileAdsDemoScript 
{
	public GameObject PauseMenuPannel;
	public GameObject ResumeButton;
	public GameObject RestartButton;
	public GameObject CheracterSelect;
	public GameObject LoadingImage;
	public GameObject Spinner;
	public GameObject HowToPlay;
	public GameObject ExitButton;

	GameObject PauseMenuPannelInstantance;
	GameObject ResumeButtonInstance;
	GameObject RestartButtonInstance;
	GameObject MainMenuInstannce;
	GameObject howToPlay;
	GameObject exitButton;

	static bool ButtonCLickedForHowToPlay;
	
	public void Pauses()
	{
		PlayCLickingNoise ();
			Time.timeScale = 0;
			PauseMenuPannelInstantance = (GameObject)Instantiate (PauseMenuPannel, PauseMenuPannel.transform.position, Quaternion.identity);	
			PauseMenuPannelInstantance.transform.SetParent (GameObject.Find ("Canvas").transform, false);

			ResumeButtonInstance = (GameObject)Instantiate (ResumeButton, ResumeButton.transform.position, Quaternion.identity);	
			ResumeButtonInstance.transform.SetParent (GameObject.Find ("Canvas").transform, false);

			RestartButtonInstance = (GameObject)Instantiate (RestartButton, RestartButton.transform.position, Quaternion.identity);	
			RestartButtonInstance.transform.SetParent (GameObject.Find ("Canvas").transform, false);

			MainMenuInstannce = (GameObject)Instantiate (CheracterSelect, CheracterSelect.transform.position, Quaternion.identity);	
			MainMenuInstannce.transform.SetParent (GameObject.Find ("Canvas").transform, false);

			GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> ().Pause ();
			if (PlayerMovement.StartedGame) {
				GameObject.Find ("MusicForWhenGameStarts").GetComponent<AudioSource> ().Pause ();
			} else {
				GameObject.Find ("BackgroundMusic").GetComponent<AudioSource> ().Pause ();
			}
	}

	public void Resume()
	{
		PlayCLickingNoise ();
		if (GameObject.FindGameObjectWithTag ("Character") != null) 
		{
			Time.timeScale = 1;
			Destroy (PauseMenuPannelInstantance, 0);
			Destroy (ResumeButtonInstance, 0);
			Destroy (RestartButtonInstance, 0);
			Destroy (MainMenuInstannce, 0);
			//GameObject.Find ("Music").GetComponent<AudioSource>().Play ();
		}
		if (GameObject.FindGameObjectWithTag ("Character") == null) 
		{
			Time.timeScale = 0;
			Destroy (PauseMenuPannelInstantance, 0);
			Destroy (ResumeButtonInstance, 0);
			Destroy (RestartButtonInstance, 0);
			Destroy (MainMenuInstannce, 0);
			//GameObject.Find ("Music").GetComponent<AudioSource>().Pause ();
		}

		GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Pause();
		if (PlayerMovement.StartedGame) {
			GameObject.Find ("MusicForWhenGameStarts").GetComponent<AudioSource> ().Play ();
		} else
		{
			GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Play();
		}


	}


	public void Restart()
	{
		PlayCLickingNoise ();
		if(PlayerPrefs.GetInt ("ButtonCLicked") == 0)
		{

		Time.timeScale = 1;
		AsyncOperation async = Application.LoadLevelAsync (1);

		//add to show adds
		if (PlayerPrefs.GetInt ("HasNoAdsBeenBought") <= 0)
		{
			addToTimesPlayed (1);
			if (PlayerPrefs.GetInt ("timesPlayed") >= 5) {
			
				RequestInterstitial ();
				ShowInterstitial ();
				PlayerPrefs.SetInt ("timesPlayed", 0);
			}
		}
		if (!async.isDone) 
		{
			GameObject loadingImage = (GameObject)Instantiate(LoadingImage, LoadingImage.transform.position, LoadingImage.transform.rotation);	
			loadingImage.transform.SetParent (GameObject.Find("Canvas").transform,false);
		}

			PlayerPrefs.SetInt ("ButtonCLicked",1);
		}
	}

	public void GoToGameFromCharacterSelect()
	{
		PlayCLickingNoise ();
		if(PlayerPrefs.GetInt ("ButtonCLicked") == 0)
		{
			
			Time.timeScale = 1;
			AsyncOperation async = Application.LoadLevelAsync (1);
			
			//add to show adds
			if (PlayerPrefs.GetInt ("HasNoAdsBeenBought") <= 0)
			{
				addToTimesPlayed (1);
				if (PlayerPrefs.GetInt ("timesPlayed") >= 5) {
					
					RequestInterstitial ();
					ShowInterstitial ();
					PlayerPrefs.SetInt ("timesPlayed", 0);
				}
			}
			if (!async.isDone) 
			{
				GameObject.Find("Canvas").transform.FindChild("BackArrow").gameObject.SetActive(false);
				GameObject loadingImage = (GameObject)Instantiate(Spinner, Spinner.transform.position, Spinner.transform.rotation);	
				loadingImage.transform.SetParent (GameObject.Find("Canvas").transform,false);
			}
			
			PlayerPrefs.SetInt ("ButtonCLicked",1);
		}
	}
	

	public void RestartForWhenMonsterAttacks()
	{
		if(PlayerPrefs.GetInt ("ButtonCLicked") == 0)
		{
		Time.timeScale = 1;
		Application.LoadLevel (1);

		if (PlayerPrefs.GetInt ("HasNoAdsBeenBought") <= 0)
			{
			addToTimesPlayed (1);
			if (PlayerPrefs.GetInt ("timesPlayed") >= 5) 
				{	
				RequestInterstitial ();
				ShowInterstitial ();
				PlayerPrefs.SetInt ("timesPlayed", 0);
				}
			}
			PlayerPrefs.SetInt ("ButtonCLicked",1);
		}
	}
	
	public void goToCharacterSelect()
	{
		PlayCLickingNoise ();
		if (PlayerPrefs.GetInt ("ButtonCLicked") == 0)
		{
			AsyncOperation async = Application.LoadLevelAsync (2);
			addToTimesPlayed (1);					//add for ads

			if (!async.isDone) 
			{
				GameObject loadingImage = (GameObject)Instantiate (LoadingImage, LoadingImage.transform.position, LoadingImage.transform.rotation);	
				loadingImage.transform.SetParent (GameObject.Find ("Canvas").transform, false);
				//Time.timeScale = 1;
			}
			PlayerPrefs.SetInt ("ButtonCLicked",1);
		}
	}


	public void LaunchHowToPlay()
	{ 
		PlayCLickingNoise ();
		if (ButtonCLickedForHowToPlay != true) 
		{
			howToPlay = (GameObject)Instantiate (HowToPlay, HowToPlay.transform.position, Quaternion.identity);	
			howToPlay.transform.SetParent (GameObject.Find ("Canvas").transform, false);

			exitButton = (GameObject)Instantiate (ExitButton, ExitButton.transform.position, Quaternion.identity);	
			exitButton.transform.SetParent (GameObject.Find ("Canvas").transform, false);
			ButtonCLickedForHowToPlay = true;
		}
	}

	public void CloseHowToPLay()
	{
		PlayCLickingNoise ();
		if (GameObject.Find ("HowToPlay(Clone)") != null) 
		{
			Destroy (GameObject.Find ("HowToPlay(Clone)"));
			Destroy (GameObject.Find ("ExitButton(Clone)"));
			ButtonCLickedForHowToPlay = false;
		}
	}

	void addToTimesPlayed(int ThisShouldBeone)
	{
		int timesPlayed = PlayerPrefs.GetInt("timesPlayed",0);    
		PlayerPrefs.SetInt ("timesPlayed", timesPlayed += ThisShouldBeone);
		PlayerPrefs.Save (); 
	}

	void PlayCLickingNoise()
	{
		GameObject.Find ("ButtonCLicked").GetComponent<AudioSource> ().Play ();
	}
}
