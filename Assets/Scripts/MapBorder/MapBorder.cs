using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBorder : MonoBehaviour
{
    // Start is called before the first frame update
    private float r = 60f;
    void Start()
    {
        //var go1 = new GameObject { name = "Circle" };
        
        gameObject.DrawCircle(r, 0.4f);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
