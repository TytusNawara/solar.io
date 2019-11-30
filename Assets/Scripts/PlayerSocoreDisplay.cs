using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSocoreDisplay : MonoBehaviour
{
    Text text;
    int lastScore;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int score =  GameMenager.getPlayerScore();
        if (score == lastScore)
            return;
        lastScore = score;
        text.text = "Your score: " + lastScore;
    }
}
