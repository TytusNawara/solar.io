using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class MediumDifficultyBot : BasicBot
{
    protected float timePassedSinceLastTargetChange = 0f;
    protected float mediumTimeBetweenChangeTarget = 6f;
    protected float timeBetweenNextTargetChange;

    protected float timePassedSinceLastStateChange = 0f;
    protected float mediumTimeBetweenChangeState;

    protected GameObject fleetScript; 

    protected enum State : int {
        MOVE_TOWARDS_TARGET = 0 ,
        ROTATE_AROUND_TARGET = 1,
        WANDER_RANDOMLY = 2 ,
        GO_AWAY_FROM_TARGET = 4
    };

    protected float[] statesProbability = {2, 2, 1, 1};

    protected State currentBotAction = State.WANDER_RANDOMLY;
    protected float chanceToChangeDirectionWhenWandersRandomly;

    protected void wanderRandomly()
    {
        if (Random.Range(0, 100f) <chanceToChangeDirectionWhenWandersRandomly)
        {
            directionToGoCalculatedByBot = Random.insideUnitCircle.normalized;
            
        }
    }

    protected void moveTowardsTarget()
    {
        Vector2 fromSelfToEnemy = targetedFleet.transform.position - transform.position;
        directionToGoCalculatedByBot = fromSelfToEnemy.normalized;
    }

    protected void rotateAroundTarget()
    {
        if(Random.Range(0f, 1f)<0.03f)
            randomizeRotatingDirection();
        Vector2 fromSelfToEnemy = targetedFleet.transform.position - transform.position;
        directionToGoCalculatedByBot = Vector2.Perpendicular(fromSelfToEnemy).normalized
                                       * randomRotationDirection;
    }

    protected void moveAwayFromTarget()
    {
        Vector2 fromSelfToEnemy = targetedFleet.transform.position - transform.position;
        directionToGoCalculatedByBot = -fromSelfToEnemy.normalized;
    }

    protected new void Start()
    {
        mediumTimeBetweenChangeState = Random.Range(4f, 8f);
        chanceToChangeDirectionWhenWandersRandomly = Random.Range(0.7f, 3f);
        instantiateFleet();
        //Debug.Log("Medium Deafaulty Bot was created");
        targetClosestFleet();
    }

    void calculateTimeBetweenNextTargetChange()
    {
        timeBetweenNextTargetChange = mediumTimeBetweenChangeTarget + Random.Range(-2f, 4f);
    }

    protected void targetClosestFleet()
    {
        Vector2[] fleetPositions = GameMenager.getAllFleetsLocations();
       
        int closestFleetIndex = -1;
        float closestFleedDistance = float.MaxValue;
        
        for (int i = 0; i < fleetPositions.Length; i++)
        {
            float disctanceBetweenFleets = Vector2.Distance(transform.position, fleetPositions[i]);
            //secont argument prevents targeting itself
            if (disctanceBetweenFleets < closestFleedDistance && disctanceBetweenFleets > 0.001)
            {
                closestFleetIndex = i;
                closestFleedDistance = disctanceBetweenFleets;
            }
        }

        targetedFleet = GameMenager.getFleetWithIndex(closestFleetIndex);
        calculateTimeBetweenNextTargetChange();
        timePassedSinceLastTargetChange = 0f;
        randomizeRotatingDirection();
    }

    // Update is called once per frame
    new void Update()
    {
        rotationSpeed = 400f;
        rotateFleetInDirectionThatBotChooses();
        timePassedSinceLastTargetChange += Time.deltaTime;
        timePassedSinceLastStateChange += Time.deltaTime;

        if (timePassedSinceLastTargetChange > timeBetweenNextTargetChange)
        {
            targetClosestFleet();
        }

        if (timePassedSinceLastStateChange > mediumTimeBetweenChangeState)
        {
            changeStateToRandomOne();
            timePassedSinceLastStateChange = 0f;
        }

        Debug.DrawLine(transform.position, targetedFleet.transform.position, Color.blue);
    }

    protected void changeStateToRandomOne()
    {
        float sum = 0f;
        for (int i = 0; i < statesProbability.Length; i++)
            sum += statesProbability[i];
        
        float result = Random.Range(0f, sum);
        for (int i = 0; i < statesProbability.Length; i++)
        {
            result -= statesProbability[i];
            if (result <= 0f)
            {
                currentBotAction = (State)i;
//                Debug.Log(currentBotAction);
                return;
            }
        }
    }

    void shootIfTargetInFrontOfBot()
    {
        float angel = Vector2.Angle(directionAplayedToFleet, targetedFleet.transform.position - transform.position);
        if (Mathf.Abs(angel) < 2f)
            fleetThatBotControlls.GetComponent<Fleet>().giveShipsShootOrder();
       // Debug.Log(Mathf.Abs(angel));

    }

    protected void FixedUpdate()
    {
        switch (currentBotAction)
        {
            case State.MOVE_TOWARDS_TARGET:
                moveTowardsTarget();
                break;
            case State.ROTATE_AROUND_TARGET:
                rotateAroundTarget();
                break;
            case State.WANDER_RANDOMLY:
                wanderRandomly();
                break;
            case State.GO_AWAY_FROM_TARGET:
                moveAwayFromTarget();
                break;
        }
        shootIfTargetInFrontOfBot();
    }

    protected new void rotateFleetInDirectionThatBotChooses()
    {

        if (Vector2.Angle(directionAplayedToFleet, directionToGoCalculatedByBot) > 3)
            directionAplayedToFleet = directionAplayedToFleet.Rotate(rotationSpeed * Time.deltaTime * randomRotationDirection);
        else
        {
            directionAplayedToFleet = directionToGoCalculatedByBot;
        }

        fleetThatBotControlls.GetComponent<Fleet>().faceFleetInDirection(directionAplayedToFleet);
    }
}
