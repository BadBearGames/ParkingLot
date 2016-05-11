using System.Collections;
using UnityEngine;

class DragObject : MonoBehaviour
{

	private bool dragging = false;
	private float distance;
	public Plane plane; 

	void OnMouseDown()
	{
		//distance = Vector3.Distance(transform.position, Camera.main.transform.position);
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
	/*
	Vector3 ScreenPosToWorldPosByPlane(Vector2 screenPos, Plane plane) {
		Ray ray=Camera.main.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, 1f));
		float distance;
		if(!plane.Raycast(ray, out distance))
			throw new UnityException("did not hit plane", this);
		return ray.GetPoint(distance);
	}*/
}
