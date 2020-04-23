using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        accelerometerSelectedObjectAlt();
    }

    public void accelerometerSelectedObjectAlt()
    {
        transform.Translate(-Input.acceleration.x * 0.05f, -Input.acceleration.y * 0.05f, -Input.acceleration.z * 0.07f);
    }
}
