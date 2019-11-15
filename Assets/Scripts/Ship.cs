using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Vector2 facingDirectionNormalized;
    public GameObject bulletPrefab;
    private float deathDistance = GameMenager.getMapRadius();
    private float distanceFromPlayerThatShipIsAllowedToShoot = 15f;
    int fleetID = -2;

    public void shoot() {
        if (Vector2.Distance(transform.position, GameMenager.getPlayerPosition()) > distanceFromPlayerThatShipIsAllowedToShoot)
            return;
        float distanceFromBullet = 0.1f;
        GameObject bullet = Instantiate(bulletPrefab,
            (Vector2)transform.position + facingDirectionNormalized * distanceFromBullet,
            transform.rotation);
        bullet.GetComponent<Bullet>().ID = fleetID;
        bullet.GetComponent<Bullet>().setFleetThatBulletOrginatesFrom(transform.parent.gameObject);
        
    }

    public int getID()
    {
        return fleetID;
    }

    public void setSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }



    void Start()
    {
        fleetID = transform.parent.gameObject.GetComponent<Fleet>().getID();
        facingDirectionNormalized = new Vector2(1, 0);
    }

    void Update()
    {
      
        float angle = Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), facingDirectionNormalized);
        if (facingDirectionNormalized.x < 0.0f)
        {
            angle = -angle;
            angle = angle + 360;
        }
        transform.rotation = Quaternion.Euler(Vector3.forward * (-angle));//TODO move to fixed update

        if(transform.position.magnitude > deathDistance)
            destroyShip();

    }

    public void faceShipInDirection(Vector2 direction) {
        facingDirectionNormalized = direction.normalized;
    }

    public void destroyShip() {
        transform.parent.gameObject.GetComponent<Fleet>().removeShipFromList(gameObject);
        Destroy(gameObject);
    }
}
