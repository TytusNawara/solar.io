using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBot : BasicBot
{
    float timeSinceLastDirectionChange = 0f;
    float minimalTimeThatNeedToPassBetweenDirectionChanges = 0.7f;
    float probabilityToChangeDirection = 1f;

    // Update is called once per frame
    void Update()
    {
        rotateFleetInDirectionThatBotChooses();
        timeSinceLastDirectionChange += Time.deltaTime;
    }

    protected void FixedUpdate()
    {
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
