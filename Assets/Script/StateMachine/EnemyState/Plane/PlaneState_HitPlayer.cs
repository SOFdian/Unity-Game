using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlaneState/HitPlayer", fileName = "PlaneState_HitPlayer")]
public class PlaneState_HitPlayer : PlaneState
{
    public override void Enter()
    {
        planeController.PlaneInitial.SetActive(true);
    }
    public override void PhysicUpdate()
    {
        Vector2 shootDir = planeFinder.GetPlayerPosition() - planeController.GetPosition();
        //瞄准敌人
        planeController.Aim(shootDir);
        planeController.ChangeFace(shootDir);
        planeController.SetVelocity(Vector2.zero);
    }
    public override void LogicUpdate()
    {
        //距离玩家远了就去追玩家
        if (Vector2.Distance(planeController.GetPosition(), planeFinder.GetPlayerPosition()) > 5f)
        {
            planeStateMachine.switchState(typeof(PlaneState_ChasePlayer));
        }
    }
}
