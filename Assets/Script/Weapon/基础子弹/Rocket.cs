using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    public float length;
    public float lerp;


    public Vector3 targetPos;
    public Vector3 direction;
    public bool arrived;
    protected bool isDestroy = false;
    protected bool unControl = false;


    //rocket的速度是动态的，重写SetSpeed函数，用来设置初始速度（即初始方向）
    public override void SetSpeed(Vector2 dir)
    {
        //获取玩家面对的方向
        Vector3 parentDir = parent.transform.up;
        //获取玩家位置
        Vector3 parentPos = parent.transform.position;
        //计算出子弹的目标位置
        targetPos = parentPos + parentDir * length;
    }
    public override void Initialized()
    {
        base.Initialized();
        isDestroy = false;
        arrived = false;
        unControl = false;
    }

    private void FixedUpdate()
    {
        timer.runTheClock();
        direction = (targetPos - transform.position).normalized;
        if (!arrived)
        {
            transform.up = Vector3.Slerp(transform.up, direction, lerp / Vector2.Distance(transform.position, targetPos));
            rb.velocity = transform.up * speed;
        } else if(!unControl){
            //随机赋予一个速度
            //rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
            transform.up = rb.velocity;
            unControl = true;
        }
        if (Vector2.Distance(transform.position, targetPos) < 1f)
        {
            arrived = true;
        }
        tooLong();
    }

}
