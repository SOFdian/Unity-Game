using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Move",fileName ="PlayerState_Move")]
public class PlayerState_Move : PlayerState
{
    
    public override void Enter()
    {

    }
    public override void Exit()
    {

    }
    public override void LogicUpdate()
    {
        
    }
    public override void PhysicUpdate()
    {
        //切换动画
        if (gameInput.xMove==0 && gameInput.yMove == 0)
        {
            animator.Play("Player_Center");
        } else if (gameInput.xMove > 0)
        {
            animator.Play("Player_Right");
        } else if(gameInput.xMove < 0 )
        {
            animator.Play("Player_Left");
        }

        playerController.SetVelocityX(gameInput.xMove * playerAttribute.speed);
        playerController.SetVelocityY(gameInput.yMove * playerAttribute.speed);
        //防止斜向移动时速度合成
        if(gameInput.xMove != 0 && gameInput.yMove != 0)
        {
            playerController.SetVelocity(new Vector2(gameInput.xMove, gameInput.yMove).normalized * playerAttribute.speed);
        }
        playerController.GetFaceDir();
        
    }
}
