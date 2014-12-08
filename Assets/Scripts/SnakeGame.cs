using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SnakeGame : MonoBehaviour {
	
	public static int speed;
	public static bool ready;
	public static bool paused;
	public static Text score;

	private const int maxSpeed = 25;
	private static int timer;
	
	void Start()
	{
		speed = 10;
		ready = false;
		paused = false;
		timer = 0;
		
		score = GameObject.Find("GUI").GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		ready = false;
		
		if (timer == maxSpeed + 1 - speed)
		{
			ready = true;
			timer = 0;
		}
		
		timer++;
	}
	
	public static void GameOver()
	{
		paused = true;

		GameObject gameOverScreen = Instantiate(Resources.Load("gameOverScreen")) as GameObject;
		
		Button[] buttons = gameOverScreen.GetComponentsInChildren<Button>();
		
		foreach (Button b in buttons)
		{
			if (b.name == "ButtonRestart")
			{
				b.onClick.AddListener(delegate {
					Application.LoadLevel(Application.loadedLevel);
					});
			}
			
			else if (b.name == "ButtonQuit")
			{
				b.onClick.AddListener(delegate {
					Application.Quit();
				});
			}
		}
	}
	
	public static void Victory()
	{
		paused = true;
		
		Instantiate(Resources.Load("victoryScreen"));
	}
}
