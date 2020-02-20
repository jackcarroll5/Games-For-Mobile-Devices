using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void accelerometerSelectedObjectAlt()
    {
        transform.Rotate(-Input.acceleration.x * 0.03f, -Input.acceleration.y * 0.03f, -Input.acceleration.z * 0.05f);
    }
}
