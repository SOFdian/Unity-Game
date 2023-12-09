using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlaneState/ChasePlayer",fileName ="PlaneState_ChasePlayer")]
public class PlaneState_ChasePlayer : PlaneState
{
    public float interval=0.5f;
    public float speed = 8;
    protected List<Vector3> myPath;
    protected MyTimer timer;
    public override void Enter()
    {
        Debug.Log("ChasePlayer");
        //玩家已经进入攻击范围，每一定时间寻路一次，同时不停射击玩家
        planeController.PlaneInitial.SetActive(true);
        timer = new MyTimer();
    }
    public override void PhysicUpdate()
    {
        timer.runTheClock();
        Vector2 dir = planeFinder.GetPlayerPosition()-planeController.GetPosition();
        planeController.Aim(dir);
        planeController.ChangeFace(dir);
        //不要秒设置速度比较好
        if (timer.GetTime() > interval)
        {
            planeController.SetVelocity(dir.normalized * speed);
            timer.refreshTime();
        }
        
    }
    public override void LogicUpdate()
    {
        //不断追击玩家，距离玩家很近的时候进入随机机动状态
        if(Vector2.Distance(planeController.GetPosition(),planeFinder.GetPlayerPosition())<3f){
            planeStateMachine.switchState(typeof(PlaneState_HitPlayer));
        } else if(Vector2.Distance(planeController.GetPosition(),planeFinder.GetPlayerPosition())>10f){
            planeStateMachine.switchState(typeof(PlaneState_Move));
        }
    }
}
