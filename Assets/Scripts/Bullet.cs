using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 5f;
    float bulletLifeTime = 5f;

    float timePassedSinceBulletShooted = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = ((Vector2)transform.up)* speed;
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
}
