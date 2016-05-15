using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : Singleton<InputManager> 
{
    #region Fields

    #endregion
    
    #region Properties
    #endregion

	static InputManager _instance;

    protected InputManager() {}

	void Awake()
	{
		//DontDestroyOnLoad(this);

		Init();
	}

	//fixes the singleton error by forcing it to find a new instance of it upon level reset
	//source: http://forum.unity3d.com/threads/static-variables-persist-for-life-of-program.80507/
	public static InputManager Instance {
		get {
			if (_instance == null){
				_instance = GameObject.Find("InputManager").GetComponent<InputManager>();
			}

			return _instance;
		}
	}

	public void AdvancePlayerTurn()
	{
		if (GameManager.Instance.CurrentState == GameState.HumanTurn || GameManager.Instance.CurrentState == GameState.Start)
		{
			foreach (Player o in GameManager.Instance.Objects[ObjectType.Human]) 
			{
				//Putting all objects in the human list for now, change that later
				o.Tick();
			}

			//Let the game manager know to move forward
			GameManager.Instance.AdvanceGameState(GameState.HumanTurn);
		}
	}

	void Init()
	{
        
    }
}

