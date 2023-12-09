using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneInitial : Initial
{
    protected override void Awake()
    {
        parent = transform.parent.gameObject;
        timer = new MyTimer();
    }
    protected override void FixedUpdate()
    {
        transform.position = parent.transform.position;
        SetDir(-transform.up);
        timer.runTheClock();
        Shoot();
    }
    public override void  SetDir(Vector2 dir)
    {
        this.dir = dir.normalized;
    }
}
