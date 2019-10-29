using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 8f;
    float bulletLifeTime = 5f;
    public int ID = -3;
    GameObject fleetThatBulletOrginatesFrom;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = ((Vector2)transform.up) * speed;
        Destroy(gameObject, bulletLifeTime);
    }


    public void setFleetThatBulletOrginatesFrom(GameObject fleet) {
        fleetThatBulletOrginatesFrom = fleet;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ship ship = collision.GetComponent<Ship>();
        if (ship != null)
        {
            if (ship.getID() != ID)
            {
                ship.destroyShip();
                fleetThatBulletOrginatesFrom.GetComponent<Fleet>().informFleetThatEnemyShipIsDown();
                Destroy(gameObject);
            }
            else {
                //Debug.Log("id jest takie samo i wynosi " + ID);
            }
        }
        else {
            //Debug.Log("ship to null");
        }




    }
 
        
}
