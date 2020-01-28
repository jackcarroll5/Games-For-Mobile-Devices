using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    Renderer thisRenderer;
    public bool chosen = false;
    // Start is called before the first frame update
    void Start()
    {
        thisRenderer = GetComponent<Renderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    internal void Moving_up()
    {
        transform.position += Vector3.up;

        this.GetComponent<Renderer>().material.color =
        Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }

    internal void Deselect()
    {
        bool IsSelected = false;

        if (IsSelected)
        {
            chosen = false;
            thisRenderer.material.color = Color.blue;
            return;
        }
    }

    internal void isSelected()
    {
        bool IsSelected = true;

        if(IsSelected)
        {
            chosen = true;
            thisRenderer.material.color = Color.red; 
            return;
        }
    }
}
