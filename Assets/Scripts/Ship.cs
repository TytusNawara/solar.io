using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Vector2 facingDirectionNormalized;
    public GameObject bulletPrefab;

    public void shoot() {
        float distanceFromBullet = 0.1f;
        Instantiate(bulletPrefab,
            (Vector2)transform.position + facingDirectionNormalized * distanceFromBullet, 
            transform.rotation);
    }

    void Start()
    {
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

    }

    public void faceShipInDirection(Vector2 direction) {
        facingDirectionNormalized = direction.normalized;
    }
}
