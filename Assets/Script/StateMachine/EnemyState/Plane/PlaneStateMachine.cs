using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneStateMachine : StateMachine
{
    protected PlaneController planeController;
    protected  ArrayList states = new ArrayList();
    protected PlaneFinder planeFinder;
    private void Awake() {
        //添加所有状态
        states.Add(ScriptableObject.CreateInstance<PlaneState_Move>());
        states.Add(ScriptableObject.CreateInstance<PlaneState_ChasePlayer>());
        states.Add(ScriptableObject.CreateInstance<PlaneState_HitIcon>());
        states.Add(ScriptableObject.CreateInstance<PlaneState_HitPlayer>());

        planeFinder = GetComponent<PlaneFinder>();
        planeController = GetComponent<PlaneController>();
        //加入字典中
        stateTable = new Dictionary<System.Type, IState>();
        //遍历所有状态，初始化状态
        PlaneState temp;
        foreach (var state in states) {
            temp = (PlaneState)state;
            stateTable.Add(state.GetType(), temp);
            temp.Initialize(planeController, this, planeFinder);
        }
    }
    protected  virtual void Start() {
        
    }
    public override void Initialize(){
        begin(stateTable[typeof(PlaneState_Move)]);
    }
}
