using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumDifficultyBot : BasicBot
{
    // Start is called before the first frame update
    protected new void Start()
    {
        instantiateFleet();
        Debug.Log("Medium Deafaulty Bot was created");
    }

    // Update is called once per frame
    new void Update()
    {
        
    }
}
