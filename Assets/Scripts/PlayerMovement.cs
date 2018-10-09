using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class PlayerMovement : GoogleMobileAdsDemoScript {

	public static int scoreNumber;

	public GameObject HighScoreImage;
	public GameObject NewScoreStarsImage;
	public Text BestScore;
	public Text RecentScore;
	public GameObject ReplayButton;
	public GameObject RateButton;
	public GameObject CharacterSelectAfterYoulost;
	public Text HowMuchMoneyYouGot;
	
	static Vector3 movement; 

	public GameObject player;
	public GameObject[] Enemy;

	public float TopPlayerPosition;
	public float BottomPlayerYPosition;
	static Vector3 PlayerPosition ;

	public Text Score;
	public Text Money;
	public Image coinImage;

	Text money;
	Text score;
	Image coin;

	int moneyGained;

	static public int closestEnemyToPlayerNumber;

	public static bool StartedGame;
	public static bool NotCalledOnceInUpdate;

	Animator animator;
	
	void Awake () 
	{
		scoreNumber = 0;
		PlayerPrefs.GetInt ("playeHasLost", 0);
		PlayerPrefs.SetInt ("playeHasLost", 0);

		StartedGame = false;

		PlayerPosition = gameObject.transform.position;
		movement = Vector3.zero; 
		player = GameObject.FindGameObjectWithTag ("Character");
		TopPlayerPosition = PlayerPosition.y + 5;
		BottomPlayerYPosition = PlayerPosition.y - 5;

		if (PlayerPrefs.GetInt ("HasNoAdsBeenBought") == 0) 
		{
			RequestInterstitial ();
		}

		closestEnemyToPlayerNumber = 0;
	}

	void Update()
	{
		if (PlayerPrefs.GetInt ("HasNoAdsBeenBought") == 0)
		{ 
			if (PlayerPrefs.GetInt ("timesPlayed") >= 5) {
			
				ShowInterstitial ();
			
				PlayerPrefs.SetInt ("timesPlayed", 0);
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		if(StartedGame)
		{
			MovePlayer();
		}

		Enemy = GameObject.FindGameObjectsWithTag("Enemy");				//has to go in update or else it won update th array
		if (Enemy.Length > 6) 
		{
			closestEnemyToPlayerNumber = Enemy.Length - 6;
		}
		if (player != null) 
		{

			Vector3 startOfLineTop = new Vector3(-2, TopPlayerPosition ,gameObject.transform.position.z);
			Vector3 endOfLineTop = new Vector3 (2,TopPlayerPosition,gameObject.transform.position.z);

			Vector3 startOfLineBotom = new Vector3(-2, BottomPlayerYPosition ,gameObject.transform.position.z);
			Vector3 endOfLineBotom = new Vector3 (2,BottomPlayerYPosition,gameObject.transform.position.z);

			Debug.DrawLine (startOfLineTop,endOfLineTop,Color.red);
			Debug.DrawLine (startOfLineBotom,endOfLineBotom,Color.red);

			if(Enemy.Length > 0)
			{
				if (transform.position.y > Enemy[closestEnemyToPlayerNumber].transform.position.y) 			//Enemy.Length - 4 enemy -4 because that will always be the on closest to the player since there can only be 4 enemies at a time
					{

						Vector2 endingPositionOfEnemy = new Vector2(0 ,Enemy[closestEnemyToPlayerNumber].transform.position.y);
						Enemy[closestEnemyToPlayerNumber].transform.position = Vector2.Lerp(Enemy[closestEnemyToPlayerNumber].transform.position,endingPositionOfEnemy,5 * Time.deltaTime);
					CallLostGame();
					GameObject.Find("LosingSound").GetComponent<AudioSource>().Play();
					LostAnimation();
					}
			}
			if(StartedGame && NotCalledOnceInUpdate)
			{
				NotCalledOnceInUpdate = false;

				score = (Text) Instantiate (Score,Score.transform.position,Score.transform.rotation);
				score.transform.SetParent (GameObject.Find ("Canvas").transform, false); 
				money = (Text) Instantiate (Money,Money.transform.position,Money.transform.rotation);
				coin = (Image)Instantiate (coinImage,coinImage.transform.position,coinImage.transform.rotation);
				money.transform.SetParent (GameObject.Find ("Canvas").transform, false);
				coin.transform.SetParent (GameObject.Find ("Canvas").transform, false); 
			}
			if(StartedGame)
			{
				//scoreNumber = (int)(player.transform.position.y) / 2;            // used to be player.transform.position.y + 2
				if (scoreNumber >= 0)
				{
					score.text = scoreNumber.ToString ();
				}
				if(scoreNumber < 0 )
				{
					score.text = "0";
				}
				money.text = "X " + PlayerPrefs.GetInt("Currency").ToString();
			}
		}
	}

	void MovePlayer()
	{
		transform.Translate (movement);
		TopPlayerPosition += movement.y;
		BottomPlayerYPosition += movement.y;

		if (TopPlayerPosition < 40 )				//tried .02
		{
			if (movement.y >.07f)
			{
				movement.y = .07f;
			}
			else
			{
				movement.y += .5f *.0002f;
			}
		} 

		if (TopPlayerPosition < 70 )				//tried .02
		{
			if (movement.y >.09f)
			{
				movement.y = .09f;
			}
			else
			{
				movement.y += .5f *.0001f;
			}
		} 
		if (TopPlayerPosition < 90 )				//tried .02
		{
			if (movement.y >.1f)
			{
				movement.y = .1f;
			}
			else
			{
				movement.y += .5f *.0001f;
			}
		}
		if (TopPlayerPosition < 110 )				//tried .02
		{
			if (movement.y >.13f)
			{
				movement.y = .13f;
			}
			else
			{
				movement.y += .3f *.0001f;
			}
		}
		if (TopPlayerPosition < 150 )				//tried .02
		{
			if (movement.y >.15f)
			{
				movement.y = .15f;
			}
			else
			{
				movement.y += .15f *.0002f;
			}
		}
		if (TopPlayerPosition < 170 )				//tried .02
		{
			if (movement.y >.17f)
			{
				movement.y = .17f;
			}
			else
			{
				movement.y += .1f *.0002f;
			}
		}
		if (TopPlayerPosition < 130 )				//tried .02
		{
			if (movement.y >.2f)
			{
				movement.y = .2f;
			}
			else
			{
				movement.y += .1f *.0002f;
			}
		}
	}


	void MoveCameraWhenPlayerIsABoveIt()
	{
		Vector3 cameraPositiontogoto = new Vector3(PlayerPosition.x, PlayerPosition.y += 3 , PlayerPosition.z); 
		PlayerPosition = Vector3.Lerp(transform.position, cameraPositiontogoto, .1f);

		gameObject.transform.position = PlayerPosition ;
		TopPlayerPosition = PlayerPosition.y + 5;
		BottomPlayerYPosition = PlayerPosition.y - 5;
	}
	
	void StoreHighscore(int newHighscore)
	{
		int oldHighscore = PlayerPrefs.GetInt("highscore",0);    
		if (newHighscore > oldHighscore) 
		{
			PlayerPrefs.SetInt ("highscore", newHighscore);
			PlayerPrefs.Save ();
			GameObject newScore = (GameObject)Instantiate (NewScoreStarsImage,NewScoreStarsImage.transform.position,NewScoreStarsImage.transform.rotation);
			newScore.transform.SetParent (GameObject.Find("Canvas").transform,false); 
		}
	}

	void StoreCurrency(int Currency)
	{
		int oldCurrency = PlayerPrefs.GetInt("Currency",0);    
		PlayerPrefs.SetInt ("Currency", oldCurrency += Currency);
		PlayerPrefs.Save (); 
	}

	void addToTimesPlayed(int ThisShouldBeOne)
	{
		int timesPlayed = PlayerPrefs.GetInt("timesPlayed",0);    
		PlayerPrefs.SetInt ("timesPlayed", timesPlayed += ThisShouldBeOne);
		PlayerPrefs.Save (); 
	}

	public void StartGame()
	{
		StartedGame = true;
		NotCalledOnceInUpdate = true;

		Destroy (GameObject.Find ("ShootButtonOnRight(Clone)"));
		Destroy (GameObject.Find ("ShootButtonOnLeft(Clone)"));
		Destroy (GameObject.Find ("CharacterSelectForStartOfGAme(Clone)"));
		Destroy(GameObject.Find("Canvas").gameObject.transform.FindChild("MonsterBusterIcon").gameObject);
		Destroy(GameObject.Find("Canvas").gameObject.transform.FindChild("QuestionMark").gameObject);
		Destroy (GameObject.Find("Canvas").gameObject.transform.FindChild("CharacterSelectForStartOfGAme").gameObject);
		Destroy (GameObject.Find ("ShootButtonOnRight"));
		Destroy (GameObject.Find ("ShootButtonOnLeft"));
	}

	public void LostAnimation ()
	{
		animator = GameObject.FindGameObjectWithTag ("Character").GetComponent<Animator> ();			//lost changing animation
		animator.SetBool ("Lost", true);
	}
	
	public void LostGame()
	{
		if (GameObject.Find ("HighScoreCanvas(Clone)") == null) 
			{
				GameObject highscoreImage = (GameObject)Instantiate (HighScoreImage, HighScoreImage.transform.position, Quaternion.identity);
		
				if (GameObject.Find ("Canvas") != null) 
				{	
					highscoreImage.transform.SetParent (GameObject.Find ("Canvas").transform, false); 
				}
		
				if (GameObject.Find ("Canvas") != null) 
				{	
					highscoreImage.transform.SetParent (GameObject.Find ("Canvas").transform, false); 
				}
		
			//curency for when the player loses 
			
			if (scoreNumber > 0) 
			{
				moneyGained = scoreNumber;
			}
		
				Text recentScore = (Text)Instantiate (RecentScore, RecentScore.transform.position, Quaternion.identity);
				recentScore.transform.SetParent (GameObject.Find ("Canvas").transform, false); 
		
				Text bestScore = (Text)Instantiate (BestScore, BestScore.transform.position, Quaternion.identity);
				bestScore.transform.SetParent (GameObject.Find ("Canvas").transform, false); 
				
				Text howMuchMoneyYouGot = (Text)Instantiate (HowMuchMoneyYouGot, HowMuchMoneyYouGot.transform.position, HowMuchMoneyYouGot.transform.rotation);
				howMuchMoneyYouGot.transform.SetParent (GameObject.Find ("Canvas").transform, false);
				
				recentScore.text = scoreNumber.ToString ();
				howMuchMoneyYouGot.text = moneyGained.ToString();
				StoreHighscore (scoreNumber);
				bestScore.text = PlayerPrefs.GetInt ("highscore").ToString (); 

				GameObject replayButton = (GameObject)Instantiate (ReplayButton, ReplayButton.transform.position, ReplayButton.transform.rotation);
				replayButton.transform.SetParent (GameObject.Find ("Canvas").transform, false);

				GameObject rateButton = (GameObject)Instantiate (RateButton, RateButton.transform.position, RateButton.transform.rotation);
				rateButton.transform.SetParent (GameObject.Find ("Canvas").transform, false);

			GameObject characterSelectAfterYouLost  = (GameObject)Instantiate(CharacterSelectAfterYoulost,CharacterSelectAfterYoulost.transform.position,CharacterSelectAfterYoulost.transform.rotation);
			characterSelectAfterYouLost.transform.SetParent (GameObject.Find ("Canvas").transform, false);

			}
		
			//GameObject.Find ("Music").GetComponent<AudioSource>().Pause ();
		
			//Destroy (player);
		
			StoreCurrency (moneyGained);
		
			addToTimesPlayed (1);
			GameObject.Find("MusicForWhenGameStarts").GetComponent<AudioSource>().Stop();
			GameObject.Find("Reward").GetComponent<AudioSource>().Play();

			Time.timeScale = 0;
	}

	public void CallLostGame()
	{
		if(GameObject.Find("Canvas").transform.FindChild("PauseButton") != null)
		{
			Destroy(GameObject.Find("Canvas").transform.FindChild("PauseButton").gameObject);
		}
		
		if (PlayerPrefs.GetInt ("playeHasLost") == 0) 
		{
			Invoke ("LostGame", 1);
		}
		PlayerPrefs.SetInt ("playeHasLost", 1);
	}
}
