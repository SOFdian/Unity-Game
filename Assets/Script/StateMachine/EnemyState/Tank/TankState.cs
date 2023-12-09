using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankState : ScriptableObject,IState
{
    protected TankController tankController;
    protected TankFinder tankFinder;
    protected StateMachine stateMachine;
    public void Initialize(TankController tankController, TankFinder tankFinder, StateMachine stateMachine)
    {
        this.tankController = tankController;
        this.tankFinder = tankFinder;
        this.stateMachine = stateMachine;
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
