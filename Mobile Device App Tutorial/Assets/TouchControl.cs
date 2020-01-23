using UnityEngine;

public class TouchControl : MonoBehaviour
{
    GameObject touch;
    bool movement = false;
    float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit = new RaycastHit();

                movement = Physics.Raycast(ray, out hit);

                if (movement)
                {
                    touch = hit.transform.gameObject;
                    Debug.Log("Touch spotted on " + touch.name);
                }
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPos = Input.GetTouch(0).deltaPosition;
                Vector3 touchPos = new Vector3();

                touchPos.Set(touchDeltaPos.x, transform.position.y, touchDeltaPos.y);

                transform.position = Vector3.Lerp(transform.position, touchPos, Time.deltaTime * speed);

                Debug.Log(touch.name + " is moving");
            }


            if ((Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase ==
           TouchPhase.Canceled) && touch != null)
            {
                movement = false;
                Debug.Log("Touch is released from " + touch.name);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                Debug.Log("Touch is left stationary on " + touch.name);
            }
        }
    }
}
