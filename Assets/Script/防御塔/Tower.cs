using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float interval;
    public float radius;
    public bool findEnemy = false;
    MyTimer timer;
    public Collider2D[] all;
    public GameObject currentEnemy;
    GameObject gun;
    private void Awake() {
        timer = new MyTimer();  
        gun = transform.GetChild(0).gameObject;
        gun.SetActive(false);
    }
    private void FixedUpdate() {
        timer.runTheClock();
        //正常情况一段时间搜索一次敌人
        if(!findEnemy){
            gun.SetActive(false);
            if(timer.GetTime()>=interval){
                all = Physics2D.OverlapCircleAll(transform.position,radius);
                //如果搜索到碰撞体，就检查是否有敌人
                if(all.Length>0){
                    for(int i = 0; i < all.Length; i++){
                        if(all[i].CompareTag("Enemy")){
                            findEnemy = true;
                            //找到第一个敌人，开始攻击这个敌人直到敌人离开攻击范围或者死亡
                            gun.SetActive(true);
                            currentEnemy = all[i].gameObject;
                            all = null;
                            //这里不刷新时间，这样杀死敌人后可以快速找到下一个敌人
                            break;
                        }
                    }
                }
                timer.refreshTime();
            } 
        }else{
            if(currentEnemy==null||Vector2.Distance(transform.position,currentEnemy.transform.position)>radius||currentEnemy.activeSelf==false){
                findEnemy = false;
                currentEnemy = null;
                return;
            }
            transform.up = (currentEnemy.transform.position-transform.position);
            gun.transform.up = transform.up;
        }
    }
}
