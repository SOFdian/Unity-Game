using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    InputActions actions;
    private void Awake() {
        actions = new InputActions();
        //启用Player表
        actions.Player.Enable();
    }
    //从表中获取Player的Move动作
    //Lambda表达式
    public Vector2 move=>actions.Player.Move.ReadValue<Vector2>();
    //移动
    public float xMove=>move.x;
    public float yMove=>move.y;
    //射击
    public bool leftClick=>actions.Player.Mouse.IsPressed();
    public bool q=>actions.Player.Other.IsPressed();
}
