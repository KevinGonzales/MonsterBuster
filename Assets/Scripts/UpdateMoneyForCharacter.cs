using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateMoneyForCharacter : MonoBehaviour 
{
	public Text Money;
	
	// Update is called once per frame
	void Update () 
	{
		Money.text = "X " + PlayerPrefs.GetInt("Currency").ToString();
	}
}
