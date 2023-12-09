using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneState : ScriptableObject, IState
{

    protected PlaneController planeController;
    protected PlaneStateMachine planeStateMachine;
    protected PlaneFinder planeFinder;
    public void Initialize(PlaneController planeController, PlaneStateMachine planeStateMachine,PlaneFinder planeFinder)
    {
        this.planeController = planeController;
        this.planeStateMachine = planeStateMachine;
        this.planeFinder  = planeFinder;
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
