using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankInitial : Initial
{
    
    protected override void Awake()
    {
        timer = new MyTimer();
        parent = transform.parent.gameObject;
    }


    protected override void FixedUpdate()
    {
        transform.position = parent.transform.position;
        SetDir(-transform.parent.up);
        timer.runTheClock();
        Shoot();
    }
    public override void SetDir(Vector2 dir)
    {
        this.dir = dir.normalized;
    }

}
