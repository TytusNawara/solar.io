using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMenager : MonoBehaviour
{
    
    public Sprite[] shipsSprites;
    public Sprite[] bulletSprites;

    public int getHowManyShipSpritesAreAvailable()
    {
        return shipsSprites.Length;
    }

    public Sprite getSpriteFromIndex(int index)
    {
        return shipsSprites[index];
    }
}
