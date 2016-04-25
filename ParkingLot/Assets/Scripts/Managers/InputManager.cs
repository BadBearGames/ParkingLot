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
		
	}

	void Init()
	{
		
	}
}

