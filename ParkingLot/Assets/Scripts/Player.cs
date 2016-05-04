using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Pathfinding2D
{
	public enum directions {north,east,south,west};
	public directions currentDirection;


    void Tick()
    {
        if (Path.Count > 0)
        {
            Move();
        } else {
			//FindPath();
			if(currentDirection == directions.north){
				FindPath(transform.position, transform.position + new Vector3(0,1,0));
			}
			else if(currentDirection == directions.east){
				FindPath(transform.position, transform.position + new Vector3(1,0,0));
			}
			else if(currentDirection == directions.south){
				FindPath(transform.position, transform.position + new Vector3(0,-1,0));
			}
			else if(currentDirection == directions.west){
				FindPath(transform.position, transform.position + new Vector3(-1,0,0));
			}
		}
    }
	void Update(){
		Tick ();
	}

   /* private void FindPath()
    {
		if(currentDirection == directions.north){
			FindPath(transform.position, transform.position + new Vector3(0,1,0));
		}
		else if(currentDirection == directions.east){
			FindPath(transform.position, transform.position + new Vector3(1,0,0));
		}
		else if(currentDirection == directions.south){
			FindPath(transform.position, transform.position + new Vector3(0,-1,0));
		}
		else if(currentDirection == directions.west){
			FindPath(transform.position, transform.position + new Vector3(-1,0,0));
		}
		//OLD MOVEMENT METHOD

		//Click to have player path to mouse's position
        /*if (Input.GetButtonDown("Fire1"))
        {
			//FindPath(transform.position, transform.position + new Vector3(0,1,0));
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
				//FindPath(transform.position, hit.point);
			}
        }
    }*/
}
