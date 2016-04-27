﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState
{
	None,
	Start,
	HumanTurn,
	EnemyTurn,
	GameOver,
	Win
}

public enum ObjectType
{
	Human,
	Enemy
}

public class GameManager : Singleton<GameManager> 
{
	#region Fields
	//Gamestate and timers
	private GameState currentState;
	private Dictionary<GameState, float> stateTimers;
	private Dictionary<GameState, float> stateTimersMax;


	//Objects
	private Dictionary<ObjectType, List<Object>> objects;
	#endregion 


	#region Properties
	//Gamestate and timers
	public GameState CurrentState { get { return currentState; } }
	public Dictionary<GameState, float> StateTimers { get { return stateTimers; } }
	public Dictionary<GameState, float> StateTimersMax { get { return stateTimersMax; } }

	//Objects
	public Dictionary<ObjectType, List<Object>> Objects;
	#endregion

	protected GameManager() {}

	void Awake()
	{
		DontDestroyOnLoad(this);

		Init();
	}

	/// <summary>
	/// Constant update logic, the only realtime update loop
	/// </summary>
	void Update()
	{
		if (stateTimers.ContainsKey(currentState) && stateTimers[currentState] == Mathf.Infinity)
		{
			//State does not depend on a timer, so execute here
			if (currentState == GameState.HumanTurn)
			{
				
			}
			else if (currentState == GameState.EnemyTurn)
			{
				//call update on all enemies in the dictionary

			}
		}
		if (stateTimers.ContainsKey(currentState) && stateTimers[currentState] != Mathf.Infinity)
		{
			if (stateTimers[currentState] > 0f)
			{
				stateTimers[currentState] -= Time.deltaTime;
			}
			else
			{
				AdvanceGameState(currentState);
			}
		}
	}

	/// <summary>
	/// Essentially resets the GameManager
	/// </summary>
	void Init()
	{
		//Init Gamestate timers
		stateTimersMax = new Dictionary<GameState, float>();
		stateTimersMax.Add(GameState.Start, 3f);
		stateTimersMax.Add(GameState.HumanTurn, Mathf.Infinity);
		stateTimersMax.Add(GameState.EnemyTurn, Mathf.Infinity);
		stateTimersMax.Add(GameState.GameOver, 3f);
		stateTimersMax.Add(GameState.Win, 3f);

		//Set state timers equal to whats in stateTimersMax
		stateTimers = new Dictionary<GameState, float>();
		foreach (GameState key in stateTimersMax.Keys)
		{
			stateTimers.Add(key, stateTimersMax[key]);
		}

		//Init Objects
		objects = new Dictionary<ObjectType, List<Object>>();
		objects.Add(ObjectType.Human, new List<Object>());
		objects.Add(ObjectType.Enemy, new List<Object>());

		//Init game state
		currentState = GameState.None;
		AdvanceGameState(currentState);
	}

	/// <summary>
	/// Goes to the next state given the current state
	/// </summary>
	/// <param name="currentState">Current state.</param>
	public void AdvanceGameState(GameState currentState)
	{
		//Set to next state, also check logic
		switch (currentState)
		{
		case GameState.None:
			this.currentState = GameState.Start;
			break;

		case GameState.Start:
			this.currentState = GameState.HumanTurn;
			break;
		}

		//Reset the timer for the previous state
		stateTimers[currentState] = stateTimersMax[currentState];
	}
}