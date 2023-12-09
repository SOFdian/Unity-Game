using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StateMachine/PlaneState/Move", fileName = "PlaneState_Move")]
public class PlaneState_Move : PlaneState
{
    public float speed=3;
    public override void Enter()
    {
        planeController.PlaneInitial.SetActive(false);
        planeController.myPath = null;
        planeFinder.StartPathAgain();
    }
    public override void PhysicUpdate()
    {
        //没寻路完就等，寻路很快所以不明显
        if(planeController.myPath == null)
        {
            return;
        } 
        Move();
    }
    public override void LogicUpdate()
    {
        //距离玩家很近就去追玩家
        if(Vector2.Distance(planeController.GetPosition(),planeFinder.GetPlayerPosition())<5f){
                planeStateMachine.switchState(typeof(PlaneState_ChasePlayer));
        } 
        //距离目标近
        else if(Vector2.Distance(planeController.GetPosition(),planeFinder.GetTargetPosition())<5f){
                planeStateMachine.switchState(typeof(PlaneState_HitIcon));
        }
    }

    private void Move()
    {
        if (planeController.myPath.Count == 0)
        {
            planeController.myPath = null;
            return;
        }
        //获取面对方向
        Vector3 myPosition = planeController.GetPosition();
        Vector3 dir = (planeController.myPath[0] - myPosition).normalized;
        planeController.SetVelocity(new Vector2(dir.x, dir.y) * speed);
        planeController.ChangeFace(dir);
        //如果位置和寻路的第一个点的位置接近，就把这个点从寻路列表中移除
        if (Vector2.Distance(new Vector2(myPosition.x, myPosition.y), new Vector2(planeController.myPath[0].x, planeController.myPath[0].y)) < 0.1f)
        {
            planeController.myPath.RemoveAt(0);
        }
    }
}
