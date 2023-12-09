using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player_OnHurt : OnHurt
{
    protected MyTimer timer;
    protected float interval;
    protected string armor;
    public delegate void OnHurtDelegate();
    public event OnHurtDelegate onHurt;

    PlayerAttribute playerAttribute;

    public  void Start()
    {
        //燃烧效果
        timer = new MyTimer();
        interval = 0.2f;

        playerAttribute = GetComponent<PlayerAttribute>();

    }
    public override float CalculateDamage(List<float> damage, List<string> type, float initialDamage)
    {
        //扣血
        //float finalDamage = base.CalculateDamage(damage, type, initialDamage);
        playerAttribute.CalculateDamage(initialDamage);
        ShowDamage();
        return 0;
    }
    //这里再遍历type，设置不同效果
    public override void CalculateType(List<string> type)
    {
        foreach (var item in type)
        {
            if(item== "火元素")
            {
                onHurt += Fire;
                Invoke("DeleteFire", 5);
                ShowDamage();
            }
        }
    }
    void Fire()
    {
        playerAttribute.CalculateDamage(5);
    }
    void DeleteFire()
    {
        onHurt -= Fire;
    }
    //显示伤害数字
    private void ShowDamage()
    {

    }
    private void FixedUpdate()
    {
        if (onHurt != null)
        {
            //燃烧判定
            timer.runTheClock();
            if (timer.GetTime() >= interval)
            {
                onHurt();
                timer.refreshTime();
            }
        }
        
    }


}
