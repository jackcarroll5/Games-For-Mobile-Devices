using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMaster : MonoBehaviour
{
    Camera cam;
    public Controllable selectedItem;
    float timer = 0f;
    GameObject touch;
    bool movement = false;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    public void select_item(Controllable new_item)
    {
        if(selectedItem)
        {
            selectedItem.Deselect();  
            selectedItem = new_item;
            selectedItem.isSelected();
            Debug.Log("One of the game objects has been selected and tapped");
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
       
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Touch count is : " + Input.touchCount + "\nTime elapsed since touch is : " + timer);

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, 20 * ray.direction);
 
                RaycastHit info_on_hit;
                movement = Physics.Raycast(ray, out info_on_hit);

                if (movement)
                {
                    Controllable gameObject = info_on_hit.transform.GetComponent<Controllable>();

                     if(selectedItem)
                        { 
                            gameObject.Moving_up();
                        }
                     else
                        {
                        select_item(gameObject);
                        gameObject.Deselect();
                        gameObject.isSelected();
                        Debug.Log("The gameobject is selected : " + gameObject.chosen);
                        }
                }          
            }

            if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPos = Input.GetTouch(0).position;
                double halfTheScreen = Screen.width / 2.0;

                if (touchDeltaPos.x < halfTheScreen)
                {
                    gameObject.transform.Translate(Vector3.left * 5 * Time.deltaTime);
                }
                else if (touchDeltaPos.x > halfTheScreen)
                {
                    gameObject.transform.Translate(Vector3.right * 5 * Time.deltaTime);
                }

                Debug.Log("Movement from the touch is happening \nThe touch delta position while moving is " + touchDeltaPos + "\nThe selected game object's position is " + gameObject.transform.position);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                Debug.Log("The touching is stationary");
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                movement = false;
                Debug.Log("The touching has ended");
            }
        }
    }
}
