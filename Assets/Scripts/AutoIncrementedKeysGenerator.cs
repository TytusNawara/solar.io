using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoIncrementedKeysGenerator
{
    static int fleetKey = 1;
    public static int generateUniqueFleetID() {
        fleetKey++;
        return fleetKey;
    }
}
