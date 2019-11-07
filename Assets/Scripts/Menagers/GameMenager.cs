using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMenager// : MonoBehaviour
{
    static List<GameObject> allFleets = new List<GameObject>();
    static float botsInstantiatingDistance = 50f;
    static int mediumBotsAtTheStart =15;
    static int ezBotsAtTheStart =30;

    static string ezBot = "BotPrefabs/EasyBot";
    static string mediumBot = "BotPrefabs/MediumBot";

    public static void addFleetToGameMenager(GameObject fleet) {
        allFleets.Add(fleet);
    }

    public static void startGame() {
        var botPrefab = (GameObject)Resources.Load(mediumBot, typeof(GameObject));
        for (int i = 0; i < mediumBotsAtTheStart; i++) {
            GameObject.Instantiate(botPrefab,
              new Vector2(Random.Range(-botsInstantiatingDistance, botsInstantiatingDistance), 
              Random.Range(-botsInstantiatingDistance, botsInstantiatingDistance)), 
              Quaternion.identity); 
        }
        botPrefab = (GameObject)Resources.Load(ezBot, typeof(GameObject));

        for (int i = 0; i < ezBotsAtTheStart; i++)
        {
            GameObject.Instantiate(botPrefab,
                new Vector2(Random.Range(-botsInstantiatingDistance, botsInstantiatingDistance),
                    Random.Range(-botsInstantiatingDistance, botsInstantiatingDistance)),
                Quaternion.identity);
        }
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
}
