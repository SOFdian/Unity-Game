using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    [SerializeField]PlayerState[] states;
    PlayerController playerController;
    GameInput gameInput;
    PlayerAttribute playerAttribute;
    Animator animator;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        gameInput = GetComponent<GameInput>();
        stateTable = new Dictionary<System.Type, IState>();
        playerAttribute = GetComponent<PlayerAttribute>();
        animator = GetComponent<Animator>();
        foreach (var state in states)
        {
            stateTable.Add(state.GetType(), state);
            //遍历所有状态，执行状态的初始化函数（状态都需要调用的组件都在这里传入）
            state.Initialize(playerController,gameInput,playerAttribute,animator);
        }

    }
    private void Start()
    {
        begin(stateTable[typeof(PlayerState_Move)]);
    }
    private void OnGUI()
    {
        
    }

}
