using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStateMachine : StateMachine
{
    //普通坦克有三种状态
    //1.周围没有可以攻击的目标，这个时候炮塔面对正前方，移动
    //2.可以攻击到玩家（或防御塔），这个时候炮塔瞄准玩家并攻击，同时移动
    //3.移动到基地附近，这个时候只攻击基地，不移动
    ArrayList states = new ArrayList();
    TankController tankController;
    TankFinder tankFinder;
    private void Awake() {
        states.Add(ScriptableObject.CreateInstance<TankState_Move>());
        states.Add(ScriptableObject.CreateInstance<TankState_HitPlayer>());
        states.Add(ScriptableObject.CreateInstance<TankState_HitIcon>());
        tankFinder = GetComponent<TankFinder>();
        tankController = GetComponent<TankController>();
        //加入字典中
        stateTable = new Dictionary<System.Type, IState>();
        //遍历所有状态，初始化状态
        TankState temp;
        foreach (var state in states) {
            temp = (TankState)state;
            stateTable.Add(state.GetType(), temp);
            temp.Initialize(tankController, tankFinder, this);
        }
    }
    private void Start() {
        
    }
    public override void Initialize(){
        begin(stateTable[typeof(TankState_Move)]);
    }
}
