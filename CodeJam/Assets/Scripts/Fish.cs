using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Catch
{
    public Fish()
    {
        type = FishType.normal;
        gold = 10;
        catchInSeconds = 3;
    }
}
