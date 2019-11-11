using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBot : BasicBot
{
    float timeSinceLastDirectionChange = 0f;
    float minimalTimeThatNeedToPassBetweenDirectionChanges = 0.7f;
    float probabilityToChangeDirection = 1f;

    float timeSinceBotShoot = 0f;
    private float minTimeBetweenBotShots = 1.1f;

  

    // Update is called once per frame
    protected new void Update()
    {
        rotationSpeed = 400f;
        rotateFleetInDirectionThatBotChooses();
        timeSinceLastDirectionChange += Time.deltaTime;
        timeSinceBotShoot += Time.deltaTime;
        informGameMenagerIfBotDied();
    }

    protected void FixedUpdate()
    {
        if (timeSinceBotShoot > minTimeBetweenBotShots)
        {
            if (Random.Range(0, 100) < (timeSinceBotShoot - minTimeBetweenBotShots))
            {
                fleetThatBotControlls.GetComponent<Fleet>().giveShipsShootOrder();
                timeSinceBotShoot = 0f;
            }

        }

        if (timeSinceLastDirectionChange > minimalTimeThatNeedToPassBetweenDirectionChanges) {
            if (Random.Range(0, 100f) <
                (timeSinceLastDirectionChange - minimalTimeThatNeedToPassBetweenDirectionChanges) *
                probabilityToChangeDirection)
            {
                directionToGoCalculatedByBot = Random.insideUnitCircle.normalized;
                timeSinceLastDirectionChange = 0;
            }
        }
    }
}
