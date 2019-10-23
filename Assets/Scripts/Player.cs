using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    Vector2 joystickDirectionNormalized;
    public GameObject fleetPrefab;
    GameObject playersFleet;
    // Start is called before the first frame update
    void Start()
    {
        playersFleet = Instantiate(fleetPrefab, transform.position, Quaternion.identity);
        joystickDirectionNormalized = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(joystick.Direction.magnitude > 0)
            joystickDirectionNormalized = joystick.Direction.normalized;
        float angle = Vector3.Angle(new Vector3(0.0f, 1.0f, 0.0f), joystickDirectionNormalized);
        if (joystickDirectionNormalized.x < 0.0f)
        {
            angle = -angle;
            angle = angle + 360;
        }
        //Vector2 lookDirection = (Vector2)transform.position + joystick.Direction.normalized;
        //transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.forward);
        //Vector3 point = new Vector3(5, 0, 0);
        //Vector3 axis = new Vector3(0, 0, 1);
        //transform.RotateAround(transform.position, axis, angle);
        transform.rotation = Quaternion.Euler(Vector3.forward * (-angle));
        playersFleet.GetComponent<Fleet>().faceFleetInDirection(joystickDirectionNormalized);

    }
}
