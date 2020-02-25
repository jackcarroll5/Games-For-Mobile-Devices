using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    private float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotateAccelerate = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
        transform.Rotate(rotateAccelerate *  speed * Time.deltaTime);
    }
}
