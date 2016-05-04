using UnityEngine;
using System.Collections;

public class Enemy : Pathfinding2D 
{
	public float speed = 20F;
	private bool search = true;
	private float tempDistance = 0F;

	//Movement Enums
	public enum movementTypes {directional,line,seekTarget,seekPosition,patrol};
	public movementTypes movementType;

	//Directional Movement Variables
	public enum directions {north,east,south,west};
	public directions currentDirection;

	//Line Movement Variables
	public Vector2 lineMovement;
	
	//Seek Movement Variables
	public Transform seekTarget;
	public Vector3 seekPosition = Vector3.zero;
	public uint searchPerSecond = 5;
    public float searchDistance = 20F;
	public bool useSearchDistance = true;

	//Patrol Movement Variables
	public Vector2 waypoints;
	public enum patrolTypes {backAndForth,lastToFirst};
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
	
	public override void Tick () 
    {
		base.Tick();

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
			FindPath(transform.position, transform.position + new Vector3(lineMovement.x, lineMovement.y, 0));
			break;
		case movementTypes.patrol:
			//Patrol movement
			//We move between set waypoints
			break;
		case movementTypes.seekPosition:
			//Seek Position Movement
			//We seek a position and move towards it

			//save distance so we do not have to call it multiple times
			tempDistance = Vector3.Distance(transform.position, seekPosition);
			
			//Check if we are able to search
			if (search == true)	{
				//Start the time
				StartCoroutine(SearchTimer());
				
				//Now check the distance to the player, if it is within the distance it will search for a new path
				if (tempDistance < searchDistance || !useSearchDistance){
					FindPath(transform.position, seekPosition);
				}
			}
			break;
		case movementTypes.seekTarget:
			//Seek Target Movement
			//We seek a target and move towards it

			//Make sure we have a target
			if (seekTarget != null)
			{
				//save distance so we do not have to call it multiple times
				tempDistance = Vector3.Distance(transform.position, seekTarget.position);
				
				//Check if we are able to search
				if (search == true)	{
					//Start the time
					StartCoroutine(SearchTimer());
					
					//Now check the distance to the player, if it is within the distance it will search for a new path
					if (tempDistance < searchDistance || !useSearchDistance){
						FindPath(transform.position, seekTarget.position);
					}
				}
				
			}
			break;
		} //end of switch statement

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
            if (Vector3.Distance(transform.position, Path[0]) < 0.2F) 
            {
                Path.RemoveAt(0);
            }   

            if(Path.Count < 1)
                return;

            //First we will create a new vector ignoreing the depth (z-axiz).
            Vector3 ignoreZ = new Vector3(Path[0].x, Path[0].y, transform.position.z);
            
            //now move towards the newly created position
            transform.position = Vector3.MoveTowards(transform.position, ignoreZ, Time.deltaTime * speed);  
        }
    }
}
