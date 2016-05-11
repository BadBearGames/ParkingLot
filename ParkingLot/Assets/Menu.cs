using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{

	public Text centerText;
	
	// Update is called once per frame
	void Update () 
	{
		if (GameManager.Instance.CurrentState == GameState.GameOver)
		{
			centerText.text = "Loser!";
			centerText.gameObject.SetActive(true);
		}
		else if (GameManager.Instance.CurrentState == GameState.Win)
		{
			centerText.text = "You Won!";
			centerText.gameObject.SetActive(true);
		}
		else
		{
			centerText.gameObject.SetActive(false);
		}
	}
}
