using UnityEngine;
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
	private Dictionary<ObjectType, List<Pathfinding2D>> objects;
	#endregion 

	static GameManager _instance;

	#region Properties
	//Gamestate and timers
	public GameState CurrentState { get { return currentState; } set { currentState = value; } }
	public Dictionary<GameState, float> StateTimers { get { return stateTimers; } }
	public Dictionary<GameState, float> StateTimersMax { get { return stateTimersMax; } }

	//Objects
	public Dictionary<ObjectType, List<Pathfinding2D>> Objects { get { return objects; } }
	#endregion

	//fixes the singleton error by forcing it to find a new instance of it upon level reset
	//source: http://forum.unity3d.com/threads/static-variables-persist-for-life-of-program.80507/
	public static GameManager Instance {
		get {
			if (_instance == null){
				_instance = GameObject.Find("GameManager").GetComponent<GameManager>();
			}
			
			return _instance;
		}
	}


	protected GameManager() {}

	void Awake()
	{
		//DontDestroyOnLoad(this); //commented this out

		Init();
	}


	/// <summary>
	/// Constant update logic, the only realtime update loop
	/// </summary>
	void Update()
	{
		if (stateTimers.ContainsKey(currentState) && stateTimers[currentState] == Mathf.Infinity)
		{
			//State does not depend on a timer, so execute here. Essentially just instantanous execution and then move to next state.
			if (currentState == GameState.HumanTurn)
			{
				//Don't do anything because this is controlled in the hud
			}
			else if (currentState == GameState.EnemyTurn)
			{
				//call update on all enemies in the dictionary
				foreach (Enemy enemy in objects[ObjectType.Enemy])
				{
					enemy.Tick();
				}
				AdvanceGameState(currentState);
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
		stateTimersMax.Add(GameState.None, Mathf.Infinity);
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
		objects = new Dictionary<ObjectType, List<Pathfinding2D>>();
		objects.Add(ObjectType.Human, new List<Pathfinding2D>());
		objects.Add(ObjectType.Enemy, new List<Pathfinding2D>());
		//For now, just add all objects to the human list
		Pathfinding2D[] objectsArray = FindObjectsOfType(typeof(Player)) as Player[];
		for (int i = 0; i < objectsArray.Length; i++)
		{
			objects[ObjectType.Human].Add(objectsArray[i]);
		}
		//Add enemies
		objectsArray = FindObjectsOfType(typeof(Enemy)) as Enemy[];
		for (int i = 0; i < objectsArray.Length; i++)
		{
			objects[ObjectType.Enemy].Add(objectsArray[i]);
		}

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

		case GameState.HumanTurn:
			this.currentState = GameState.EnemyTurn;
			break;

		case GameState.EnemyTurn:
			this.currentState = GameState.HumanTurn;
			break;
		}

		//Reset the timer for the previous state
		stateTimers[currentState] = stateTimersMax[currentState];
	}
}
