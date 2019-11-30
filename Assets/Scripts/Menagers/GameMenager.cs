using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public static class GameMenager// : MonoBehaviour
{
    static List<GameObject> allFleets = new List<GameObject>();
    static List<GameObject> allEzBots = new List<GameObject>();
    static List<GameObject> allMediumBots = new List<GameObject>();

    static Dictionary<int, string> nicknamesMap;
    static Dictionary<int, int> scoresMap;

    private static int playerFleetID;

    private static float mapRadius = 40f;
    private static Vector2 playerPosition;
    //TODO if performence allows it can be inreaset, it will give more bots next to player, as they wont be waiting
    private static float distanceFromPlayerWhenBotFreezesPosition = 30f;

    static string[] allNicknames;
    static int nicknameArrayIndex = 0;


    static int deafultName;

    public static float getMapRadius()
    {
        return mapRadius;
    }

    public static float getPositionFreezingDistance()
    {
        return distanceFromPlayerWhenBotFreezesPosition;
    }

    public static void setPlayerFleetID(int id) {
        playerFleetID = id;
    }

    static int mediumBotsAtTheStart = 40;
    static int ezBotsAtTheStart = 15;

    static float timeBetweenShots = 1.2f;

    static string ezBot = "BotPrefabs/EasyBot";
    static string mediumBot = "BotPrefabs/MediumBot";

    private static GameObject ezBotPrefab;
    private static GameObject mediumBotPrefab;

    private static Scoreboard scoreboardScript;

    public static void addFleetToGameMenager(GameObject fleet) {
        allFleets.Add(fleet);
    }

    public static float getTimeBetweenShots() {
        return timeBetweenShots;
    }

    public static void startGame()
    {
        nicknamesMap = new Dictionary<int, string>();
        scoresMap = new Dictionary<int, int>();

        deafultName = Random.Range(1001, 9998);

        scoreboardScript = GameObject.Find("Scoreboard").GetComponent<Scoreboard>();

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

        string[,] table = new string[2, 5];//{ { "nick1", "1" }, { "nick2", "2"}, { "nick3", "3" }, { "nick4", "4" }, { "nick5", "5" } };
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 5; j++) {
                table[i, j] = j.ToString();
            }
        }
        scoreboardScript.changeValuesOnScoreUI(table);

        readNicknamesFromFile();

    }

    private static void readNicknamesFromFile() {
        if (allNicknames != null)
            return;

        TextAsset asset = (TextAsset)Resources.Load("1000nicknames");

        allNicknames = asset.text.ToString().Split(
        new[] { System.Environment.NewLine },
         System.StringSplitOptions.None);
    }

    static void spawnMediumBot()
    {
        GameObject go = GameObject.Instantiate(mediumBotPrefab,
            Random.insideUnitCircle * mapRadius * 0.95f,
            Quaternion.identity);
        allMediumBots.Add(go);

        
    }

    public static void remapPlayerNickname(Fleet playerFleet) {
        string nick = "To ja";
        nicknamesMap[playerFleet.getID()] = nick;
        scoresMap[playerFleet.getID()] = 1000;//TODO change to 0

        playerFleet.changeNickname(nick);
    }

    public static void getNicknameForMe(Fleet fleet) {
        string nick = "";
        if (Random.Range(0f, 1f) > 0.4f)
            nick = getRandomNicknameFromPull();
        else
            nick = getRandomDeafultNickname();
        nicknamesMap[fleet.getID()] = nick;
        int initialScore = Random.Range(0, 10);
        for (int i = 0; i < 5; i++) {
            if (Random.Range(0f, 1f) > 0.7f)
            {
                initialScore += Random.Range(10, 30);
            }
            else {
                break;
            }
        }

        scoresMap[fleet.getID()] = initialScore;
        fleet.changeNickname(nick);
    }

    public static void increaseScoreBy(int id, int score) {
        int value = 0; 
        scoresMap.TryGetValue(id, out value);
        value += score;
        scoresMap[id] = value;
        //eut
        string a = "broken";
        nicknamesMap.TryGetValue(id, out a);

        Debug.Log(a + "   " +"final score: "+ value+" how much we incremented: " + s);
    }

    public static void refreshScoreboard() {
        

        List<KeyValuePair<int, int>> sortedScores = scoresMap.ToList();

        sortedScores.Sort(
            delegate (KeyValuePair<int, int> pair1,
            KeyValuePair<int, int> pair2)
            {
                return (-1) *pair1.Value.CompareTo(pair2.Value);
            }
        );
        //Debug.Log("sorted scores");
        //foreach (var i in sortedScores) {
           // Debug.Log(i.Key + "   " + i.Value);
       // }

        string[,] table = new string[2, 5];//{ { "nick1", "1" }, { "nick2", "2"}, { "nick3", "3" }, { "nick4", "4" }, { "nick5", "5" } };

        for (int i = 0; i < 5; i++)
        {
            string nick = "Deafault";
            int score = -1;
            try
            {
                nicknamesMap.TryGetValue(sortedScores[i].Key, out nick);
                score = sortedScores[i].Value;
            }
            catch (System.ArgumentOutOfRangeException e) {
                return;
            }
            

            table[0, i] = nick;
            table[1, i] = score.ToString();
        }
       
        scoreboardScript.changeValuesOnScoreUI(table);
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

    private static string getRandomNicknameFromPull() {
        nicknameArrayIndex += (int)Random.Range(3, 17);
        if (nicknameArrayIndex > allNicknames.Length - 1) {
            nicknameArrayIndex = 0;
        }
        return allNicknames[nicknameArrayIndex];
    }

    private static string getRandomDeafultNickname()
    {
        // 197 prime number
        deafultName += 197;
        if (deafultName > 9999)
            deafultName -= 9000;
        return "Player#" + deafultName.ToString();
    }

    public static void gameOver()
    {
        

        SceneManager.LoadScene("SampleScene");

    }
}
