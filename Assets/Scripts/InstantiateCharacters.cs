using UnityEngine;
using System.Collections;

public class InstantiateCharacters : MonoBehaviour 
{
	Camera cam;

	public Transform[] Enemies;
	public Transform[] Citizens;

	Vector3 EnemyPositionYValue;
	Vector3 CitizenPositionYValue;
	
	static float whatTheCameraHasToBeGreaterThan = 5 ; 
	static float moveUpBy = 2 ;

	GameObject EnemyClone;
	GameObject CitizenCLone;

	static int whatEnemyToInstantiate ;
	static int whatCitizenToInstantiate ;

	
	void Awake()
	{
		cam = Camera.main;
		int whatCharacterToInstantiate = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if ((cam.transform.position.y + 10) > whatTheCameraHasToBeGreaterThan) 
		{
			whatEnemyToInstantiate = Random.Range(0,Enemies.Length);
			whatCitizenToInstantiate = Random.Range(0,Citizens.Length);

			whatTheCameraHasToBeGreaterThan += 2f;
			InstantiateBothCharacters ();
			
			moveUpBy += 2;
			EnemyClone = GameObject.FindWithTag("Enemy");
			CitizenCLone = GameObject.FindWithTag("Citizen");
		}


		if (EnemyClone != null)
		{
			if (EnemyClone.transform.position.y < (cam.transform.position.y - 6))
			{
				Destroy(CitizenCLone);
			}
		}

		if (CitizenCLone != null)
		{
			if (CitizenCLone.transform.position.y < (cam.transform.position.y - 6))
			{
				Destroy(CitizenCLone);
			}
		}
	}
	
	void InstantiateBothCharacters()
	{
		EnemyPositionYValue = Enemies[whatEnemyToInstantiate].position;
		EnemyPositionYValue.y += moveUpBy;

		Vector2 PositionForLeft = new Vector2 (2,EnemyPositionYValue.y);
		Vector2 PositionForRight = new Vector2 (-2,EnemyPositionYValue.y);

		int randomInt  = Random.Range (0, 2);

		if (randomInt == 0)
		{
			Instantiate (Enemies[whatEnemyToInstantiate], PositionForLeft ,Enemies[whatEnemyToInstantiate].rotation);
			Instantiate (Citizens[whatCitizenToInstantiate], PositionForRight ,Enemies[whatEnemyToInstantiate].rotation);
		} 
		if (randomInt == 1) 
		{
			Instantiate (Enemies[whatEnemyToInstantiate], PositionForRight ,Enemies[whatEnemyToInstantiate].rotation);
			Instantiate (Citizens[whatCitizenToInstantiate], PositionForLeft ,Enemies[whatEnemyToInstantiate].rotation);
		}
	}

	public void RestartVariables()
	{
		whatTheCameraHasToBeGreaterThan = 5;
		moveUpBy = 2;
		EnemyPositionYValue.y = 5; 
		CitizenPositionYValue.y = 5;
		whatEnemyToInstantiate = 0;
		whatCitizenToInstantiate = 0;
	}
}