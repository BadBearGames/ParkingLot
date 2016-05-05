using UnityEngine;
using System.Collections;

/// <summary>
/// Important code in this class has been merged into Pathfinding2D. Use that class for objects.
/// Extend that class for any object. If it does not move, simply don't move it in code.
/// </summary>
public class ObjectBehavior : MonoBehaviour {
    
	public float speed;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //transform.position += transform.forward * speed;

        //transform.Rotate(0, 90, 0);
    }

    public void tick () {
        transform.position += transform.forward * speed;
        Debug.Log("tick");
    }

    void OnCollisionEnter(Collision collision){
        Debug.Log("HIT!");
        if (collision.gameObject.tag == "arrow") {
            transform.Rotate(0,90,0);
        }
    }
}
