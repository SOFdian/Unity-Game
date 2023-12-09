using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData 
{
    private static EnemyData instance;
    public static EnemyData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyData();
            }
            return instance;
        }
    }

    public float fullLife = 100;
    public float maxLife = 5000;

    public float initialDamage=2;
    public float maxDamage=10;

    public float interval=2;
    public float maxInterval=1;

    public float bulletSpeed=2;
    public float maxBulletSpeed=15;

    public int bulletNum=1;
    public int maxBulletNum=5;

    public float temp = 1.5f;

    public void UpdateData()
    {
        initialDamage *= temp;
        interval /= temp;
        bulletSpeed *= temp;
        bulletNum += 1;
        fullLife += 200;

        if (initialDamage>maxDamage)
        {
            initialDamage = maxDamage;
        }
        if(interval<maxInterval)
        {
            interval = maxInterval;
        }
        if (bulletSpeed > maxBulletSpeed)
        {
            bulletSpeed = maxBulletSpeed;
        }
        if(bulletNum > maxBulletNum)
        {
            bulletNum = maxBulletNum;
        }
        if (fullLife < maxLife)
        {
            fullLife = maxLife;
        }
    }
}
