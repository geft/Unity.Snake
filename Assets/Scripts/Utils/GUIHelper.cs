using UnityEngine;
using System.Collections;

public class GUIHelper : MonoBehaviour {

	public static GUIText CreateGetGUIText(Vector2 offset, string strText, float layer)
	{
		return CreateGetGUIText(offset, "GUITextObject", strText, layer);
	}
	
	public static GUIText CreateGetGUIText(Vector2 offset, string name, string strText, float layer)
	{
		GameObject guiTextObject = new GameObject();
		
		guiTextObject.transform.position = new Vector3(0, 0, layer);
		guiTextObject.transform.rotation = Quaternion.identity;
		guiTextObject.transform.localScale = Vector3.one;
		
		GUIText guiDisplayText = guiTextObject.AddComponent<GUIText>();
		guiDisplayText.pixelOffset = offset;
		guiDisplayText.text = strText;
		
		return guiDisplayText;
	}
	
	public static void CreateGUITexture (Rect coordinates, Color colTexture, float layer)
	{
		CreateGUITexture (coordinates, colTexture, "GUITextureObject", layer);
	}
	
	public static void CreateGUITexture (Rect coordinates, Color colTexture, string name, float layer)
	{
		GameObject guiTextureObject = new GameObject(name);
		
		guiTextureObject.transform.position = new Vector3(0, 0, layer);
		guiTextureObject.transform.rotation = Quaternion.identity;
		guiTextureObject.transform.localScale = new Vector3(0.01f, 0.01f, 1.0f);
		
		GUITexture guiDisplayTexture = guiTextureObject.AddComponent<GUITexture>();
		
		Texture2D guiTexture = TextureHelper.Create1x1Texture(colTexture);
		
		guiDisplayTexture.texture = guiTexture;
		guiDisplayTexture.pixelInset = coordinates;
	}
	
	public static GUITexture CreateGetGUITexture (Rect coordinates, Color colTexture, float layer)
	{
		return CreateGetGUITexture(coordinates, colTexture, "GUITextureObject", layer);
	}
	
	public static GUITexture CreateGetGUITexture (Rect coordinates, Color colTexture, string name, float layer)
	{
		GameObject guiTextureObject = new GameObject(name);
		
		guiTextureObject.transform.position = new Vector3(0, 0, layer);
		guiTextureObject.transform.rotation = Quaternion.identity;
		guiTextureObject.transform.localScale = new Vector3(0.01f, 0.01f, 1.0f);
		
		GUITexture guiDisplayTexture = guiTextureObject.AddComponent<GUITexture>();
		
		Texture2D guiTexture = TextureHelper.Create1x1Texture(colTexture);
		
		guiDisplayTexture.texture = guiTexture;
		guiDisplayTexture.pixelInset = coordinates;
		
		return guiDisplayTexture;
	}
}
