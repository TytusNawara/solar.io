using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private Transform scoreboardEntryContainer;
    private Transform scoreboardEntryTemplate;
    private int howManyColumns = 5;
    private Text[] nicknames = new Text[5];
    private Text[] scores = new Text[5];


    private void Awake()
    {
        scoreboardEntryContainer = transform.Find("scorebordEntryContainer");
        scoreboardEntryTemplate = scoreboardEntryContainer.Find("scorebordEntry");


        scoreboardEntryTemplate.gameObject.SetActive(false);

        float templateHeight = 44f;
        for (int i = 0; i < howManyColumns; i++) {
            Transform entryTransform = Instantiate(scoreboardEntryTemplate, scoreboardEntryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("index").GetComponent<Text>().text = (i+ 1).ToString() + "." ;

            nicknames[i] = entryTransform.Find("nickname").GetComponent<Text>();
            scores[i] = entryTransform.Find("score").GetComponent<Text>();
        }
    }

    //nickname, score
    public void changeValuesOnScoreUI(string[,] scoreTable) {
        for (int i = 0; i < howManyColumns; i++) {
            nicknames[i].text = scoreTable[0, i];
            scores[i].text = scoreTable[1, i];
        }
    }
}
