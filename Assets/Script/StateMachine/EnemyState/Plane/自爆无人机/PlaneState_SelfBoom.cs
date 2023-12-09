using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneState_SelfBoom : PlaneState
{
    GameObject target;
    public float speed;
    public override void Enter()
    {
        target = GameObject.Find("HP Icon");
    }
    public override void PhysicUpdate()
    {
        //
    }
}
