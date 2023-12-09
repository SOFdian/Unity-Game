using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : TankController
{
    public GameObject PlaneInitial;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //自爆飞机没有武器
        try
        {
            PlaneInitial = transform.Find("PlaneInitial").gameObject;
            PlaneInitial.SetActive(false);
        }
        catch { }

    }
    public override void Aim(Vector2 dir)
    {
        PlaneInitial.transform.up = dir;
    }
    //自爆飞机
    void Boom()
    {
        //找半径内的玩家或基地造成伤害
        Collider2D[] playerOrIcon = Physics2D.OverlapCircleAll(GetPosition(), 3, LayerMask.GetMask("Player"));
        foreach (Collider2D col in playerOrIcon)
        {
            col.GetComponent<OnHurt>().CalculateDamage(null, null, 20);
        }
        ObjectPool.Instance.RecycleGameObject(gameObject);
    }
}
