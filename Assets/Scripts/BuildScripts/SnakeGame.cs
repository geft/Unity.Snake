﻿using UnityEngine;
using System.Collections;

public class SnakeGame : MonoBehaviour {
	
	private static SnakeGame instance = null;
	
	private GUIText displayLives;
	private GUIText displayScore;
	
	public int gameScore = 0;
	public int gameLives = 3;
	public int scoreMultiplier = 100;
	
	public static SnakeGame Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("SnakeGame").AddComponent<SnakeGame>();
			}
			
			return instance;
		}
	}
	
	public void OnApplicationQuit()
	{
		DestroyInstance();
	}
	
	public void DestroyInstance()
	{
		print ("Snake Game Instance destroyed");
		
		instance = null;
	}
	
	public void UpdateScore (int additive)
	{
		gameScore += additive * scoreMultiplier;
		
		displayScore.text = "Score: " + gameScore.ToString();
	}
	
	public void UpdateLives (int additive)
	{
		gameLives += additive;
		
		gameLives = Mathf.Clamp(gameLives, 0, gameLives);
		
		displayLives.text = "Lives: " + gameLives.ToString();
	}
	
	public void Initialize ()
	{
		print("SnakeGame initialized");
		
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		
		gameScore = 0;
		gameLives = 3;
		scoreMultiplier = 100;
		
		GUIHelper.CreateGUITexture(new Rect(0,0,1024,768), Color.grey, "ScreenBorder", 0);
		GUIHelper.CreateGUITexture(new Rect(22, 84, 980, 600), Color.black, "ScreenField", 1);
		
		displayScore = GUIHelper.CreateGetGUIText(new Vector2(10, 758), "Game Score", "Score", 1);
		UpdateScore(0);
		
		displayLives = GUIHelper.CreateGetGUIText(new Vector2(944,758), "Game Lives", "Lives", 1);
		UpdateLives(0);
	}
}
