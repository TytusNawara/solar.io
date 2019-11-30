using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InterfaceLookMenager : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickHandle;
    public GameObject button;

    public GameObject playerScoreBackground;
    public GameObject scoreTableBackground;



    // Start is called before the first frame update
    void Start()
    {
        Color buttonsColor = Color.white;
        ColorUtility.TryParseHtmlString(GameMenager.interfaceColor, out buttonsColor);
        joystick.GetComponent<Image>().color = buttonsColor;
        joystickHandle.GetComponent<Image>().color = buttonsColor;
        button.GetComponent<Image>().color = buttonsColor;

        buttonsColor.a = 0.5f;
        playerScoreBackground.GetComponent<Image>().color = buttonsColor;
        scoreTableBackground.GetComponent<Image>().color = buttonsColor;



    }

}
