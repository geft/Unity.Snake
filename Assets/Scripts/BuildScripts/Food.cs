﻿using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	public Rect foodPos = new Rect(0,0,20,20);
	
	private static Food instance = null;
	private static int[] initXPos = new int[49];
	private static int[] initYPos = new int[29];
	
	private Texture2D foodTexture;
	private AudioClip foodPickup;
	
	void Start()
	{
		int xPosBase = 22;
		int yPosBase = 94;
	
		for (int i = 0; i < 49; i++)
		{
			initXPos[i] = xPosBase;
			xPosBase += 20;
		}
		
		for (int i = 0; i < 29; i++)
		{
			initYPos[i] = yPosBase;
			yPosBase += 20;
		}
		
		print (initXPos[48].ToString());
	}
	
	public static Food Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("Food").AddComponent<Food>();
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
		print("Food Instance destroyed");
		
		instance = null;
	}
	
	public void UpdateFood()
	{
		print("Food updated");
		
		audio.Play();
		
		int ranX = Random.Range(0, initXPos.Length);
		int ranY = Random.Range(0, initYPos.Length);
		
		foodPos = new Rect(initXPos[ranX], initYPos[ranY], 20, 20);
	}
	
	void OnGUI()
	{
		if (Food.Instance != null)
		{
			GUI.DrawTexture(foodPos, foodTexture);
		}
	}
	
	public void Initialize()
	{
		print("Food initialized");
		
		if (!gameObject.GetComponent<AudioSource>())
		{
			foodPickup = Resources.Load("Sounds/Food Pickup") as AudioClip;
			
			gameObject.AddComponent<AudioSource>();
			
			audio.playOnAwake = false;
			audio.loop = false;
			audio.clip = foodPickup;
		}
		
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		
		foodTexture = TextureHelper.CreateTexture(20, 20, Color.red);
		
		int ranX = Random.Range(0, initXPos.Length);
		int ranY = Random.Range(0, initYPos.Length);
		
		foodPos = new Rect(initXPos[ranX], initYPos[ranY], 20, 20);
	}
}
