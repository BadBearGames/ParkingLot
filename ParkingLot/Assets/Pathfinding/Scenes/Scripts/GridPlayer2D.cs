using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridPlayer2D : Pathfinding2D
{
	enum directions {north,east,south,west};
	directions public currentDirection;

	void Start(){
		if(currentDirection == null){
			currentDirection = directions.north;
		}
	}

    void Update()
    {
        FindPath();
        if (Path.Count > 0)
        {
            Move();
        }
    }

    private void FindPath()
    {

		if(currentDirection == directions.north){
			FindPath(transform.position, transform.position + Vector3(0,1,0));
		}
		else if(currentDirection == directions.east){
			FindPath(transform.position, transform.position + Vector3(1,0,0));
		}
		else if(currentDirection == directions.south){
			FindPath(transform.position, transform.position + Vector3(0,-1,0));
		}
		else if(currentDirection == directions.west){
			FindPath(transform.position, transform.position + Vector3(-1,0,0));
		}
		//OLD MOVEMENT METHOD

		//Click to have player path to mouse's position
        /*if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                FindPath(transform.position, hit.point);
            }
        }*/
    }
}
