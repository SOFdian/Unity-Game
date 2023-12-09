using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initial : MonoBehaviour
{
    #region 初始化部分
    //射击间隔
    public float initialInterval;
    public float interval;
    //子弹
    public GameObject bullet;
    //射击方向
    public Vector2 dir;
    //获取父物体
    public GameObject parent;
    public MyTimer timer;
    public PlayerAttribute playerAttribute;
    Bullet bull;

    public Weapon_CardList weapon_CardList;
    public Player_CardList player_CardList;
    protected virtual void Awake()
    {
        try
        {
            //敌人是没有playerAttribute的，所以要try一下
            playerAttribute = transform.parent.GetComponent<PlayerAttribute>();
        }
        catch { }
        try
        {
            //激光没有子弹，所以要try一下
            bull = bullet.GetComponent<Bullet>();
        }
        catch { }


        interval = initialInterval;

        timer = new MyTimer();
        try
        {
            parent = transform.parent.gameObject;


            if (parent.tag == "Player")
            {

                weapon_CardList = parent.GetComponent<Weapon_CardList>();
                player_CardList = parent.GetComponent<Player_CardList>();

            }
            else
            {

                weapon_CardList = GameObject.Find("Player").GetComponent<Weapon_CardList>();
                player_CardList = GameObject.Find("Player").GetComponent<Player_CardList>();
            }

        }
        catch { }

    }
    #endregion

    protected virtual void FixedUpdate()
    {
        //将自身的方向同步至父物体的方向，然后发射子弹
        timer.runTheClock();
        SetDir(transform.up);
        Shoot();
    }
    public virtual void SetDir(Vector2 dir)
    {
        this.dir = dir.normalized;
    }
    protected virtual void Shoot()
    {
        refreshInterval();
        if (timer.GetTime() >= interval)
        {
            Fire();
            timer.refreshTime();
        }

    }
    protected virtual void Fire()
    {

    }
    protected virtual void refreshInterval()
    {

        if (this.tag == "Player")
        {
            interval = initialInterval-player_CardList.GetInterval();

        }
    }


}
