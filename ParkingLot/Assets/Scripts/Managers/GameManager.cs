using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
	None
}

public class GameManager : Singleton<GameManager> 
{

	protected GameManager() {}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}
}

