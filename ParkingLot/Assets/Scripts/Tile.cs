using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour 
{
	#region Fields
	//assigned
	public Direction direction;

	//private
	private GameObject arrowSprite;
	#endregion

	void Start()
	{
		arrowSprite = transform.GetChild(0).gameObject;

		//rotate arrow sprite to its direction
		switch (direction)
		{
		case Direction.east:
			arrowSprite.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
			break;

		case Direction.west:
			arrowSprite.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			break;
		case Direction.north:
			arrowSprite.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
			break;
		case Direction.south:
			arrowSprite.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
			break;
		}
	}
}
