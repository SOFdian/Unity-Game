using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlaneState/HitIcon",fileName ="PlaneState_HitIcon")]
public class PlaneState_HitIcon : PlaneState
{
    Vector2 dir;
    public override void Enter()
    {
        //获取射击方向
        dir = planeFinder.GetTargetPosition()-planeController.GetPosition();
        planeController.Aim(dir);
        planeController.PlaneInitial.SetActive(true);
    }
    public override void PhysicUpdate()
    {
        planeController.SetVelocity(Vector2.zero);
    }
    public override void LogicUpdate()
    {
        //距离玩家很近就去追玩家
        if(Vector2.Distance(planeController.GetPosition(),planeFinder.GetPlayerPosition())<5f){
                planeStateMachine.switchState(typeof(PlaneState_ChasePlayer));
        } 
    }
}
