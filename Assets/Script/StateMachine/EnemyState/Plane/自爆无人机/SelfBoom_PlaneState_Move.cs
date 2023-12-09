using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBoom_PlaneState_Move : PlaneState_Move
{
    public override void Enter()
    {
        planeController.myPath = null;
        planeFinder.StartPathAgain();
        planeController.GetComponent<Animator>().Play("移动");
    }
    public override void LogicUpdate()
    {
        //朝着基地移动，如果在中途遇到玩家就去炸玩家
        if(Vector2.Distance(planeController.GetPosition(),planeFinder.GetPlayerPosition())<3f){
            planeStateMachine.switchState(typeof(SelfBoom_PlaneState_HitPlayer));
        } 
        if(Vector2.Distance(planeController.GetPosition(),planeFinder.GetTargetPosition())<5f){
            planeStateMachine.switchState(typeof(SelfBoom_PlaneState_HitIcon));
        }
    }
}
