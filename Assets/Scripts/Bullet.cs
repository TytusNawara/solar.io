using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 8f;
    float bulletLifeTime = 5f;
    public int ID = -3;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = ((Vector2)transform.up)* speed;
        Destroy(gameObject, bulletLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        /*timePassedSinceBulletShooted += Time.deltaTime;
        if (timePassedSinceBulletShooted >= timePassedSinceBulletShooted) {
            Debug.Log("bullet destroyed");
            DestroyObject(gameObject);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ship ship = collision.GetComponent<Ship>();
        if (ship != null)
        {
            if (ship.getID() != ID)
            {
                Debug.Log(collision.name);
                ship.destroyShip();
                Destroy(gameObject);
            }
            else {
                Debug.Log("id jest takie samo i wynosi " + ID);
            }
        }
        else {
            Debug.Log("ship to null");
        }
            

        
        
    }
}
