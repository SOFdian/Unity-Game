using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steam : MonoBehaviour
{
    MyTimer timer = new MyTimer();
    private void OnTriggerStay2D(Collider2D other) {

    }
    private void FixedUpdate()
    {
        timer.runTheClock();
        if(timer.GetTime() > 2)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
