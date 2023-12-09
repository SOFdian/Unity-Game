using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Initial
{                         
    //子弹数量
    public int bulletNum = 6;
    //射击角度
    public float angle = 15;
    protected override void Fire()
    {
        if(parent.tag != "Player")
        {
            interval = EnemyData.Instance.interval;
            bulletNum = EnemyData.Instance.bulletNum;
        }
        float leftAngle;
        if (bulletNum % 2 == 0)
        {
            //如果是偶数
            leftAngle = -angle / 2 - angle * (bulletNum / 2 - 1);
        }
        else
        {
            //如果是奇数
            leftAngle = -angle * (bulletNum / 2);
        }
        Bullet temp;
        for(int i=0;i<bulletNum;i++){
            GameObject bul = ObjectPool.Instance.GetGameObject(bullet);
            bul.transform.position = parent.transform.position;
            bul.transform.up = Quaternion.AngleAxis(leftAngle, Vector3.forward) * dir;
            temp = bul.GetComponent<Bullet>();
            temp.SetParent(gameObject);
            temp.CalculateDamage(player_CardList,weapon_CardList);
            temp.SetSpeed(Quaternion.AngleAxis(leftAngle, Vector3.forward) * dir);
            temp.Initialized();
            leftAngle += angle;
        }
    }


}
