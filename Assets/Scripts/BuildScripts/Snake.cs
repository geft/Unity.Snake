using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
	
	private static Snake instance = null;
	
	private List<Rect> snakePos = new List<Rect>();
	private List<Texture2D> snakeIcon = new List<Texture2D>();
	private int snakeLength = 2;
	private float moveDelay = 0.5f;
	private AudioClip move1;
	private AudioClip move2;
	private AudioClip death;
	
	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}
	
	public static Snake Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("Snake").AddComponent<Snake>();
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
		print ("Snake Instance destroyed");
		
		instance = null;
	}
	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(UpdateSnake());
	}
	
	IEnumerator UpdateSnake()
	{
		while(true)
		{
			if (InputHelper.GetStandardMoveMultiInputKeys())
			{
				Debug.Log("We are pressing multiple keys for direction");
				yield return null;
				continue;
			}
			
			if (InputHelper.GetStandardMoveUpDirection())
			{
				yield return StartCoroutine(MoveSnake(Direction.UP));
			}
			
			if (InputHelper.GetStandardMoveLeftDirection())
			{
				yield return StartCoroutine(MoveSnake(Direction.LEFT));
			}
			
			if (InputHelper.GetStandardMoveDownDirection())
			{
				yield return StartCoroutine(MoveSnake(Direction.DOWN));
			}
			
			if (InputHelper.GetStandardMoveRightDirection())
			{
				yield return StartCoroutine(MoveSnake(Direction.RIGHT));
			}
			
			if (SnakeCollidedWithSelf())
			{
				break;
			}
			
			yield return new WaitForSeconds(moveDelay);
		}
		
		audio.clip = death;
		audio.Play();
		
		yield return StartCoroutine(ScreenHelper.FlashDeathScreen(6, 0.1f, new Color(1,0,0,0.5f)));
		
		SnakeGame.Instance.UpdateLives(-1);
		
		if (SnakeGame.Instance.gameLives == 0)
		{
			Application.LoadLevel("SnakeGame");
		}
		
		else
		{
			Initialize();
			
			Start();
		}
	}
	
	public IEnumerator MoveSnake (Direction moveDirection)
	{
		List<Rect> tempRects = new List<Rect>();
		Rect segmentRect = new Rect(0,0,0,0);
		
		for (int i = 0; i < snakePos.Count; i++)
		{
			tempRects.Add(snakePos[i]);
		}
		
		switch (moveDirection)
		{
			case Direction.UP:
				if (snakePos[0].y > 94)
				{
					snakePos[0] = new Rect(	snakePos[0].x, snakePos[0].y - 20, 
											snakePos[0].width, snakePos[0].height);
											
					UpdateMovePosition(tempRects);
					
					if (CheckForFood())
					{
						segmentRect = CheckForValidDownPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
							
							yield break;
						}
						
						segmentRect = CheckForValidRightPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
							
							yield break;
						}
						
						segmentRect = CheckForValidLeftPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
						}
					}
					
					audio.clip = (audio.clip == move1) ? move2 : move1;
					audio.Play();
				}
				
				break;
			
			case Direction.LEFT:
				if (snakePos[0].x > 22)
				{
					snakePos[0] = new Rect(	snakePos[0].x - 20, snakePos[0].y, 
					                       snakePos[0].width, snakePos[0].height);
					
					UpdateMovePosition(tempRects);
					
					if (CheckForFood())
					{
						segmentRect = CheckForValidRightPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
							
							yield break;
						}
						
						segmentRect = CheckForValidUpPosition();
						if (segmentRect.x != 0)
						{

							BuildSnakeSegment(segmentRect);
							

							snakeLength++;
							

							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
							

							yield break;

						}
						
						segmentRect = CheckForValidDownPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
						}
					}
					
					audio.clip = (audio.clip == move1) ? move2 : move1;
					audio.Play();
				}
				
				break;
				
			case Direction.DOWN:
				if (snakePos[0].y < 654)
				{
					snakePos[0] = new Rect(	snakePos[0].x, snakePos[0].y + 20, 
					                       snakePos[0].width, snakePos[0].height);
					
					UpdateMovePosition(tempRects);
					
					if (CheckForFood())
					{
						segmentRect = CheckForValidUpPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
							
							yield break;
						}
						
						segmentRect = CheckForValidLeftPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
							
							yield break;
						}
						
						segmentRect = CheckForValidRightPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
						}
					}
					
					audio.clip = (audio.clip == move1) ? move2 : move1;
					audio.Play();
				}
				
				break;
				
			case Direction.RIGHT:
				if (snakePos[0].y < 982)
				{
					snakePos[0] = new Rect(	snakePos[0].x +20, snakePos[0].y, 
					                       snakePos[0].width, snakePos[0].height);
					
					UpdateMovePosition(tempRects);
					
					if (CheckForFood())
					{
						segmentRect = CheckForValidDownPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
							
							yield break;
						}
						
						segmentRect = CheckForValidUpPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
							
							yield break;
						}
						
						segmentRect = CheckForValidLeftPosition();
						
						if (segmentRect.x != 0)
						{
							BuildSnakeSegment(segmentRect);
							
							snakeLength++;
							
							moveDelay = Mathf.Max(0.05f, moveDelay - 0.01f);
						}
					}
					
					audio.clip = (audio.clip == move1) ? move2 : move1;
					audio.Play();
				}
				
				break;
			}
		
		yield return null;
	}
	
	private void UpdateMovePosition(List<Rect> tmpRects)
	{
		for (int i = 0; i < tmpRects.Count - 1; i++)
		{
			snakePos[i+1] = tmpRects[i];
		}
	}
	
	private bool CheckForFood()
	{
		if (Food.Instance != null)
		{
			Rect foodRect = Food.Instance.foodPos;
			
			if (snakePos[0].Contains(new Vector2(foodRect.x, foodRect.y)))
			{
				Debug.Log("We hit the food");
				
				Food.Instance.UpdateFood();
				
				SnakeGame.Instance.UpdateScore(1);
				
				return true;
			}
		}
		
		return false;
	}
	
	private Rect CheckForValidDownPosition()
	{
		if (snakePos[snakePos.Count-1].y != 654)
		{
			return new Rect(snakePos[snakePos.Count-1].x, snakePos[snakePos.Count-1].y - 20, 20, 20);
		}
		
		return new Rect(0,0,0,0);
	}
	
	private Rect CheckForValidUpPosition()
	{
		if (snakePos[snakePos.Count-1].y != 94)
		{
			return new Rect(snakePos[snakePos.Count-1].x, snakePos[snakePos.Count-1].y + 20, 20, 20);
		}
		
		return new Rect(0,0,0,0);
	}
	
	private Rect CheckForValidLeftPosition()
	{
		if (snakePos[snakePos.Count-1].x != 22)
		{
			return new Rect(snakePos[snakePos.Count-1].x - 20, snakePos[snakePos.Count-1].y, 20, 20);
		}
		
		return new Rect(0,0,0,0);
	}
	
	private Rect CheckForValidRightPosition()
	{
		if (snakePos[snakePos.Count-1].x != 982)
		{
			return new Rect(snakePos[snakePos.Count-1].x + 20, snakePos[snakePos.Count-1].y, 20, 20);
		}
		
		return new Rect(0,0,0,0);
	}
	
	private void BuildSnakeSegment(Rect rctPos)
	{
		snakeIcon.Add(TextureHelper.CreateTexture(20, 20, Color.green));
		snakePos.Add(rctPos);
	}
	
	private bool SnakeCollidedWithSelf()
	{
		bool didCollide = false;
		
		if (snakePos.Count <= 4)
		{
			return false;
		}
		
		for (int i = 0; i < snakePos.Count; i++)
		{
			if (i > 0)
			{
				if (snakePos[0].x == snakePos[snakePos.Count - i].x && 
				    snakePos[0].y == snakePos[snakePos.Count - i].y)
				{
					didCollide = true;
					
					break;
				}
			}
		}
		
		return didCollide;
	}
	
	void OnGUI()
	{
		for (int i = 0; i < snakeLength; i++)
		{
			GUI.DrawTexture(snakePos[i], snakeIcon[i]);
		}
	}
	
	public void Initialize()
	{
		print ("Snake initialized");
		
		snakePos.Clear();
		snakeIcon.Clear();
		
		snakeLength = 2;
		moveDelay = 0.5f;
		
		if (!gameObject.GetComponent<AudioSource>())
		{
			move1 = Resources.Load("Sounds/Move1 Blip") as AudioClip;
			move2 = Resources.Load("Sounds/Move2 Blip") as AudioClip;
			death = Resources.Load("Sounds/Death") as AudioClip;
		
			gameObject.AddComponent<AudioSource>();
			audio.playOnAwake = false;
			audio.loop = false;
			audio.clip = move1;
		}
		
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		
		snakeIcon.Add(TextureHelper.CreateTexture(20, 20, Color.green));
		snakeIcon.Add(TextureHelper.CreateTexture(20, 20, Color.green));
		
		snakePos.Add(new Rect(	Screen.width * 0.5f - 10, 
								Screen.height * 0.5f - 10,
								snakeIcon[0].width,
								snakeIcon[0].height));
								
		snakePos.Add(new Rect(	Screen.width * 0.5f + 10, 
		                      	Screen.height * 0.5f - 10,
		                      	snakeIcon[1].width,
		                      	snakeIcon[1].height));
	}
}