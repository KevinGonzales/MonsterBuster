using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelectScript : MonoBehaviour {

	public GameObject[] PlayerChosen;
	public Sprite[] ImageForCharacter; 
	public Sprite[] NameOfPlayerChosen;
	public Sprite[] CostOfPlayerChosen;

	public GameObject PlayerLocked;
	public GameObject BuyButton ;
	public GameObject NotEnoughMoney;
	public GameObject[] randomThingsToInstantiate; 
	GameObject playerLocked;
	
	GameObject Character;
	Sprite CharacterImage;
	
	GameObject CurrentCharacter;
	Sprite CurrentCharacterImage;

	GameObject NextCharacter;
	Sprite NextCharacterImage;

	GameObject LastCharacter;
	Sprite LastCharacterImage;
	
	static int characterNumber;

	// Use this for initialization
	void Awake() 
	{
		PlayerPrefs.GetInt("intForSayingSomethingElse", 0);

		for(int i = 0; i < PlayerChosen.Length; i++)
		{
			PlayerPrefs.GetInt("p" + i, 0);
		}

		if(GameObject.Find("Character") != null)
		{
			Character = GameObject.Find("Character");
			CharacterImage = Character.GetComponent<Image> ().sprite;
			characterNumber = 0;

			CurrentCharacter = GameObject.Find("CurrentPlayer");
			CurrentCharacterImage = Character.GetComponent<Image> ().sprite;

			NextCharacter = GameObject.Find("NextPlayer");
			NextCharacterImage = Character.GetComponent<Image> ().sprite;

			LastCharacter = GameObject.Find("LastPlayer");
			LastCharacterImage = Character.GetComponent<Image> ().sprite;
		}

		if (GameObject.FindGameObjectWithTag ("tagToCHeckIfOnGame")) 
		{
			Instantiate (PlayerChosen [PlayerPrefs.GetInt("Character")], PlayerChosen [PlayerPrefs.GetInt("Character")].transform.position, Quaternion.identity);
		}

		PlayerPrefs.SetInt("p0", 1);
		PlayerPrefs.SetInt ("ButtonCLicked", 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(GameObject.FindGameObjectWithTag ("tagToCHeckIfOnCharacterSelelct"))
		   {
		if(PlayerPrefs.GetInt("p" + characterNumber) == 0)			//if character not bought
		{
			GameObject playerCost = GameObject.Find("CostOfPlayerChosen");				
			Sprite PlayerCostImage = playerCost.GetComponent<Image> ().sprite;
			PlayerCostImage = CostOfPlayerChosen [characterNumber];
			playerCost.GetComponent<Image> ().sprite = PlayerCostImage;	
		}
		
		if(PlayerPrefs.GetInt("p" + characterNumber) == 1) 			//if character has been bought
		{
			GameObject playerCost = GameObject.Find("CostOfPlayerChosen");				
			Sprite PlayerCostImage = playerCost.GetComponent<Image> ().sprite;
			PlayerCostImage = CostOfPlayerChosen [0];
			playerCost.GetComponent<Image> ().sprite = PlayerCostImage;	
		}
		
		if (GameObject.Find ("Character") != null)									//did to get rid of error when playing the game in scene 2
		{

			GameObject playerName = GameObject.Find("NameOfCharacter");				//how to change the gameobject image
			Sprite PlayerImageName = playerName.GetComponent<Image> ().sprite;
			PlayerImageName = NameOfPlayerChosen [characterNumber];
			playerName.GetComponent<Image> ().sprite = PlayerImageName;				//

			CharacterImage = ImageForCharacter [characterNumber];
			Character.GetComponent<Image> ().sprite = CharacterImage;

			CurrentCharacterImage = ImageForCharacter [PlayerPrefs.GetInt("Character")];
			CurrentCharacter.GetComponent<Image> ().sprite = CurrentCharacterImage;

			if (characterNumber +1 < ImageForCharacter.Length)
			{
				NextCharacterImage = ImageForCharacter [characterNumber +1];
				NextCharacter.GetComponent<Image> ().sprite = NextCharacterImage;
			}

			if(characterNumber -1 >= 0)
			{
				LastCharacterImage = ImageForCharacter [characterNumber -1];
				LastCharacter.GetComponent<Image> ().sprite = LastCharacterImage;
			}
		}



		if (PlayerPrefs.GetInt("p" + characterNumber) == 1)										//character has been bought
		{
			if(GameObject.Find ("Canvas").transform.Find("LockedImage(Clone)") != null)
			{
				Destroy(GameObject.Find ("Canvas").transform.Find("LockedImage(Clone)").gameObject);
				Destroy(GameObject.Find ("Canvas").transform.Find("BuyButton(Clone)").gameObject);
				//Destroy(GameObject.Find ("Canvas").transform.Find("CostOfPlayerChosen").gameObject);

			}
		}
		}
	}

	public void MoveToTheRightCharacter()
	{
		PlayCLickingNoise ();
		if (characterNumber < (ImageForCharacter.Length -1))
		{
			characterNumber += 1;
		}


		if (PlayerPrefs.GetInt("p" + characterNumber) == 0)											//add lock if character not bought yet
		{
			if(GameObject.Find ("Canvas").transform.Find("LockedImage(Clone)") == null)
			{
				playerLocked = (GameObject)Instantiate(PlayerLocked, PlayerLocked.transform.position, PlayerLocked.transform.rotation);
				playerLocked.transform.SetParent(GameObject.Find("Canvas").transform, false);

				GameObject buyButton = (GameObject) Instantiate(BuyButton, BuyButton.transform.position, BuyButton.transform.rotation);
				buyButton.transform.SetParent(GameObject.Find("Canvas").transform, false);

			}
		}
	}

	public void MoveToTheLeftCharacter()
	{
		PlayCLickingNoise ();
		if(characterNumber > -1)
		{
			characterNumber -= 1;

			if(characterNumber < 0)
			{
				characterNumber = 0;
			}
		}


		if (PlayerPrefs.GetInt("p" + characterNumber) == 0)											//add lock if character not bought yet
		{
			if(GameObject.Find ("Canvas").transform.Find("LockedImage(Clone)") == null)
			{
				playerLocked = (GameObject)Instantiate(PlayerLocked, PlayerLocked.transform.position, PlayerLocked.transform.rotation);
				playerLocked.transform.SetParent(GameObject.Find("Canvas").transform, false);

				GameObject buyButton = (GameObject) Instantiate(BuyButton, BuyButton.transform.position, BuyButton.transform.rotation);
				buyButton.transform.SetParent(GameObject.Find("Canvas").transform, false);
			}
		}
	}

	void PayWithCurrency(int Currency)
	{
		int oldCurrency = PlayerPrefs.GetInt("Currency",0);    
		PlayerPrefs.SetInt ("Currency", oldCurrency -= Currency);
		PlayerPrefs.Save (); 
	}
	void addToEasterEggTimer(int Currency)
	{
		int oldCurrency = PlayerPrefs.GetInt("intForSayingSomethingElse",0);    
		PlayerPrefs.SetInt ("intForSayingSomethingElse", oldCurrency += Currency);
		PlayerPrefs.Save (); 
	}


	public void ClickToBuy()
	{
		//characterNumber starts with 0
		if (characterNumber <= 1) 								// make the costs into diferent sections. So the first 4 cost 2 coins
		{
			if(PlayerPrefs.GetInt("Currency") >= 200)
			{
				PayWithCurrency (200);
				PlayerPrefs.SetInt ("p" + characterNumber, 1);
				PlayerPrefs.Save ();
				GameObject.Find ("PurchaseSound").GetComponent<AudioSource> ().Play ();
			}


			else
			{ 	/*
				addToEasterEggTimer(1);
				if(PlayerPrefs.GetInt("intForSayingSomethingElse") > 5) 						// if pleyer keeps trying to buy show a easteregg
				{
					int randomInt = Random.Range(0,randomThingsToInstantiate.Length);
					GameObject randomThing = (GameObject)Instantiate(randomThingsToInstantiate[randomInt],randomThingsToInstantiate[randomInt].transform.position,randomThingsToInstantiate[randomInt].transform.rotation); 
					randomThing.transform.SetParent(GameObject.Find("Canvas").transform, false);
					Destroy(randomThing.gameObject,1.5f);

					PlayerPrefs.SetInt("intForSayingSomethingElse",0);
				}*/

					//tell player not enough Money goes in an else if statement if top is not commented out
													
					GameObject notEnoughMoney = (GameObject) Instantiate(NotEnoughMoney,NotEnoughMoney.transform.position,NotEnoughMoney.transform.rotation);
					notEnoughMoney.transform.SetParent(GameObject.Find("Canvas").transform, false);
					GameObject.Find("CantBuy").GetComponent<AudioSource>().Play();
					Destroy(notEnoughMoney.gameObject,1.5f);
			}
		}

		else if (characterNumber == 2) 								// Then the following cost 4 coins so on
		{ 
			if(PlayerPrefs.GetInt("Currency") >= 1000)
			{
				PayWithCurrency (1000);
				PlayerPrefs.SetInt ("p" + characterNumber, 1);
				PlayerPrefs.Save ();
				GameObject.Find ("PurchaseSound").GetComponent<AudioSource> ().Play ();
			}
		
			else
			{
				/*addToEasterEggTimer(1);
				if(PlayerPrefs.GetInt("intForSayingSomethingElse") > 5) 						// if pleyer keeps trying to buy show a easteregg
				{
					int randomInt = Random.Range(0,randomThingsToInstantiate.Length);
					GameObject randomThing = (GameObject)Instantiate(randomThingsToInstantiate[randomInt],randomThingsToInstantiate[randomInt].transform.position,randomThingsToInstantiate[randomInt].transform.rotation); 
					randomThing.transform.SetParent(GameObject.Find("Canvas").transform, false);
					Destroy(randomThing.gameObject,1.5f);

					PlayerPrefs.SetInt("intForSayingSomethingElse",0);
				}
				else
				{ dont forgtet the other bracket*/
				//tell player not enough Money

					GameObject notEnoughMoney = (GameObject) Instantiate(NotEnoughMoney,NotEnoughMoney.transform.position,NotEnoughMoney.transform.rotation);
					notEnoughMoney.transform.SetParent(GameObject.Find("Canvas").transform, false);
					GameObject.Find("CantBuy").GetComponent<AudioSource>().Play();
					Destroy(notEnoughMoney.gameObject,1.5f);
			
			}
		}

		else if (characterNumber <= 5 ) 								// Then the following cost 4 coins so on
		{ 
			if(PlayerPrefs.GetInt("Currency") >= 2000)
			{
				PayWithCurrency (2000);
				PlayerPrefs.SetInt ("p" + characterNumber, 1);
				PlayerPrefs.Save ();
				GameObject.Find ("PurchaseSound").GetComponent<AudioSource> ().Play ();
			}
			
			else
			{	
				GameObject notEnoughMoney = (GameObject) Instantiate(NotEnoughMoney,NotEnoughMoney.transform.position,NotEnoughMoney.transform.rotation);
				notEnoughMoney.transform.SetParent(GameObject.Find("Canvas").transform, false);
				GameObject.Find("CantBuy").GetComponent<AudioSource>().Play();
				Destroy(notEnoughMoney.gameObject,1.5f);
				
			}
		}
		else if (characterNumber <= 7) 								// Then the following cost 4 coins so on
		{ 
			if(PlayerPrefs.GetInt("Currency") >= 1000)
			{
				PayWithCurrency (1000);
				PlayerPrefs.SetInt ("p" + characterNumber, 1);
				PlayerPrefs.Save ();
				GameObject.Find ("PurchaseSound").GetComponent<AudioSource> ().Play ();
			}
			
			else
			{	
				GameObject notEnoughMoney = (GameObject) Instantiate(NotEnoughMoney,NotEnoughMoney.transform.position,NotEnoughMoney.transform.rotation);
				notEnoughMoney.transform.SetParent(GameObject.Find("Canvas").transform, false);
				GameObject.Find("CantBuy").GetComponent<AudioSource>().Play();
				Destroy(notEnoughMoney.gameObject,1.5f);
				
			}
		}

		else if (characterNumber == 8) 								// Then the following cost 4 coins so on
		{ 
			if(PlayerPrefs.GetInt("Currency") >= 2000)
			{
				PayWithCurrency (2000);
				PlayerPrefs.SetInt ("p" + characterNumber, 1);
				PlayerPrefs.Save ();
				GameObject.Find ("PurchaseSound").GetComponent<AudioSource> ().Play ();
			}
			
			else
			{
				
				GameObject notEnoughMoney = (GameObject) Instantiate(NotEnoughMoney,NotEnoughMoney.transform.position,NotEnoughMoney.transform.rotation);
				notEnoughMoney.transform.SetParent(GameObject.Find("Canvas").transform, false);
				GameObject.Find("CantBuy").GetComponent<AudioSource>().Play();
				Destroy(notEnoughMoney.gameObject,1.5f);
				
			}
		}
		else if (characterNumber == 9) 								// Then the following cost 4 coins so on
		{ 
			if(PlayerPrefs.GetInt("Currency") >= 2500)
			{
				PayWithCurrency (2500);
				PlayerPrefs.SetInt ("p" + characterNumber, 1);
				PlayerPrefs.Save ();
				GameObject.Find ("PurchaseSound").GetComponent<AudioSource> ().Play ();
			}
			
			else
			{
				
				GameObject notEnoughMoney = (GameObject) Instantiate(NotEnoughMoney,NotEnoughMoney.transform.position,NotEnoughMoney.transform.rotation);
				notEnoughMoney.transform.SetParent(GameObject.Find("Canvas").transform, false);
				GameObject.Find("CantBuy").GetComponent<AudioSource>().Play();
				Destroy(notEnoughMoney.gameObject,1f);
				
			}
		}
	}

	public void  SelectCharacter()
	{

		if (PlayerPrefs.GetInt("p" + characterNumber) == 1)
		{
			ChangeCharacter (characterNumber);
			GameObject.Find ("CharacterChangeSound").GetComponent<AudioSource> ().Play ();
		}

		else
		{
			//give noise that sounds like failed attampt
			GameObject.Find("CantBuy").GetComponent<AudioSource>().Play();
		}
	}

	void PlayCLickingNoise()
	{
		GameObject.Find ("ButtonCLicked").GetComponent<AudioSource> ().Play ();
	}

	void ChangeCharacter(int numberForCharacter)
	{
		PlayerPrefs.SetInt ("Character", numberForCharacter);
		PlayerPrefs.Save ();
	}
}