using UnityEngine;
using System.Collections;

public class ScreenHelper : MonoBehaviour {

	public static IEnumerator FlashDeathScreen (int flashTimes, float flashDelay, Color flashColor)
	{
		GUITexture flashScreenTexture = GUIHelper.CreateGetGUITexture(new Rect(0, 0, 1024, 768), flashColor, 20);
		
		for (int i = 0; i < flashTimes; i++)
		{
			flashScreenTexture.color = flashColor;
			yield return new WaitForSeconds(flashDelay);
			flashScreenTexture.color = Color.clear;
			yield return new WaitForSeconds(flashDelay);
		}
		
		Destroy(flashScreenTexture.gameObject);
	}
}
