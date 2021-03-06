using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction {north,east,south,west};

public class Player : Pathfinding2D
{
	public Direction currentDirection = Direction.north;


    public override void Tick()
    {
		base.Tick ();
		switch (currentDirection)
		{
		case Direction.east:
			transform.Translate(new Vector3(1, 0, 0));
			break;
		case Direction.west:
			transform.Translate(new Vector3(-1, 0, 0));
			break;
		case Direction.north:
			transform.Translate(new Vector3(0, 1, 0));
			break;
		case Direction.south:
			transform.Translate(new Vector3(0, -1, 0));
			break;
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Tile")
		{
			currentDirection = other.gameObject.GetComponent<Tile>().direction;
		}
	}

	public void Die()
	{
		//Kill this player
		GameManager.Instance.Objects[ObjectType.Human].Remove(this);
		Destroy(gameObject);

		if (GameManager.Instance.Objects[ObjectType.Human].Count == 0)
		{
			GameManager.Instance.AutoPlay = false;
			GameManager.Instance.CurrentState = GameState.GameOver;
			GameManager.Instance.AdvanceGameState(GameState.GameOver);
		}
	}
}
