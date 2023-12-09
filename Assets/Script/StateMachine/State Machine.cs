using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StateMachine : MonoBehaviour
{

    protected IState currentState;
    protected IState lastState;
    
    protected Dictionary<System.Type, IState> stateTable;
    void Update()
    {
        //主要用于状态间切换
        currentState.LogicUpdate();
    }
    void FixedUpdate()
    {
        //主要用于状态内逻辑处理
        currentState.PhysicUpdate();
    }
    protected void begin(IState state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void switchState(IState state)
    {
        lastState = state;
        currentState.Exit();
        begin(state);
    }
    public void switchState(System.Type state)
    {
        switchState(stateTable[state]);
    }
    public IState GetState()
    {
        return currentState;
    }
    public virtual void Initialize(){

    }

}


