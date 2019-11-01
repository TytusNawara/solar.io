using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMenager// : MonoBehaviour
{
    static List<GameObject> allFleets = new List<GameObject>();
    static float botsInstantiatingDistance = 7f;
    static int botsAtTheStart = 4;

    public static void addFleetToGameMenager(GameObject fleet) {
        allFleets.Add(fleet);
    }

    public static void startGame() {
        var fleetPrefab = (GameObject)Resources.Load("BotPrefabs/EasyBot", typeof(GameObject));
        for (int i = 0; i < botsAtTheStart; i++) {
            GameObject.Instantiate(fleetPrefab,
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
}
