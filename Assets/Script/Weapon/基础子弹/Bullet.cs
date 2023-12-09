using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region 初始化
    public float playerDamage = 10;
    //子弹速度
    public float initialSpeed;
    public float speed;
    //子弹摧毁特效
    public GameObject gb;
    protected Rigidbody2D rb;
    bool isDestroy;
    protected MyTimer timer;
    protected GameObject parent;
    protected string parentType;
    public float cardSpeed;
    //这个playerAttribute是为了获取玩家的子弹速度
    protected PlayerAttribute playerAttribute;
    //一个用于存储元素类型，一个用于存储伤害值

    public List<string> type;
    public List<float> damage;

    public float initialDamage;
    public virtual void Awake()
    {
        speed = initialSpeed;
        rb = GetComponent<Rigidbody2D>();
        timer = new MyTimer();
    }
    #endregion
    //处理子弹运动逻辑
    private void FixedUpdate()
    {
        timer.runTheClock();
        tooLong();
    }
    //计算子弹速度（根据卡牌或EnemyData）
    protected virtual void refreshBulletSpeed(Player_CardList player_CardList)
    {

        if (parent.tag == "Player")
        {
            //有两种修改子弹速度的方式，一是通过element，二是通过playerAttribute
            //element直接修改initialSpeed
            //playerAttribute通过百分比修改
            speed = initialSpeed + player_CardList.GetBulletSpeed(); ;
        }
        //需要增加对于敌人发射子弹的判断，否则敌人的子弹的速度可能会和玩家的子弹的速度相同
        else
        {
            speed = EnemyData.Instance.bulletSpeed; ;
        }
    }
    //修改子弹Layer和ParentType
    public virtual void SetParent(GameObject parent)
    {
        this.parent = parent;
        //尝试获得玩家的playerAttribute
        playerAttribute = parent.transform.parent.GetComponent<PlayerAttribute>();
        if(playerAttribute != null)
        {
            parentType = "Player";
        } else
        {
            //检查是坦克还是飞机
            if (parent.transform.parent.name == "TankInitial")
            {
                parentType = "Tank";
            }
            else
            { 
                parentType = "Plane";
            }
        }
        gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(parent.layer));
    }
    //存在时间过长时销毁子弹
    protected virtual void tooLong()
    {
        if (timer.GetTime() >= 2.5f)
        {
            //生成特效
            GameObject go = ObjectPool.Instance.GetGameObject(gb);
            go.transform.position = transform.position;
            //回收子弹
            ObjectPool.Instance.RecycleGameObject(gameObject);
            isDestroy = true;
            timer.refreshTime();
        }

    }
    //传入方向，设置具体的速度
    public virtual void SetSpeed(Vector2 dir)
    {

        rb.velocity = dir * speed;
    }
    //计算子弹伤害
    public void CalculateDamage(Player_CardList player_CardList, Weapon_CardList weapon_CardList)
    {
        if (parentType == "Player")
        {
            refreshBulletSpeed(player_CardList);
            type = weapon_CardList.getType();
            damage = weapon_CardList.getDamage();
        }
        else
        {
            //读取数值到initialDamage
            initialDamage = EnemyData.Instance.initialDamage;

        }

    }
    
    public virtual void Initialized()
    {
        isDestroy = false;
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        MyOnDestroy(collision);
    }
    
    //碰撞时触发
    protected virtual void MyOnDestroy(Collider2D collision)
    {

        if(collision.tag == "Enemy"|| collision.tag == "Cover"|| collision.tag == "Player"|| collision.tag == "HPIcon")
        {
            //玩家、敌人、基地、护盾
            collision.GetComponent<OnHurt>().CalculateDamage(damage, type, initialDamage);
            OnDestroy();
            return;
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            //只有坦克的子弹会被阻挡
            if (parentType == "Tank")
            {
                OnDestroy();
            }
        }


        //后面可能会做个防御类的无人机来
    }


    protected virtual void OnDestroy()
    {
        if (isDestroy) return;
        //生成特效
        GameObject go = ObjectPool.Instance.GetGameObject(gb);
        go.transform.position = transform.position;
        //回收子弹
        Initialized();
        ObjectPool.Instance.RecycleGameObject(gameObject);
    }

}
