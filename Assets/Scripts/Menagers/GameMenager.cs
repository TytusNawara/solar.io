using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameMenager// : MonoBehaviour
{
    static List<GameObject> allFleets = new List<GameObject>();
    static List<GameObject> allEzBots = new List<GameObject>();
    static List<GameObject> allMediumBots = new List<GameObject>();
    private static float mapRadius = 40f;
    private static Vector2 playerPosition;
    //TODO if performence allows it can be inreaset, it will give more bots next to player, as they wont be waiting
    private static float distanceFromPlayerWhenBotFreezesPosition = 30f;

    public static float getMapRadius()
    {
        return mapRadius;
    }

    public static float getPositionFreezingDistance()
    {
        return distanceFromPlayerWhenBotFreezesPosition;
    }

    static int mediumBotsAtTheStart = 40;
    static int ezBotsAtTheStart = 15;

    static float timeBetweenShots = 1.2f;

    static string ezBot = "BotPrefabs/EasyBot";
    static string mediumBot = "BotPrefabs/MediumBot";

    private static GameObject ezBotPrefab;
    private static GameObject mediumBotPrefab;

    public static void addFleetToGameMenager(GameObject fleet) {
        allFleets.Add(fleet);
    }

    public static float getTimeBetweenShots() {
        return timeBetweenShots;
    }

    public static void startGame()
    {
        allFleets = new List<GameObject>();
        allEzBots = new List<GameObject>();
        allMediumBots = new List<GameObject>();

        Time.timeScale = 0.7f;
        mediumBotPrefab = (GameObject)Resources.Load(mediumBot, typeof(GameObject));
        for (int i = 0; i < mediumBotsAtTheStart; i++) {
            spawnMediumBot();
        }
        ezBotPrefab = (GameObject)Resources.Load(ezBot, typeof(GameObject));

        for (int i = 0; i < ezBotsAtTheStart; i++)
        {
            spawnEzBot();
        }
    }

    static void spawnMediumBot()
    {
        GameObject go = GameObject.Instantiate(mediumBotPrefab,
            Random.insideUnitCircle * mapRadius * 0.95f,
            Quaternion.identity);
        allMediumBots.Add(go);
    }

    static void spawnEzBot()
    {
        GameObject go = GameObject.Instantiate(ezBotPrefab,
            Random.insideUnitCircle * mapRadius * 0.95f,
            Quaternion.identity);
        allEzBots.Add(go);
    }

    public static Vector2[] getAllFleetsLocations() {
        Vector2[] toReturn = new Vector2[allFleets.Count];
        for (int i = 0; i < toReturn.Length; i++)
            toReturn[i] = allFleets[i].transform.position;
        return toReturn;
    }

    public static GameObject getFleetWithIndex(int index)
    {
        //if (index >= 0 && index < allFleets.Count)
        return allFleets[index];
        // return allFleets[0];
    }

    public static void removeBotAndRespawnNewOne(GameObject bot)
    {
        GameObject deadFleet = bot.GetComponent<BasicBot>().fleetThatBotControlls;
        allFleets.Remove(deadFleet);
        if (allMediumBots.Remove(bot))
        {
            spawnMediumBot();
        } else if (allEzBots.Remove(bot))
        {
            spawnEzBot();
        }
        else
        {
            // Debug.Log("error, tried to remove bot that is not at list, new bot was not spawned");
        }
    }
    public static Vector2 getPlayerPosition() {
        return playerPosition;
    }

    public static void setPlayerPostion(Vector2 _playerPosition) {
        playerPosition = _playerPosition;
    }

    public static void gameOver()
    {
        

        SceneManager.LoadScene("SampleScene");

    }
}
