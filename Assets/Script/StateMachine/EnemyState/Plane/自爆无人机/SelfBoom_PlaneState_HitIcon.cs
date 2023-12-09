using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SelfBoom_PlaneState_HitIcon : PlaneState
{
    protected float speed = 20f;
    protected Vector2 dir;
    protected MyTimer timer;
    public override void Enter()
    {
        planeController.SetVelocity(Vector2.zero);
        timer = new MyTimer();
        //冲刺方向
        dir = planeFinder.GetTargetPosition(true) - planeController.GetPosition();
        dir = dir.normalized;
        //播放动画
        planeController.GetComponent<Animator>().Play("自爆飞机");


    }
    public override void PhysicUpdate()
    {
        timer.runTheClock();
        //0.5s后冲向目标，在这期间逐渐旋转至方向
        if (timer.GetTime() > 0.5f)
        {
            planeController.SetVelocity(dir*speed);
            return;
        } else
        {
            // 计算插值比例
            float t = timer.GetTime() / 0.5f;
            Vector2 newDir = Vector2.Lerp(-planeController.transform.up, dir, t);
            // 使用Lerp进行旋转
            planeController.transform.up = -newDir;
        }
        
    }
    public override void LogicUpdate()
    {
        if (timer.GetTime() > 1f || 
            Vector2.Distance(planeController.GetPosition(), planeFinder.GetTargetPosition(true)) < 1.5f||
            Vector2.Distance(planeController.GetPosition(), planeFinder.GetPlayerPosition()) < 1.5f)
        {
            planeStateMachine.switchState(typeof(SelfBoom_PlaneState_Boom));
        }
    }
    public override void Exit()
    {
        timer.refreshTime();
    }


}
