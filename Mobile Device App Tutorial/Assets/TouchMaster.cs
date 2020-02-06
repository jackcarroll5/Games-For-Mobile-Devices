using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TouchMaster : MonoBehaviour
{
	Camera cam;

	[SerializeField]
	Controllable selectedItem;
	float timer = 10f;
	bool has_hit_something = false;
	bool isTouched = false;
	bool timeEnd = false;
	private float startTouchTime = 0f;
	private float thresholdTapTime = 0.1f;
	bool isDragging = false;
	private Transform objToDrag;
	private float distance;
	Vector3 offset;

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
	}

	/*public void select_item(Controllable new_item)
	{
		if (selectedItem)
		{
			selectedItem.Deselect();
			selectedItem = new_item;
			selectedItem.isSelected();
			Debug.Log("One of the game objects has been selected and tapped");
		}

	}*/

	//Rotation through axis - Camera.main.transform.forward
	//Sin,Cos,Tan
	//float start_angle - Start Phase
	//Quaternion start_rotation = transform.rotation
	//float new_angle - Moved Phase
	//float diff_angle = new_angle - start_angle (Moved phase)
	//transform.rotation = Quaternion.Angle(rotation_axis,diff_angle) * start_rotation

	// Update is called once per frame
	void Update()
	{
		if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				if(timeEnd == false)
				{			  
					isTouched = true;
					startTouchTime = Time.deltaTime;
					Vector3 objPos = Input.GetTouch(0).position;

					if (Input.touchCount != 1)
					{
						isDragging = false;
						return;
					}

					Debug.Log("Touch count is : " + Input.touchCount + "\nStart Touch Time : " + startTouchTime);

					Ray ray = cam.ScreenPointToRay(Input.mousePosition);
					Debug.DrawRay(ray.origin, 20 * ray.direction);

					RaycastHit info_on_hit;

					has_hit_something = Physics.Raycast(ray, out info_on_hit);

				if (has_hit_something)
				{
					Controllable hit_object = info_on_hit.transform.GetComponent<Controllable>();

					if (hit_object)
					{
						// Select this object
						if (selectedItem)
						{
							selectedItem.Deselect();
							selectedItem = hit_object;
							selectedItem.IsSelected();
						}
						else
						{
							selectedItem.Deselect();
							selectedItem = null;
							Debug.Log("Item is deselected");
						}
					}
					else
					{
						if (!selectedItem)
						{
							selectedItem = null;
							Debug.Log("Nothing is selected!");
						}
					}

						objToDrag = hit_object.transform;
						distance = hit_object.transform.position.z - cam.transform.position.z;
						Vector3 vector = new Vector3(objPos.x, objPos.y, distance);
						vector = cam.ScreenToWorldPoint(vector);
						offset = objToDrag.position - vector;
						isDragging = true;
				  }
				else
					{
						selectedItem.Deselect();
						Debug.Log("Nothing selected! Empty screen space selected");
					}
				}
			}

			if (Input.GetTouch(0).phase == TouchPhase.Moved && isDragging)
				{
					Vector2 touchDeltaPos = Input.GetTouch(0).position;
					double halfTheScreen = Screen.width / 2.0;

				Vector3 touchingGameObjectPos = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, distance));
				objToDrag.position = touchingGameObjectPos
					 + offset;

					if (touchDeltaPos.x < halfTheScreen)
					{
						gameObject.transform.Translate(Vector3.left * 5 * Time.deltaTime);
					}
					else if (touchDeltaPos.x > halfTheScreen)
					{
						gameObject.transform.Translate(Vector3.right * 5 * Time.deltaTime);
					}

				selectedItem.transform.position = Vector3.Lerp(selectedItem.transform.position,touchingGameObjectPos,Time.deltaTime);

					Debug.Log("Movement from the touch is happening \nThe touch delta position while moving is " + touchingGameObjectPos + "\nThe selected game object's position is " + selectedItem.transform.position);
				}

				if (Input.GetTouch(0).phase == TouchPhase.Stationary)
				{
					Debug.Log("The touching is stationary");
				}

				if (Input.GetTouch(0).phase == TouchPhase.Ended && isDragging)
				{
					/*if (isTouched)
					{
						timer -= Time.deltaTime;
						print("Current Time: " + timer);

						if (timer <= 0)
						{
							timeEnd = true;
							print("Time is up");
						}
					}*/
					isTouched = false;
					isDragging = false;
					timer = 0.0f;

					float timeTap = Time.deltaTime - startTouchTime;
					Debug.Log("Time for end tap: " + timeTap + "\nDeltaTime: " + Time.deltaTime + "\nTime Threshold: " + thresholdTapTime + "\nStarting Time: " + startTouchTime);

					if(timeTap <= thresholdTapTime)
					{
					//Indicates Tap at start of timer and end of timer when time is up
					  Debug.Log("Tap officially determined and spotted \nThe time is up: " + timeEnd);
					}
				}
		}
	}
}