using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{	
			GameManager.Instance.CurrentState = GameState.Win;
			GameManager.Instance.AdvanceGameState(GameState.Win);
		}
	}
}
