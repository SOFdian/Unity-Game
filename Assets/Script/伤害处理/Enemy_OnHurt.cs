using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_OnHurt : OnHurt
{
    public GameObject coin;
    public delegate void OnHurtDelegate();
    public event OnHurtDelegate onHurt;
    //对于敌人，从ScriptableObject中拿自身属性
    public EnemyData enemyData;
    //当前生命和最大生命值
    public float curLife;
    public float fullLife;
    //护甲类型
    public string armor;
    //只有敌人会因为受到伤害的类型不同而变色
    //飞机和坦克的spriteRenderer数量不同，要区分
    public SpriteRenderer spriteRenderer;
    public Color color;
    public Color fireColor;
    public Color iceColor;
    public Color thunderColor;
    public Color poisonColor;
    public Color normalColor;

    MyTimer timer;
    float interval;
    //敌人的OnHurt同时也有Attribute的作用

    private void Start()
    {
        coin = Resources.Load<GameObject>("Coin");
        timer = new MyTimer();
        interval = 0.2f;
    }
    void ChangeLife(float damage)
    {
        curLife-=damage;
        if (curLife <= 0)
        {
            //爆金币咯
            GameObject temp = ObjectPool.Instance.GetGameObject(coin);
            temp.transform.position = gameObject.transform.position;
            //销毁并且初始化状态
            ObjectPool.Instance.RecycleGameObject(this.gameObject);

        }
    }

    public override float CalculateDamage(List<float> damage, List<string> type, float initialDamage)
    {
        ChangeLife(base.CalculateDamage(damage, type, initialDamage));
        return 0;
    }

    public override void Initialize()
    {
        fullLife = EnemyData.Instance.fullLife;
        curLife = fullLife;
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

    #region 元素伤害
    public override void CalculateType(List<string> type)
    {
        foreach (var item in type)
        {
            if (item == "火元素")
            {
                onHurt += Fire;
                Invoke("DeleteFire", 5);
            }
        }
    }
    void Fire()
    {
        ChangeLife(5);
        changeColor(fireColor); 
    }

    void DeleteFire()
    {
        onHurt -= Fire;
        changeColorBack();
    }
    #endregion

    #region 受伤变色
    void changeColor(Color color)
    {
        spriteRenderer.color = color;
    }
    void changeColorBack()
    {
        if (spriteRenderer.color != color)
        {
            spriteRenderer.color = color;
        }
    }
    #endregion
}
