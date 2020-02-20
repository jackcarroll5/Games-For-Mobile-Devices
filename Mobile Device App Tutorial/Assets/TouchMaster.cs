using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TouchMaster : MonoBehaviour
{
	Camera cam;


	[SerializeField]
	Controllable selectedItem;
	float timer = 0.5f;
	bool has_hit_something = false;
	public bool isTouched = false;
	bool timeEnd = false;
	private float startTouchTime = 0f;
	private readonly float thresholdTapTime = 0.1f;

	bool isDragging = false;
	private Transform objToDrag;
	private float distance;
	Vector3 offset;

	public float startDist;

	public Vector3 initScale;

	Vector3 point1;
    Vector3 point2;
	public float angleX = 0.0f;
	public float angleY = 0.0f;
	float xAngleTemp = 0.0f;
	float yAngleTemp = 0.0f;

	private float speedAccelerometer = 0.5f;

	//private float rotVelocityX = 0.0f;
	//private float rotVelocityY = 0.0f;
	public bool isRotating = false;

	//[SerializeField]
	//float rotationSpeed = 0.1f;
	public Quaternion localRot;

	[SerializeField]
	float movingSpeed = 0.1f;

	Quaternion start_orientation;

	public float zoomPersPSpeed = 0.05f;
	public float zoomOrthoSpeed = 0.05f;

	public Vector3 startPosObject;


	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		angleX = 0.0f;
		angleY = 0.0f;

		cam.transform.rotation = Quaternion.Euler(angleX, angleY, 0.0f);

		localRot = selectedItem.transform.rotation;

		Screen.orientation = ScreenOrientation.Portrait;

	    startPosObject = selectedItem.transform.position;

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

	public void accelerometerSelectedObject()
	{
		float speedAcc = 0.8f;

		float presentSpeed = Time.deltaTime * speedAcc;

		localRot.x = Input.acceleration.y * presentSpeed;
		localRot.y = Input.acceleration.x * presentSpeed;

		selectedItem.transform.rotation = localRot;
	}


	// Update is called once per frame
	void Update()
	{
		//cam.transform.Rotate(Input.acceleration.x, 0, 0);
		if (Input.touchCount > 0)
		{
			/*if (isTouched)
			{
				timer -= Time.deltaTime;
				print("Current Time: " + timer);
			}*/

			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				/*if(timer < 0)
				{*/
				if(timeEnd == false)
				{			  
					isTouched = true;
					startTouchTime = Time.deltaTime;
					timer = 0.5f;
					isRotating = false;

					start_orientation = selectedItem.transform.rotation;

					//Rotation through axis - Camera.main.transform.forward
					//Sin,Cos,Tan
			
					//Inverse Tan multiplied by degree of values

					Vector3 objPos = Input.GetTouch(0).position;
			
					initScale = selectedItem.transform.localScale;
					point1 = Input.GetTouch(0).position;

					xAngleTemp = angleX;
					yAngleTemp = angleY;

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

						//accelerometerSelectedObject();
						

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
				//}
			}

		if (Input.GetTouch(0).phase == TouchPhase.Moved && isDragging)
			{
				if (selectedItem)
				{ 
					Vector2 touchDeltaPos = Input.GetTouch(0).position;
					double halfTheScreen = Screen.width / 2.0;

					Vector3 touchingGameObjectPos = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, distance));
					objToDrag.position = touchingGameObjectPos + offset;

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
				}
					//Camera Rotation with no object selected
				
				/*Debug.Log("Movement from the touch is happening \nThe touch delta position while moving is " + touchingGameObjectPos + "\nThe selected game object's position is " + selectedItem.transform.position);*/

				if (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved))
				{
					Debug.Log("Two touches are made");
					if(selectedItem)
					{
						Rotation_Movement();
						Scale_Object();
					}
							
				}
			}

			else if (Input.GetTouch(0).phase == TouchPhase.Moved && !isDragging)
			{
				if(Input.touchCount == 1)
				{
					Vector2 touchDragPos = Input.GetTouch(0).deltaPosition;
					cam.transform.Translate(-touchDragPos.x * movingSpeed, -touchDragPos.y * movingSpeed, 0);
				}

				if (Input.touchCount == 2)
				{
					Camera_Zoom();

					point2 = Input.GetTouch(0).position;

					angleX = xAngleTemp + (point2.x - point1.x) * 180 / Screen.width;

					angleY = yAngleTemp + (point2.y - point1.y) * 90 / Screen.height;
					cam.transform.rotation = Quaternion.Euler(angleY, angleX, 0.0f);
				}
			}

				if (Input.GetTouch(0).phase == TouchPhase.Stationary)
				{
					Debug.Log("The touching is stationary");
				}

				if (Input.GetTouch(0).phase == TouchPhase.Ended && !isDragging)
				{
					isTouched = false;
					isDragging = false;
					timer = 0.5f;

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

	private void Camera_Zoom()
	{
		Touch toucher1 = Input.GetTouch(0);
		Touch toucher2 = Input.GetTouch(1);

		Vector2 touchOneLastPos = toucher1.position - toucher1.deltaPosition;
		Vector2 touchTwoLastPos = toucher2.position - toucher2.deltaPosition;

		float lastTouchMagDelta = (touchOneLastPos - touchTwoLastPos).magnitude;
		float touchMagDelta = (toucher1.position - toucher2.position).magnitude;

		float deltaMagDiff = touchMagDelta - lastTouchMagDelta;

		if(cam.orthographic)
		{
			cam.orthographicSize += deltaMagDiff * zoomOrthoSpeed;
			cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
		}
		else
		{
			cam.fieldOfView += deltaMagDiff * zoomPersPSpeed;
			cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 0.1f, 180.0f);
		}
	}

	private void Rotation_Movement()
	{
		float currentAngle = Mathf.Atan2(Input.GetTouch(1).position.y - Input.GetTouch(0).position.y, Input.GetTouch(1).position.x - Input.GetTouch(0).position.x);

		float start_angle = Mathf.Atan2(Input.GetTouch(1).position.y - Input.GetTouch(0).position.y, Input.GetTouch(1).position.x - Input.GetTouch(0).position.x);

		float angle = (currentAngle - start_angle) * Mathf.Rad2Deg;

		angle = currentAngle * Mathf.Rad2Deg;

		selectedItem.transform.rotation = Quaternion.AngleAxis(angle, cam.transform.forward) * start_orientation;

		isRotating = true;
	}

	public void Scale_Object()
	{
		Touch touch1 = Input.GetTouch(0);
		Touch touch2 = Input.GetTouch(1);

		Vector2 touchOnePrevPos = touch1.position - touch1.deltaPosition;
		Vector2 touchTwoPrevPos = touch2.position - touch2.deltaPosition;

		float previousTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
		float touchingDeltaMag = (touch1.position - touch2.position).magnitude;

		float deltaMagDiff = touchingDeltaMag - previousTouchDeltaMag;

		Vector3 scaleNew = selectedItem.transform.localScale - new Vector3(deltaMagDiff, deltaMagDiff, deltaMagDiff);

		selectedItem.transform.localScale = scaleNew;

	}

}