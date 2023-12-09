using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/TankState/HitIcon",fileName ="TankState_HitIcon")]
public class TankState_HitIcon : TankState
{
    override public void Enter()
    {
        tankController.tankInitial.SetActive(true);
        //停止移动，瞄准基地
        tankController.rb.bodyType = RigidbodyType2D.Kinematic;
        tankController.SetVelocity(Vector2.zero);
        tankController.ChangeFace(tankFinder.GetTargetPosition() - tankController.GetPosition());
        tankController.Aim(tankFinder.GetTargetPosition() - tankController.GetPosition());
    }
    
    public override void PhysicUpdate()
    {

    }
}
