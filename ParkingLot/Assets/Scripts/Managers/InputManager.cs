using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : Singleton<InputManager> 
{
    #region Fields

    #endregion
    
    #region Properties
    #endregion

    protected InputManager() {}

	void Awake()
	{
		DontDestroyOnLoad(this);

		Init();
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

