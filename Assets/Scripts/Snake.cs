using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour {

	private Vector3 planePosition;
	private GameObject bodyParent;
	private List<GameObject> snakeBody;
	private List<Vector3> foodPositionList;
	private GameObject food;
	private int score;
	private GameObject bodyObject;
	private AudioClip move1, move2;

	// Use this for initialization
	void Start () {
		planePosition = GameObject.Find("Plane").transform.position;
		bodyParent = GameObject.Find("SnakeBody");
		snakeBody = new List<GameObject>();
		foodPositionList = new List<Vector3>();
		score = 0;
		
		InitialiseSnakeBody();
		GenerateFoodPositions();
		CreateFoodPiece();
		
		move1 = Resources.Load("Sounds/Move1 Blip") as AudioClip;
		move2 = Resources.Load("Sounds/Move2 Blip") as AudioClip;
		
		AudioSource bodyAudio = bodyParent.AddComponent<AudioSource>();
		bodyAudio.playOnAwake = false;
		bodyAudio.clip = move1;
	}
	
	// Update is called once per frame
	void Update () {
		if (InputHelper.direction == InputHelper.Direction.NONE)
			return;
			
		if (SnakeGame.paused)
			return;
	
		if (SnakeCollideWithSelf() || SnakeCollideWithWall())
		{
			SnakeGame.GameOver();
			audio.PlayOneShot(Resources.Load("Sounds/Death") as AudioClip);
		}
		
		if (foodPositionList.Count < 5)
		{
			SnakeGame.Victory();
		}
	
		if (SnakeGame.ready)
		{
			MoveSnake();
		
			if (SnakeCollideWithFood())
			{
				EatFood();
				CreateSnakePiece();
				CreateFoodPiece();
			}
		}
	}
	
	private void InitialiseSnakeBody ()
	{
		// create snake head
		GameObject snake = (GameObject) Instantiate(Resources.Load("SnakePrefab"));		
		snake.name = "SnakeHead";
		snake.transform.position = new Vector3(planePosition.x, planePosition.y, 0);
		snake.transform.rotation = Quaternion.AngleAxis(270, new Vector3(1, 0, 0));
		snakeBody.Add(snake);
		
		//create snake tail
		for (float i = 0.5f; i < 2; i += 0.5f)
		{
			GameObject body = (GameObject) Instantiate(Resources.Load("SnakePrefab"));
			body.name = "SnakePiece";
			body.transform.parent = bodyParent.transform;
			body.transform.position = new Vector3(planePosition.x - i, planePosition.y, 0);
			snakeBody.Add(body);
		}
	}
	
	private void CreateSnakePiece ()
	{
		if (snakeBody.Count < 2)
			return;
	
		GameObject body = (GameObject) Instantiate(Resources.Load("SnakePrefab"));
		
		body.name = "SnakePiece";
		body.transform.parent = bodyParent.transform;
		body.transform.rotation = Quaternion.AngleAxis(270, new Vector3(1, 0, 0));
		
		// find last two positions
		Vector3 tailPosition1 = snakeBody[snakeBody.Count - 1].transform.position;
		Vector3 tailPosition2 = snakeBody[snakeBody.Count - 2].transform.position;
		Vector3 diff = tailPosition1 - tailPosition2;
		
		// new tail is the sum of the two
		body.transform.position = tailPosition1 + diff;
		
		snakeBody.Add(body);
	}
	
	private void CreateFoodPiece ()
	{
		if (foodPositionList.Count == 0)
			return;
	
		// remove snake body positions from the list
		foreach (GameObject body in snakeBody)
		{
			foodPositionList.Remove(body.transform.position);
		}
		
		food = (GameObject) Instantiate(Resources.Load("SnakePrefab"));
		
		food.name = "Food";
	    food.transform.position = foodPositionList[Random.Range(0, foodPositionList.Count - 1)];
		food.transform.rotation = Quaternion.AngleAxis(270, new Vector3(1, 0, 0));
		food.renderer.material.color = Color.red;
	}
	
	private bool SnakeCollideWithFood()
	{
		if (food == null)
			return false;
		
		if (snakeBody[0].transform.position == food.transform.position)
			return true;
	
		return false;
	}
	
	private void EatFood ()
	{
		audio.Play();
	
		GameObject.Destroy(food);
		
		score += 10;
		
		SnakeGame.score.text = "Score: " + score;
	}
	
	private void GenerateFoodPositions()
	{
		for (float i = -4.0f; i <= 4; i += 0.5f)
		{
			for (float j = -4.0f; j <= 4; j += 0.5f)
			{
				foodPositionList.Add(new Vector3(planePosition.x + i, planePosition.y + j, 0));
			}
		}
	}
	
	private void MoveSnake()
	{
		if (snakeBody.Count < 2)
			return;
		
		// place body positions in a temporary list
		List<Vector3> bodyPositions = snakeBody.Select(pos => pos.transform.position).ToList();

		if (InputHelper.direction == InputHelper.Direction.UP)
			snakeBody[0].transform.position += new Vector3(0, 0.5f, 0);
		
		else if (InputHelper.direction == InputHelper.Direction.DOWN)
			snakeBody[0].transform.position += new Vector3(0, -0.5f, 0);
		
		else if (InputHelper.direction == InputHelper.Direction.LEFT)
			snakeBody[0].transform.position += new Vector3(-0.5f, 0, 0);
		
		else if (InputHelper.direction == InputHelper.Direction.RIGHT)
			snakeBody[0].transform.position += new Vector3(0.5f, 0, 0);
				
		// shift body coordinates towards the tail
		for (int i = 1; i < snakeBody.Count; i++)
		{
			snakeBody[i].transform.position = bodyPositions[i-1];
		}
		
		bodyParent.audio.clip = (bodyParent.audio.clip == move1) ? move2 : move1;
		bodyParent.audio.Play();
	}
	
	public bool SnakeCollideWithWall()
	{
		if (snakeBody[0].transform.position.x < (planePosition.x - 4.0f) ||
			snakeBody[0].transform.position.x > (planePosition.x + 4.0f) ||
		    snakeBody[0].transform.position.y < (planePosition.y - 4.0f) ||
		    snakeBody[0].transform.position.y > (planePosition.y + 4.0f))
		{
			return true;
		}
		
		return false;
	}
	
	public bool SnakeCollideWithSelf()
	{
		if (snakeBody.Count < 4)
			return false;
	
		for (int i = 3; i < snakeBody.Count; i++)
		{
			if (snakeBody[0].transform.position == snakeBody[i].transform.position)
				return true;
		}
	
		return false;
	}
}
