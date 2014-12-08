using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SnakeGame.Instance.Initialize();
		Food.Instance.Initialize();
		Snake.Instance.Initialize();
	}
}
