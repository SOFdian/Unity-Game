using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/TankState/HitPlayer",fileName ="TankState_HitPlayer")]
public class TankState_HitPlayer : TankState
{
    public float speed = 2;
    override public void Enter()
    {
        
        tankController.tankInitial.SetActive(true);
    }
    public override void Exit()
    {
        Debug.Log("Exit");
        tankController.tankInitial.SetActive(false);
    }
    public override void PhysicUpdate()
    {
        if (tankController.myPath == null)
        {
            return;
        }
        Move();
        //瞄准玩家
        tankController.Aim(tankFinder.GetPlayerPosition() - tankController.GetPosition());
        
    }
    public override void LogicUpdate()
    {
        //距离基地太近，进入攻击基地状态
        if (Vector2.Distance(tankController.GetPosition(), tankFinder.GetTargetPosition()) < 3f)
        {
            stateMachine.switchState(typeof(TankState_HitIcon));
        } 
        //玩家离开射程，进入移动状态
        else if(Vector2.Distance(tankController.GetPosition(), tankFinder.GetPlayerPosition()) > 8f)
        {
            stateMachine.switchState(typeof(TankState_Move));
        }
    }

    private void Move()
    {
        if (tankController.myPath.Count == 0)
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
