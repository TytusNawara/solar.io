using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBot : MonoBehaviour
{
    public GameObject fleetPrefab;
    protected GameObject targetedFleet;
    protected GameObject fleetThatBotControlls;

    protected Vector2 directionToGoCalculatedByBot;
    //preventing snaping
    protected Vector2 directionAplayedToFleet;
    protected float rotationSpeed = 200f;
    protected float randomRotationDirection = 1f;

    protected void FindFleetToTarget() {
        Vector2[] fleetTransforms = GameMenager.getAllFleetsLocations();
        for (int i = 0; i < fleetTransforms.Length; i++)
            Debug.Log(fleetTransforms[i].ToString());
    }

    protected void Start()
    {
        configureThisBotSpecificValues();
        instantiateFleet();
    }

    protected void configureThisBotSpecificValues()
    {

    }

    protected void instantiateFleet() {
        fleetThatBotControlls = Instantiate(fleetPrefab, transform.position, Quaternion.identity);
        transform.parent = fleetThatBotControlls.transform;
        for (int i = 0; i < 5; i++)
        {
            fleetThatBotControlls.GetComponent<Fleet>().informFleetThatEnemyShipIsDown();//debug eut this
        }
        directionToGoCalculatedByBot = Random.insideUnitCircle.normalized;//eut
        directionAplayedToFleet = directionToGoCalculatedByBot;
    }

    protected void randomizeRotatingDirection() {
        if (Random.Range(0, 1f) > 0.5f)
            randomRotationDirection *= -1;
    }

    protected void rotateFleetInDirectionThatBotChooses() {
        //if (Vector2.Angle(directionToGoCalculatedByBot, directionAplayedToFleet) > 0.5f)
        //{
        //    directionAplayedToFleet = directionAplayedToFleet.Rotate(rotationSpeed * Time.deltaTime * randomRotationDirection);
        //}
        //else {
        //    randomizeRotatingDirection();
        //    directionAplayedToFleet = directionToGoCalculatedByBot;
        //}
        if(Vector2.Angle(directionAplayedToFleet, directionToGoCalculatedByBot) > 3)
            directionAplayedToFleet = directionAplayedToFleet.Rotate(rotationSpeed * Time.deltaTime * randomRotationDirection);
        else
        {
            directionAplayedToFleet = directionToGoCalculatedByBot;
            randomizeRotatingDirection();
        }

        fleetThatBotControlls.GetComponent<Fleet>().faceFleetInDirection(directionAplayedToFleet);

        
    }

    protected void Update()
    {
        rotateFleetInDirectionThatBotChooses();
    }
}
