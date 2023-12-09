using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame4 : MonoBehaviour
{
    MyTimer allTimer;
    //MyTimer timer;
    public GameObject overGame;
    // Start is called before the first frame update
    void Start()
    {
        allTimer = new MyTimer();
        //timer = new MyTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        allTimer.runTheClock();
        if (allTimer.GetTime() >180)
        {
            Time.timeScale = 0;
            overGame.SetActive(true);
        }
    }
}
