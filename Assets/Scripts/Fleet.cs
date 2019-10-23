using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public GameObject shipPrefab;
    List<GameObject> ships = new List<GameObject>();
    Vector2 range = new Vector2(5, 5);
    int shipsAtTheStart = 20;
    Vector2 fleetFacingDirection = Vector2.up;

    public void faceFleetInDirection(Vector2 direction) {
        fleetFacingDirection = direction;
    }

    void updateAllShipsRotation()
    {
        foreach (var ship in ships)
        {
            ship.GetComponent<Ship>().faceShipInDirection(fleetFacingDirection);
        }
    }

    void Start()
    {
        for (int i = 0; i < shipsAtTheStart; i++)
            ships.Add(Instantiate(shipPrefab, 
                new Vector2(Random.RandomRange(-range.x, range.x), Random.RandomRange(-range.y, range.y)),
                Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        updateAllShipsRotation();
        foreach (var ship in ships)
        {
            Vector2 normalizeDirectionToCenter = (transform.position - ship.transform.position).normalized;
            Debug.DrawRay(ship.transform.position, normalizeDirectionToCenter, Color.red);
            ship.GetComponent<Rigidbody2D>().velocity = normalizeDirectionToCenter* 1;
        }
    }
}
