using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunCOPY : LaserGun
{
    public GameObject laserfather;
    //这里的awake是无法正确执行的，因为awake里用了transform.parent，然鹅复制品是没有父物体的,所以要
    protected override void Shoot()
    {
        //先看一下laserGun是否激活
        if(!laserfather.activeSelf){
            this.gameObject.SetActive(false);
            return;
        }
        base.Shoot();

    }
    public void Initialize(){
        laserPartical.SetActive(false);
        try{
            //敌人是没有playerAttribute的，所以要try一下
            playerAttribute = transform.parent.GetComponent<PlayerAttribute>();
        } catch{} 

        interval = initialInterval;

        timer = new MyTimer();
        parent = transform.parent.gameObject;
        line = GetComponent<LineRenderer>();
    }
}
