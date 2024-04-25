using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : Catch
{
    public Boot()
    {
        type = FishType.boot;
        gold = 1;
        
        catchInSeconds = 4;
    }
}
