using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public GameObject shipPrefab;
    float speed = 0.1f;
    List<GameObject> ships = new List<GameObject>();
    Vector2 range = new Vector2(5, 5);
    int shipsAtTheStart = 4;
    public bool _debug_FleetCanMove = true;
    Vector2 fleetFacingDirection = Vector2.up;

    

    public void faceFleetInDirection(Vector2 direction) {
        fleetFacingDirection = direction;
    }

    public void giveShipsShootOrder() {
        foreach (var ship in ships) {
            ship.GetComponent<Ship>().shoot();
        }
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
                new Vector2(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y)),
                Quaternion.identity));
        foreach (var ship in ships) {
            ship.transform.parent = transform;
        }
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
            //Debug.DrawRay(ship.transform.position, normalizeDirectionToCenter, Color.red);
            
            Rigidbody2D rb = ship.GetComponent<Rigidbody2D>();
            if ((transform.position - ship.transform.position).magnitude > 0.01f)
                rb.velocity = normalizeDirectionToCenter * 1;
            else
                rb.velocity = Vector2.zero;
           
        }
        if (_debug_FleetCanMove)//eut
            transform.position = (Vector2)transform.position + fleetFacingDirection.normalized * speed;
    }
}
