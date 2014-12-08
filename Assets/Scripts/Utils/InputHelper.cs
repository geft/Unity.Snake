using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputHelper : MonoBehaviour {

	public KeyCode Up, Down, Left, Right;
	public enum Direction {NONE, UP, DOWN, LEFT, RIGHT};
	public static Direction direction, buffer;
	
	private Button[] buttons;

	private void Start()
	{
		direction = Direction.NONE;
		buffer = Direction.NONE;
		
		buttons = GameObject.Find("GUI").GetComponentsInChildren<Button>();
		
		foreach (Button b in buttons)
		{
			if (b.name == "Up")
			{
				b.onClick.AddListener(delegate {
					if (direction != Direction.DOWN)
					{
						//Handheld.Vibrate();
						buffer = Direction.UP;
					}
				});
			}
			
			else if (b.name == "Down")
			{
				b.onClick.AddListener(delegate {
					if (direction != Direction.UP)
					{
						//Handheld.Vibrate();
						buffer = Direction.DOWN;
					}
				});
			}
			
			else if (b.name == "Left")
			{
				b.onClick.AddListener(delegate {
					if (direction != Direction.RIGHT && direction != Direction.NONE)
					{
						//Handheld.Vibrate();
						buffer = Direction.LEFT;
					}
				});
			}
			
			else if (b.name == "Right")
			{
				b.onClick.AddListener(delegate {
					if (direction != Direction.LEFT)
					{
						//Handheld.Vibrate();
						buffer = Direction.RIGHT;
					}
				});
			}
			
		}
		
	}

	private void Update()
	{
		if (SnakeGame.paused)
			return;
	
		CheckForInput();

		if (SnakeGame.ready)
		{
			direction = buffer;
		}
	}

	private void CheckForInput()
	{
		if (Input.GetKey(Up) && direction != Direction.DOWN)
		{
			buffer = Direction.UP;
		}
		
		else if (Input.GetKey(Down) && direction != Direction.UP)
		{
			buffer = Direction.DOWN;
		}
		
		else if (Input.GetKey(Left) && direction != Direction.RIGHT && direction != Direction.NONE)
		{
			buffer = Direction.LEFT;
		}
		
		else if (Input.GetKey(Right) && direction != Direction.LEFT)
		{
			buffer = Direction.RIGHT;
		}
	}
}
