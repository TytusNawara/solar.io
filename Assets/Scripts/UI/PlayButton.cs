using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public void playGame() {
        SceneManager.LoadScene(1);
    }
    public void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(playGame);
    }
}
