using UnityEngine;
using System.Collections;

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
