using UnityEngine;
using System.Collections;

public class RateButton: MonoBehaviour
{
	
	public void OnRateButtonClick()
	{
		#if UNITY_ANDROID
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.HowWeLife.MonsterBuster");
		#elif UNITY_IPHONE
		Application.OpenURL("");
		#endif
	}
	
}
