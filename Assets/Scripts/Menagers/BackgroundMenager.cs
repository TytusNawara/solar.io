using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMenager : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public GameObject cameraThatIsFollowedByBackground;

    private float offsetBetweenBackgounds = 16;
    private int tilesSuplyBeforeMiddleOne = 2;
    private GameObject[,] allBackgroundTiles;

    void Start()
    {
        int arraySize = 2 * tilesSuplyBeforeMiddleOne + 1;
        allBackgroundTiles = new GameObject[arraySize, arraySize];
        for (int x = -tilesSuplyBeforeMiddleOne; x <= tilesSuplyBeforeMiddleOne; x++)
        {
            for (int y = -tilesSuplyBeforeMiddleOne; y <= tilesSuplyBeforeMiddleOne; y++)
            {
                allBackgroundTiles[x + tilesSuplyBeforeMiddleOne, y + tilesSuplyBeforeMiddleOne] = Instantiate(backgroundPrefab,
                    new Vector2(x * offsetBetweenBackgounds, y * offsetBetweenBackgounds),
                    Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 positionNearCameraInGrid = cameraThatIsFollowedByBackground.transform.position;
        positionNearCameraInGrid.x = positionNearCameraInGrid.x -
                                     positionNearCameraInGrid.x % offsetBetweenBackgounds;
        positionNearCameraInGrid.y = positionNearCameraInGrid.y -
                                     positionNearCameraInGrid.y % offsetBetweenBackgounds;
        transform.position = positionNearCameraInGrid;
    }
}


