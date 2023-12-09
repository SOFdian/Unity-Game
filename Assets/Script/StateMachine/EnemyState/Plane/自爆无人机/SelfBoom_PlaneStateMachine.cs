using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBoom_PlaneStateMachine : PlaneStateMachine
{
    private void Awake()
    {
        states.Add(ScriptableObject.CreateInstance<SelfBoom_PlaneState_Move>());
        states.Add(ScriptableObject.CreateInstance<SelfBoom_PlaneState_HitIcon>());
        states.Add(ScriptableObject.CreateInstance<SelfBoom_PlaneState_HitPlayer>());
        states.Add(ScriptableObject.CreateInstance<SelfBoom_PlaneState_Boom>());
        planeFinder = GetComponent<PlaneFinder>();
        planeController = GetComponent<PlaneController>();
        //加入字典中
        stateTable = new Dictionary<System.Type, IState>();
        //遍历所有状态，初始化状态
        PlaneState temp;
        foreach (var state in states)
        {
            temp = (PlaneState)state;
            stateTable.Add(state.GetType(), temp);
            temp.Initialize(planeController, this, planeFinder);
        }
    }
    protected override void Start()
    {
        
    }
    public override void Initialize()
    {
        begin(stateTable[typeof(SelfBoom_PlaneState_Move)]);
    }
}
