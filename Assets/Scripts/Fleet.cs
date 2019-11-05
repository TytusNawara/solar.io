﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public GameObject shipPrefab;
    private float speed = 0.02f;
    List<GameObject> ships = new List<GameObject>();
    Vector2 range = new Vector2(-0.25f, 0.25f);
    int shipsAtTheStart = 4;
    public bool _debug_FleetCanMove = true;
    Vector2 fleetFacingDirection = Vector2.up;
    private Sprite shipSprite;

    int ID;

    const float minTimePeriodBetweenShoots = 1f;
    float timePassedSinceLastShoot = minTimePeriodBetweenShoots;

    public void faceFleetInDirection(Vector2 direction) {
        fleetFacingDirection = direction;
    }

    public void giveShipsShootOrder() {
        if (timePassedSinceLastShoot >= minTimePeriodBetweenShoots) {
            foreach (var ship in ships)
            {
                ship.GetComponent<Ship>().shoot();
            }
            timePassedSinceLastShoot = 0f;
        }
        
    }

    public int getID()
    {
        return ID;
    }

    void updateAllShipsRotation()
    {
        foreach (var ship in ships)
        {
            ship.GetComponent<Ship>().faceShipInDirection(fleetFacingDirection);
        }
    }

    public void removeShipFromList(GameObject ship) {
        ships.Remove(ship);
    }

    void setAllShipsToRandomSprite()
    {
        var sMenager = GameObject.Find("SpriteMenager").GetComponent<SpriteMenager>();
        shipSprite = sMenager.getSpriteFromIndex((int) Random
                .Range(0, sMenager.getHowManyShipSpritesAreAvailable()));
        foreach (var ship in ships)
        {
            ship.GetComponent<Ship>().setSprite(shipSprite);
        }
    }

    void Start()
    {
        ID = gameObject.GetInstanceID();//AutoIncrementedKeysGenerator.generateUniqueFleetID();
        for (int i = 0; i < shipsAtTheStart; i++)
            ships.Add(Instantiate(shipPrefab, 
                (Vector2)transform.position + new Vector2(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y)),
                Quaternion.identity));
        foreach (var ship in ships) {
            ship.transform.parent = transform;
        }
        GameMenager.addFleetToGameMenager(gameObject);
        setAllShipsToRandomSprite();
    }
 
    void Update()
    {
        timePassedSinceLastShoot += Time.deltaTime;
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

    public void informFleetThatEnemyShipIsDown() {
        addNewShipToFleet();
    }

    void addNewShipToFleet()
    {
        GameObject toAdd = Instantiate(shipPrefab,
            (Vector2) transform.position +
            new Vector2(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y)),
            Quaternion.identity, transform);
        toAdd.GetComponent<Ship>().setSprite(shipSprite);
        ships.Add(toAdd);
    }
}
