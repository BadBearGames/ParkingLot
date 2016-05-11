using System.Collections;
using UnityEngine;

class DragObject : MonoBehaviour
{

	private bool dragging = false;
	private float distance;
	public Plane plane; 

	void OnMouseDown()
	{
		dragging = true;
	}

	void OnMouseUp()
	{
		dragging = false;
	}

	void Update()
	{
		if (dragging && GameManager.Instance.CurrentState == GameState.HumanTurn)
		{
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Vector3 rayPoint = ray.GetPoint(distance);
			distance = Vector3.Distance(transform.position, Camera.main.transform.position);

			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			transform.position = ray.GetPoint(distance);

			//transform.position = rayPoint;
		}
		Vector3 pos = transform.position; 
		pos.z = -1; 
		transform.position= pos; 
	}

}
