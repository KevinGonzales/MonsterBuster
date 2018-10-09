using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerShooting : PlayerMovement 
{
	public GameObject LeftGunFire;
	GameObject leftGunFire;

	public GameObject RightGunFire;
	GameObject rightGunFire;

	public GameObject Angel;
	GameObject angel;

	public GameObject[] Citizen;
	static int closestCitizenToPlayerNumber;

	static bool PlayerHasLost;			// do not delete. This is so the player cant keep playting

	float startTime;


	//update awake ect do not get called on this script

	public void ClickedOnRight()
	{
		if (StartedGame == false) 
		{
			Enemy = GameObject.FindGameObjectsWithTag ("Enemy");
			if (Enemy [0].transform.position.x == 2) 
			{
				StartGame();
				GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Stop();
				GameObject.Find("MusicForWhenGameStarts").GetComponent<AudioSource>().Play();
			}
		}
		if (StartedGame == true && PlayerPrefs.GetInt ("playeHasLost") == 0 && PlayerHasLost == false)
		{
			if (GameObject.Find ("HighScoreCanvas(Clone)") == null) 
			{ 
				//usingInplace of fixedUPdate
	
				player = GameObject.FindGameObjectWithTag ("Character");
				closestEnemyToPlayerNumber = 0;
				closestCitizenToPlayerNumber = 1;


				Enemy = GameObject.FindGameObjectsWithTag ("Enemy");				//has to go in update or else it won update th array
				Citizen = GameObject.FindGameObjectsWithTag ("Citizen");


				for (int i = 0; i < Citizen.Length; i++)
				{
					if(Citizen[i].transform.position.y == Enemy[closestEnemyToPlayerNumber].transform.position.y)
					{
						closestCitizenToPlayerNumber = i; 
					}
				}

				Animator animator;
				animator = GameObject.FindGameObjectWithTag ("Character").GetComponent<Animator> ();			//gun changing animation
				animator.SetBool ("LeftCLicked", false);

				rightGunFire = (GameObject)Instantiate (RightGunFire, RightGunFire.transform.position, RightGunFire.transform.rotation);		
				rightGunFire.transform.SetParent (GameObject.FindWithTag ("Character").transform, false);
				Destroy (rightGunFire, .1f);

				if (Enemy [closestEnemyToPlayerNumber].transform.position.x == -2)
				{
					Vector2 citizenPosition = new Vector2 (2, Enemy [closestEnemyToPlayerNumber].transform.position.y);
					angel = (GameObject)Instantiate (Angel, citizenPosition, Enemy [closestEnemyToPlayerNumber].transform.rotation);
					Destroy (Citizen [closestCitizenToPlayerNumber].gameObject);

					CallLostGame();
					PlayerHasLost = true;
					LostAnimation();
					GameObject.Find("LosingSound").GetComponent<AudioSource>().Play();
				}
				if (Enemy [closestEnemyToPlayerNumber].transform.position.x == 2)
				{
					/*Vector2 endingPositionOfPlayer = new Vector2 (0, Enemy [closestEnemyToPlayerNumber].transform.position.y);
					player.transform.position = Vector2.Lerp (player.transform.position, endingPositionOfPlayer, .5f);*/

					GameObject.Find("ShootingSound").GetComponent<AudioSource>().Play();
					scoreNumber += 1;
					Destroy (Enemy [closestEnemyToPlayerNumber].gameObject);
				}
			}
		}
	}

	public void ClickedOnLeft()
	{
		if (StartedGame == false)
		{
			Enemy = GameObject.FindGameObjectsWithTag ("Enemy");
			if (Enemy [0].transform.position.x == -2) 
			{
				StartGame ();
				GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Stop();
				GameObject.Find("MusicForWhenGameStarts").GetComponent<AudioSource>().Play();
			}
		}

		if (StartedGame && PlayerPrefs.GetInt ("playeHasLost") == 0 && PlayerHasLost == false)
		{
			if (GameObject.Find ("HighScoreCanvas(Clone)") == null) { 
				//usingInplace of fixedUPdate

				player = GameObject.FindGameObjectWithTag ("Character");
				closestEnemyToPlayerNumber = 0;
				closestCitizenToPlayerNumber = 1;
		
		
				Enemy = GameObject.FindGameObjectsWithTag ("Enemy");				//has to go in update or else it won update th array
				Citizen = GameObject.FindGameObjectsWithTag ("Citizen");

				if (Enemy.Length > 6)
				{
					closestEnemyToPlayerNumber = Enemy.Length - 6;
				}


				for (int i = 0; i < Citizen.Length; i++)
				{
					if(Citizen[i].transform.position.y == Enemy[closestEnemyToPlayerNumber].transform.position.y)
					{
						closestCitizenToPlayerNumber = i; 
					}
				}

				//

				Animator animator;
				animator = GameObject.FindGameObjectWithTag ("Character").GetComponent<Animator> ();		//gun changing animation
				animator.SetBool ("LeftCLicked", true);

				leftGunFire = (GameObject)Instantiate (LeftGunFire, LeftGunFire.transform.position, LeftGunFire.transform.rotation);
				leftGunFire.transform.SetParent (GameObject.FindWithTag ("Character").transform, false);
				Destroy (leftGunFire, .1f); 

				if (Enemy [closestEnemyToPlayerNumber].transform.position.x == 2) 
				{
					Vector2 citizenPosition = new Vector2 (-2, Enemy [closestEnemyToPlayerNumber].transform.position.y);
					angel = (GameObject)Instantiate (Angel, citizenPosition, Enemy [closestEnemyToPlayerNumber].transform.rotation);
					Destroy (Citizen [closestCitizenToPlayerNumber].gameObject);


					CallLostGame();
					PlayerHasLost = true;
					LostAnimation();
					GameObject.Find("LosingSound").GetComponent<AudioSource>().Play();
				}
				if (Enemy [closestEnemyToPlayerNumber].transform.position.x == -2) {
					/*Vector2 endingPositionOfPlayer = new Vector2 (0, Enemy [closestEnemyToPlayerNumber].transform.position.y);
					player.transform.position = Vector2.Lerp (player.transform.position, endingPositionOfPlayer, .5f);*/

					GameObject.Find("ShootingSound").GetComponent<AudioSource>().Play();
					scoreNumber += 1;
					Destroy (Enemy [closestEnemyToPlayerNumber].gameObject);

				}
			}
		}
	}

	public void restartPlayerHasLost()
	{
		PlayerPrefs.SetInt ("playeHasLost", 0);
		PlayerHasLost = false;
	}
}
