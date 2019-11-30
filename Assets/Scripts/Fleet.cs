using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fleet : MonoBehaviour
{
    public GameObject shipPrefab;
    public GameObject canvasPrefab;

    private float speed = 0.08f*2f;//0.056f;//0.08f;
    List<GameObject> ships = new List<GameObject>();
    Vector2 range = new Vector2(-0.25f, 0.25f);
    int shipsAtTheStart = 4;
    public bool _debug_FleetCanMove = true;
    Vector2 fleetFacingDirection = Vector2.up;
    private Sprite shipSprite;
    private GameObject canvas;
    private float numberShipsToAdd = 0f;
    private bool wereShipsInstantiated = false;

    float distanceWhereNicknameIsDisabled;
    float distanceWhereNicknameIsEnabled;
    bool isTextEnabled = true;


    private Text textWithNickname;
    private string playerNameIfExist = "";

    int ID;

    private float minTimePeriodBetweenShoots;
    float timePassedSinceLastShoot;

    private float distnceToText = 1.7f;

    public void faceFleetInDirection(Vector2 direction) {
        fleetFacingDirection = direction;
    }

    public int getHowManyShips()
    {
        if (!wereShipsInstantiated)
            return -1;
        return ships.Count;
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
        ID = this.GetInstanceID();///eut

        distanceWhereNicknameIsDisabled = 16f;
        distanceWhereNicknameIsEnabled = distanceWhereNicknameIsDisabled - 2f;
        minTimePeriodBetweenShoots = GameMenager.getTimeBetweenShots()*0.6f;
        timePassedSinceLastShoot = minTimePeriodBetweenShoots;
        ID = gameObject.GetInstanceID();//AutoIncrementedKeysGenerator.generateUniqueFleetID();
        for (int i = 0; i < shipsAtTheStart; i++)
            ships.Add(Instantiate(shipPrefab, 
                (Vector2)transform.position + new Vector2(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y)),
                Quaternion.identity));
        foreach (var ship in ships) {
            ship.transform.parent = transform;
        }

        wereShipsInstantiated = true;
        GameMenager.addFleetToGameMenager(gameObject);
        setAllShipsToRandomSprite();

        Vector3 pos = transform.position;
        pos.y += distnceToText;

        canvas = Instantiate(canvasPrefab, pos, Quaternion.identity, transform);
        Transform nick = canvas.GetComponent<Transform>().Find("nickname");
        GameObject a = nick.gameObject;
        textWithNickname = nick.GetComponent<Text>();
        
        GameMenager.getNicknameForMe(this);
    }

    void Update()
    {
        timePassedSinceLastShoot += Time.deltaTime;
        if (playerNameIfExist != "") {
            changeNickname(playerNameIfExist);
            playerNameIfExist = "";
            Color color = Color.red;
            ColorUtility.TryParseHtmlString("#c487bc", out color);
            textWithNickname.color = color;

            GameMenager.remapPlayerNickname(this);//eut
        }
        checkForDistanceAndMenageIfCanvasEnabled();
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
       
            if (_debug_FleetCanMove &&
                Vector2.Distance(transform.position, GameMenager.getPlayerPosition()) <
        GameMenager.getPositionFreezingDistance())//eut
                transform.position = (Vector2)transform.position + fleetFacingDirection.normalized * speed;
        
    }

    public void informFleetThatEnemyShipIsDown()
    {
        //to balance, also remove some ships from pull because 
        //bot are respawning, creating new ones
        float howManyPointsGot = 0f;
        if (ships.Count < 3)
            howManyPointsGot = 1f;
        else if (ships.Count < 14)
        {
            howManyPointsGot = 3f / ships.Count;
        }
        else {
            howManyPointsGot = 0.05f;
        }

        numberShipsToAdd += howManyPointsGot;
        if (numberShipsToAdd >= 1)
        {
            addNewShipToFleet();
            numberShipsToAdd--;
        }
        GameMenager.increaseScoreBy(ID, 1);
        
    }

    public void changeNickname(string nick) {
        if (textWithNickname == null) {
            playerNameIfExist = nick;
            return;
        }
        textWithNickname.text = nick;
    }

    public void addNewShipToFleet()
    {
        GameObject toAdd = Instantiate(shipPrefab,
            (Vector2) transform.position +
            new Vector2(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y)),
            Quaternion.identity, transform);
        toAdd.GetComponent<Ship>().setSprite(shipSprite);
        ships.Add(toAdd);
    }

    private void checkForDistanceAndMenageIfCanvasEnabled()
    {
        float dist = Vector2.Distance(transform.position, GameMenager.getPlayerPosition());
        if (dist > distanceWhereNicknameIsDisabled)
        {
            Debug.DrawLine(transform.position, GameMenager.getPlayerPosition(), Color.red);

            canvas.SetActive(false);
        }

        else if (dist < distanceWhereNicknameIsEnabled)
        {
            Debug.DrawLine(transform.position, GameMenager.getPlayerPosition());

            canvas.SetActive(true);
        }

    }
}
