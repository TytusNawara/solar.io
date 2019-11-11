using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMenager// : MonoBehaviour
{
    static List<GameObject> allFleets = new List<GameObject>();
    static List<GameObject> allEzBots = new List<GameObject>();
    static List<GameObject> allMediumBots = new List<GameObject>();
    private static float botsInstantiatingDistance = 20f;//30f;
    static int mediumBotsAtTheStart =30;
    static int ezBotsAtTheStart =0;

    static string ezBot = "BotPrefabs/EasyBot";
    static string mediumBot = "BotPrefabs/MediumBot";

    private static GameObject ezBotPrefab;
    private static GameObject mediumBotPrefab;

    public static void addFleetToGameMenager(GameObject fleet) {
        allFleets.Add(fleet);
    }

    public static void startGame()
    {
        Time.timeScale = 0.7f;//eut
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
            new Vector2(Random.Range(-botsInstantiatingDistance, botsInstantiatingDistance),
                Random.Range(-botsInstantiatingDistance, botsInstantiatingDistance)),
            Quaternion.identity);
        allMediumBots.Add(go);
    }

    static void spawnEzBot()
    {
        GameObject go = GameObject.Instantiate(ezBotPrefab,
            new Vector2(Random.Range(-botsInstantiatingDistance, botsInstantiatingDistance),
                Random.Range(-botsInstantiatingDistance, botsInstantiatingDistance)),
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
        }else if (allEzBots.Remove(bot))
        {
            spawnEzBot();
        }
        else
        {
            Debug.Log("error, tried to remove bot that is not at list, new bot was not spawned");
        }
    }
}
