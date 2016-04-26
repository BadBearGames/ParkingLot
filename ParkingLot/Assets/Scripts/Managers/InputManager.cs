using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : Singleton<InputManager> 
{
    #region Fields
    #endregion
    
    #region Properties
    ObjectBehavior[] objects;
    #endregion

    protected InputManager() {}

	void Awake()
	{
		DontDestroyOnLoad(this);

		Init();
	}

	void Update()
	{
        
        if (Input.GetKeyDown("space")) {
            foreach (ObjectBehavior o in objects) {
                o.tick();
            }
        }
		
	}

	void Init()
	{
        objects = FindObjectsOfType(typeof(ObjectBehavior)) as ObjectBehavior[];
    }
}

