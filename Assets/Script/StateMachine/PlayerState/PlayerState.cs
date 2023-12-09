using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState

{
    protected PlayerController playerController;
    protected GameInput gameInput;
    protected PlayerAttribute playerAttribute;
    protected Animator animator;
    public void Initialize(PlayerController playerController,GameInput gameInput,PlayerAttribute playerAttribute, Animator animator)
    {
        this.playerController = playerController;
        this.gameInput = gameInput;
        this.playerAttribute = playerAttribute;
        this.animator = animator;
    }
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicUpdate()
    {
        
    }
    
}
