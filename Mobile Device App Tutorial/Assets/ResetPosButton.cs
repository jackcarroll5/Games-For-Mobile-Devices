using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosButton : MonoBehaviour
{
    Vector3 startingPos;
    Quaternion startingRot;
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        startingRot = go.transform.rotation;
        startingPos = go.transform.position;
    }

    public void ResetPosition()
    {
        go.transform.rotation = startingRot;
        go.transform.position = startingPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
