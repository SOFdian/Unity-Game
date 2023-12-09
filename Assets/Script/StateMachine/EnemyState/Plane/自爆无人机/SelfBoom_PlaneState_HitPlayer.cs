using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelfBoom_PlaneState_HitPlayer : SelfBoom_PlaneState_HitIcon
{

    public override void Enter()
    {
        base.Enter();
        dir = planeFinder.GetPlayerPosition() - planeController.GetPosition();
        dir = dir.normalized;
    }

}
