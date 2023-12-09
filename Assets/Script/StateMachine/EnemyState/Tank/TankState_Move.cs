using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/TankState/Move",fileName ="TankState_Move")]
public class TankState_Move : TankState 
{
    public float speed = 2 ;
    public override void Enter()
    {
        tankController.tankInitial.SetActive(false);
        //炮塔要回正
        tankController.Aim(-tankController.transform.up);
        tankFinder.StartPathAgain();

    }
    public override void PhysicUpdate()
    {
        if(tankController.myPath == null)
        {
            return;
        }
        Move();
    }
    public override void LogicUpdate()
    {
        //距离基地太近，进入攻击基地状态
        if (Vector2.Distance(tankController.GetPosition(), tankFinder.GetTargetPosition()) < 3f)
        {
            stateMachine.switchState(typeof(TankState_HitIcon));
        } else
        //距离玩家太近，进入攻击玩家状态
        if(Vector2.Distance(tankController.GetPosition(), tankFinder.GetPlayerPosition()) < 8f)
        {
            stateMachine.switchState(typeof(TankState_HitPlayer));
        }
    }
    
    private void Move()
    {
        if(tankController.myPath.Count == 0)
        {
            tankController.myPath = null;
            return;
        }
        //获取坦克面对方向
        Vector3 myPositon = tankController.GetPosition();
        Vector3 dir = (tankController.myPath[0] - myPositon).normalized;
        tankController.SetVelocity(new Vector2(dir.x, dir.y) * speed);
        tankController.ChangeFace(dir);
        //如果坦克的位置和寻路的第一个点的位置接近，就把这个点从寻路列表中移除
        if (Vector2.Distance(new Vector2(myPositon.x, myPositon.y), new Vector2(tankController.myPath[0].x, tankController.myPath[0].y)) < 0.1f)
        {
            tankController.myPath.RemoveAt(0);
        }
    }
}
