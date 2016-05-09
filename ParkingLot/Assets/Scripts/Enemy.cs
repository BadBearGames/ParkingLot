using UnityEngine;
using System.Collections;

public class Enemy : Pathfinding2D 
{
	public float speed = 20F;
	private bool search = true;
	public uint searchPerSecond = 5;
	public float searchDistance = 20F;
	private float tempDistance = 0F;

	//Movement Enums
	public enum movementTypes {directional,line,seekTarget,seekPosition,patrol,noMovement};
	public movementTypes movementType;

	//Directional Movement Variables
	//Enemy will move North, East, South or West in a straight line
	public enum directions {north,east,south,west};
	public directions currentDirection;

	//Line Movement Variables
	//Enemy will move in a predetermined straight line path (not contrained to NESW)
	public Vector2 lineMovement;
	
	//Seek Movement Variables
	//Enemy will seek a Target OR Position, this will most likely be related to the player
	public Transform seekTarget;
	public Vector3 seekPosition = Vector3.zero;
	public bool useSearchDistance = true;

	//Patrol Movement Variables
	//Enemy will patrol on a set path between two or more points
	public Vector2 waypoints;
	public enum patrolTypes {backAndForth,lastToFirst};	//back and forth: A->B->C->B->A (repeat); Last to First: A->B->C->A (repeat)
	public patrolTypes patrolType;

	void Start () 
    {
        //Make sure that we dont divide by 0 in our search timer coroutine
        if (searchPerSecond == 0)
            searchPerSecond = 1;

        //We do not want a negative distance
        if (searchDistance < 0)
            searchDistance = 0;
	}

	void Update(){
		//Tick();
	}
	
	public override void Tick () 
    {
		base.Tick();
		//save distance so we do not have to call it multiple times
		tempDistance = Vector3.Distance(transform.position, seekPosition);

		//Check if we are able to search
		if (search == true)	{
			//how we move
			switch(movementType){
			case movementTypes.directional:
				//Directional movement
				//We move in a straight line either north, east, south, or west
				if(currentDirection == directions.north){
					FindPath(transform.position, transform.position + new Vector3(0,2,0));
				}
				else if(currentDirection == directions.east){
					FindPath(transform.position, transform.position + new Vector3(2,0,0));
				}
				else if(currentDirection == directions.south){
					FindPath(transform.position, transform.position + new Vector3(0,-2,0));
				}
				else if(currentDirection == directions.west){
					FindPath(transform.position, transform.position + new Vector3(-2,0,0));
				}
				break;
			case movementTypes.line:
				//Line movement
				//We move in a straight line, set in the Inspector
				FindPath(transform.position, new Vector3(lineMovement.x, lineMovement.y, 0));
				break;
			case movementTypes.patrol:
				//Patrol movement
				//We move between set waypoints
				break;
			case movementTypes.seekPosition:
				//Seek Position Movement
				//We seek a position and move towards it

				//Start the time
				StartCoroutine(SearchTimer());
				
				//Now check the distance to the player, if it is within the distance it will search for a new path
				if (tempDistance < searchDistance || !useSearchDistance){
					FindPath(transform.position, seekPosition);
				}
				break;
			case movementTypes.seekTarget:
				//Seek Target Movement
				//We seek a target and move towards it

				//Make sure we have a target
				if (seekTarget != null){
					//Start the time
					StartCoroutine(SearchTimer());
					
					//Now check the distance to the player, if it is within the distance it will search for a new path
					if (tempDistance < searchDistance || !useSearchDistance){
						FindPath(transform.position, seekTarget.position);
					}
				}
				break;
			} //end of switch statement
		}

		//Make sure that we actually got a path! then call the new movement method
		if (Path.Count > 0)
		{
			MoveAI();
		}
	}

    IEnumerator SearchTimer()
    {
        //Set search to false for an amount of time, and then true again.
        search = false;
        yield return new WaitForSeconds(1 / searchPerSecond);
        search = true;
    }

    private void MoveAI()
    {
        //Make sure we are within distance + 1 added so we dont get stuck at exactly the search distance
        if (tempDistance < searchDistance + 1)
        {       
            //if we get close enough or we are closer then the indexed position, then remove the position from our path list, 
			if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(Path[0].x,Path[0].y)) < 0.2F) 
            {
                Path.RemoveAt(0);
            }   

            if(Path.Count < 1)
                return;

            //First we will create a new vector ignoring the depth (z-axiz).
            Vector3 ignoreZ = new Vector3(Path[0].x, Path[0].y, transform.position.z);
            
            //now move towards the newly created position
            transform.position = Vector3.MoveTowards(transform.position, ignoreZ, Time.deltaTime * speed);  
        }
    }
}
