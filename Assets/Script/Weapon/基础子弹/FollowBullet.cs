using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowBullet : Bullet
{

    //追踪的距离
    public float followDis;
    public float percentSpeed;
    public float percent = 0;
    Vector2 startPosition;
    public GameObject target;
    //二阶贝塞尔曲线的那个中间点
    Vector2 midPoint;
    Vector2 pos;

    List<Vector2> arr;
    private void FixedUpdate()
    {
        timer.runTheClock();
        tooLong();
        //如果范围内没有敌人就是普通子弹（消灭敌人后也变为普通子弹）
        if (target == null)
        {

            return;
        } else
        {
            if (arr.Count == 2)
            {
                //即将走完路径，此时利用最后两个点计算速度方向
                Vector2 dir =  arr[1] - arr[0];
                rb.velocity = dir.normalized*(Vector2.Distance(arr[1], arr[0]) / Time.deltaTime) ;
                return;
            }
            transform.position = arr[0];
            arr.RemoveAt(0);

        }
    }
    
    //获得二阶贝塞尔曲线的那个中间点
    Vector2 GetMidPoint(Vector2 a, Vector2 b)
    {
        //先在玩家位置和敌人位置构成的线段上随机取一个点
        Vector2 temp = Vector2.Lerp(a, b, UnityEngine.Random.Range(0.2f,0.8f));
        //求出这个线段的垂线
        Vector2 normal = Vector2.Perpendicular(b - a).normalized;
        //往这个方向上随机取一个点
        float length = (a - b).magnitude;
        //顺便把percentSpeed也求了
        percentSpeed = speed / (b - a).magnitude;
        return temp + normal * length * UnityEngine.Random.Range(-1f, 1f) * 1;

    }
    public override void Initialized()
    {
        base.Initialized();
        startPosition = transform.position;
        percent = 0;
        pos = Vector3.zero;
        //这里要设置为null，也就是清空上一轮的target
        target = null;
        //搜寻敌人
        try
        {
            target = Physics2D.OverlapCircle(startPosition, followDis, LayerMask.GetMask("Enemy")).gameObject;
            midPoint = GetMidPoint(startPosition, target.transform.position);
            pos = new Vector3(1, 1, 1);
            //这个n不要定死比较好，根据两者距离来定
            int n = 20;
            arr = CalculateCubicBezierCurve(startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition), midPoint, target.transform.position, n);

        }
        catch
        {
            rb.velocity = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized*speed;
        }

        
    }

    public List<Vector2> CalculateCubicBezierCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, int n)
    {
        List<Vector2> points = new List<Vector2>();

        for (int i = 0; i < n; i++)
        {
            float t = i / (float)(n - 1);
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            // 三次贝塞尔曲线公式
            Vector2 point = uuu * p0 + 3 * uu * t * p1 + 3 * u * tt * p2 + ttt * p3;

            points.Add(point);
        }

        return points;
    }
}
