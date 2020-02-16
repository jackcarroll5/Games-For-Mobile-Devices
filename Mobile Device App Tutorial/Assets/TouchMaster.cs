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
	private readonly float thresholdTapTime = 0.1f;

	bool isDragging = false;
	private Transform objToDrag;
	private float distance;
	Vector3 offset;

	float startDist;

	Vector3 initScale;


	private float rotVelocityX = 0.0f;
	private float rotVelocityY = 0.0f;
	bool isRotating = false;

	[SerializeField]
	float rotationSpeed = 0.1f;

	[SerializeField]
	float movingSpeed = 0.1f;

	Quaternion start_orientation;


	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;

		Screen.orientation = ScreenOrientation.Portrait;

		Touch t1 = Input.GetTouch(0);
		Touch t2 = Input.GetTouch(0);

		float start_angle = Mathf.Atan2(t2.position.y - t1.position.y, t2.position.x - t1.position.x);

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


	// Update is called once per frame
	void Update()
	{
		if (Input.touchCount > 0)
		{
			timer += Time.deltaTime;

			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				if(timeEnd == false)
				{			  
					isTouched = true;
					startTouchTime = Time.deltaTime;
					timer = 0f;
					isRotating = false;

					start_orientation = selectedItem.transform.rotation;


					//Rotation through axis - Camera.main.transform.forward
					//Sin,Cos,Tan
					
					
					//Inverse Tan multiplied by degree of values


					Vector3 objPos = Input.GetTouch(0).position;

					initScale = selectedItem.transform.localScale;


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

				//Alt to Drag - Distance to Object
				/*Vector3 distObj = Vector3.Distance(selectedItem.transform.position, cam.transform.position);*/

					if (touchDeltaPos.x < halfTheScreen)
					{
						gameObject.transform.Translate(Vector3.left * 5 * Time.deltaTime);
					}
					else if (touchDeltaPos.x > halfTheScreen)
					{
						gameObject.transform.Translate(Vector3.right * 5 * Time.deltaTime);
					}

				selectedItem.transform.position = Vector3.Lerp(selectedItem.transform.position,touchingGameObjectPos,Time.deltaTime);

					/*Debug.Log("Movement from the touch is happening \nThe touch delta position while moving is " + touchingGameObjectPos + "\nThe selected game object's position is " + selectedItem.transform.position);*/

				if(Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved))
				{
					Rotation_Movement();
					Scale_Object();
							
				}	
			}

				if (Input.GetTouch(0).phase == TouchPhase.Stationary)
				{
					Debug.Log("The touching is stationary");
				}

				if (Input.GetTouch(0).phase == TouchPhase.Ended && !isDragging)
				{
					/*if (isTouched)
					{
						timer -= Time.deltaTime;
						print("Current Time: " + timer);*/

						if (timer < 0 )
						{
							timeEnd = true;
							print("Time is up");
						}
					//}
					isTouched = false;
					isDragging = false;
					timer = 0f;

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

	private void Rotation_Movement()
	{
		float currentAngle = Mathf.Atan2(Input.GetTouch(1).position.y - Input.GetTouch(0).position.y, Input.GetTouch(1).position.x - Input.GetTouch(0).position.x);

		float start_angle = Mathf.Atan2(Input.GetTouch(1).position.y - Input.GetTouch(0).position.y, Input.GetTouch(1).position.x - Input.GetTouch(0).position.x);

		float angle = currentAngle - start_angle;

		angle = currentAngle * Mathf.Rad2Deg;

		selectedItem.transform.rotation = Quaternion.AngleAxis(angle, cam.transform.forward) * start_orientation;

		isRotating = true;
	}

	public void Scale_Object()
	{
		Touch touch1 = Input.GetTouch(0);
		Touch touch2 = Input.GetTouch(1);

		// Find the position in the previous frame of each touch.
		Vector2 touchOnePrevPos = touch1.position - touch1.deltaPosition;
		Vector2 touchTwoPrevPos = touch2.position - touch2.deltaPosition;

		// Find the magnitude of the vector (the distance) between the touches in each frame.
		float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
		float touchDeltaMag = (touch1.position - touch2.position).magnitude;

		// Find the difference in the distances between each frame.
		float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

		selectedItem.transform.localScale = new Vector3(deltaMagDiff, deltaMagDiff, deltaMagDiff);


	}

}