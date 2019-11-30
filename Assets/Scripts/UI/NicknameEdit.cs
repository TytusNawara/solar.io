using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NicknameEdit : MonoBehaviour
{
    InputField inputField;
    Color color;
    const string key = "playerNickname";
    // Start is called before the first frame update

    void endEdit(InputField input)
    {
        string nick = GameMenager.playerNickname;

        if (input.text.Length > 0)
        {
            nick = input.text;

        }
        else if (input.text.Length == 0)
        {
            input.text = nick;
        }

        GameMenager.playerNickname = nick;
        PlayerPrefs.SetString(key, nick);
    }
    void Start()
    {
        inputField = gameObject.GetComponent<InputField>();

        color = Color.white;
        ColorUtility.TryParseHtmlString(GameMenager.interfaceColor, out color);
        ColorBlock colorBlock = inputField.colors;
        colorBlock.normalColor = color;
        colorBlock.highlightedColor = color * 1.1f;
        inputField.colors = colorBlock;

        string nick = PlayerPrefs.GetString(key, GameMenager.playerNickname);
        inputField.text = nick;

        inputField.onEndEdit.AddListener(delegate{ endEdit(inputField); });
    }

    
}
