using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMaster : MonoBehaviour
{
    Camera cam;
    float speed = 1.0f;
    Controllable selectedItem;
    bool movementFlag;
    private Vector3 endPt;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    

    // Update is called once per frame
    void Update()
    {
        Touch touch;
       
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, 20 * ray.direction);
 

                RaycastHit info_on_hit;           
                if (Physics.Raycast(ray, out info_on_hit))
                {
                    Controllable gameObject = info_on_hit.transform.GetComponent<Controllable>();

                     if(selectedItem)
                    { 
                     if(gameObject != selectedItem)
                        {

                            gameObject.isSelected();
                            selectedItem.Deselect();

                    Debug.Log("One of the game objects has been selected and tapped");

                            gameObject.Moving_up();
                        }
                     else
                        {
                            gameObject.isSelected();
                        }
                    }
                }          

            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
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

                Debug.Log("Selected Game Object is moving \nThe touch delta position is " + touchDeltaPos + "\nThe selected game object's position is " + gameObject.transform.position.ToString());
            }
        }
    }
}
