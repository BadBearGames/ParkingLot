using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Pathfinding2D 
{
	public float speed = 20F;				//how fast we move
	private bool search = true;				//used to see if we can search
	public uint searchPerSecond = 5;		//how often we search (save resources by not searching every tick)
	private float tempDistance = 0F;		//our distance away from the target (saved to save resources)

	//Movement Enums
	public enum movementTypes {directional,line,seekNearestPlayer,seekPosition,patrol,noMovement};
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
	private List<GameObject> playerList = new List<GameObject>(); 
	private Transform seekTarget;
	public bool useSearchDistance = true;	//whether we are going to use a search distance or not
	public float searchDistance = 20F;		//how far we can see around us to search

	//Patrol Movement Variables
	//Enemy will patrol on a set path between two or more points
	public Vector2 waypoints;
	public enum patrolTypes {backAndForth,lastToFirst};	//back and forth: A->B->C->B->A (repeat); Last to First: A->B->C->A (repeat)
	public patrolTypes patrolType;

	void Start () 
    {
		//populate player list
		playerList.AddRange(GameObject.FindGameObjectsWithTag("Player"));

		//set the target we are seeking
		if(playerList != null){
			seekTarget = playerList[0].transform;
		}

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
				FindPath(transform.position, new Vector3(transform.position.x+lineMovement.x, transform.position.y+lineMovement.y, 0));
				break;

			case movementTypes.patrol:
				//Patrol movement
				//We move between set waypoints
				break;

			case movementTypes.seekNearestPlayer:
				//Seek Nearest Player Movement
				//We seek the nearest player and move towards it

				//Make sure we have a list of targets
				if (GameManager.Instance.Objects[ObjectType.Human].Count > 0){
					//Start the time
					StartCoroutine(SearchTimer());
					tempDistance = Mathf.Infinity;

					//if playerList has only one player, it's our target
					if(GameManager.Instance.Objects[ObjectType.Human].Count == 1){
						seekTarget = GameManager.Instance.Objects[ObjectType.Human][0].transform;
						tempDistance = Vector3.Distance(transform.position, seekTarget.position);
					} 
					//if playerList has more than one player see which is the closest
					else {
						//change seektarget to the nearest player
						foreach(Pathfinding2D player in GameManager.Instance.Objects[ObjectType.Human])
						{
							//check if this player is closer than our previous target
							if(Vector3.Distance(transform.position, player.gameObject.transform.position) < tempDistance){
								//if we are closer, seek this target now
								seekTarget = player.gameObject.transform;
								tempDistance = Vector3.Distance(transform.position, seekTarget.position);
							}
						}
					}

					//Now check the distance to the player, if it is within the distance it will search for a new path
					if (tempDistance < searchDistance || !useSearchDistance)
					{
						FindPath(transform.position, seekTarget.position);
					}
				} else{
					Debug.Log("No gameobjects tagged with Player.");
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

	/// <summary>
	/// Kill that player
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)
	{
		other.gameObject.GetComponent<Player>().Die();
	}
}
