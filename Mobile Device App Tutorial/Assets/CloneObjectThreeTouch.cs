using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneObjectThreeTouch : MonoBehaviour
{
    public Transform go;
    Camera cam;
    float minZVal;
    float maxZVal;
    float minYVal;
    float maxYVal;
    float minXVal;
    float maxXVal;

    // Start is called before the first frame update
    void Start()
    { 
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 3 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 offset = new Vector3(Random.Range(minXVal, maxXVal),
                Random.Range(minYVal, maxYVal), Random.Range(minZVal, maxZVal));

           Instantiate(go,  transform.position + offset, Quaternion.identity);
        }      
    }
}
