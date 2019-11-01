using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBot : MonoBehaviour
{
    public GameObject fleetPrefab;
    protected GameObject targetedFleet;
    protected GameObject fleetThatBotControlls;
    protected Vector2 directionToGoCalculatedByBot;
    
    protected void FindFleetToTarget() {
        Vector2[] fleetTransforms = GameMenager.getAllFleetsLocations();
        for (int i = 0; i < fleetTransforms.Length; i++)
            Debug.Log(fleetTransforms[i].ToString());
    }

    protected void Start()
    {
        
        instantiateFleet();
    }

    protected void instantiateFleet() {
        fleetThatBotControlls = Instantiate(fleetPrefab, transform.position, Quaternion.identity);
        transform.parent = fleetThatBotControlls.transform;
        for (int i = 0; i < 5; i++)
        {
            fleetThatBotControlls.GetComponent<Fleet>().informFleetThatEnemyShipIsDown();//debug eut this
        }
        directionToGoCalculatedByBot = Random.insideUnitCircle.normalized;//eut
    }

    protected void rotateFleetInDirectionThatBotChooses() {
        fleetThatBotControlls.GetComponent<Fleet>().faceFleetInDirection(directionToGoCalculatedByBot);
    }

    void Update()
    {
        rotateFleetInDirectionThatBotChooses();
    }
}
