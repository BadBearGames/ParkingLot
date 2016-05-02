using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log ("I'm hit!");

		//if the gameObject that hit the trigger is a player
		if (other.gameObject.name == "Player")
		{	
			Application.LoadLevel("Level Select");//loads the Level Slect Screen for now
		}
	}
}
