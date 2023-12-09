using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTimer
{
    public float aClock;
    public bool stop = false;
    public void runTheClock()
    {
        if (!stop)
        {
            aClock += Time.deltaTime;
        }
    }
    public float GetTime()
    {
        return aClock;
    }
    public void refreshTime()
    {
        aClock=0;
    }

}
