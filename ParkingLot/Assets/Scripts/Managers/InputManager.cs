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

	void Update()
	{
        
        if (Input.GetKeyDown("space")) 
		{
			
			foreach (Pathfinding2D o in GameManager.Instance.Objects[ObjectType.Human]) 
			{
				//Putting all objects in the human list for now, change that later
                o.Tick();
            }
        }
		
	}

	void Init()
	{
        
    }
}

