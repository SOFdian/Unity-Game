using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Initial
{
    //激光相关的属性：伤害，长度，宽度，条数，角度
    //需要一个偏移量，来调整激光的发射角度
    public Quaternion offset;
    public int num = 1;

    public float angle;
    public GameObject laserPartical;
    public LineRenderer line;
    public float laserLength = 10;
    public GameObject copy;
    public float distance;
    public List<GameObject> copies = new List<GameObject>();


    public List<string> type;
    public List<float> damage;
    public Weapon_CardList cardList;
    public float initialDamage;

 

    protected override void Awake()
    {
        timer = new MyTimer();
        cardList = GameObject.Find("Player").GetComponent<Weapon_CardList>();
        laserPartical.SetActive(false);
        base.Awake();
        line = GetComponent<LineRenderer>();
    }
    protected override void Shoot()
    {
        //要把复制品全部激活
        for (int i = 0; i < copies.Count; i++)
        {
            copies[i].SetActive(true);
        }
        Fire();
    }
    public override void SetDir(Vector2 dir)
    {
        if (offset == null)
        {
            this.dir = dir.normalized;
        }
        else
        {
            this.dir = offset * dir.normalized;
        }

    }
    public void CreateCOPY(){
        this.gameObject.SetActive(true);
        for(int i=0;i<copies.Count;i++){
            Destroy(copies[i]);
        }
        copies = new List<GameObject>();

        //GameObject parent = transform.parent.gameObject;因为this.gameobject不是active的，无法用transform.parent
        GameObject parent = GameObject.Find("Player");
        //找到所有LaserGunCopy，注意，此时它们的active都是false
        while(parent.transform.Find("LaserGunCopy(Clone")!=null){
            Destroy(parent.transform.Find("LaserGunCopy(Clone)").gameObject);
        }

        float leftAngle;
        if (num % 2 == 0)
        {
            //如果是偶数，最左侧的激光的角度为-angle/2-angle*(num/2-1)
            leftAngle = -angle / 2 - angle * (num / 2 - 1);
        }
        else
        {
            //如果是奇数，最左侧的激光的角度为-angle*(num/2)
            leftAngle = -angle * (num / 2);
        }
        float currentAngle;
        for (int i = 1; i < num; i++)
        {
            //在这里生成num个激光COPY，激光COPY和自身的区别在于：offset不为null，并且玩家只持有主激光
            currentAngle = leftAngle + angle * i;

            GameObject temp = Instantiate(copy, transform.position, transform.rotation);
            temp.GetComponent<LaserGunCOPY>().offset = Quaternion.AngleAxis(currentAngle, Vector3.forward);
            temp.transform.SetParent(parent.transform);
            temp.GetComponent<LaserGunCOPY>().Initialize();
            temp.GetComponent<LaserGunCOPY>().laserfather = this.gameObject;
            copies.Add(temp);
        }
        //考虑到主激光的存在，把主激光放到最左侧
        offset = Quaternion.AngleAxis(leftAngle, Vector3.forward);
        //反正子弹数量不会减少
    }
    
    protected override void Fire()
    {
        //发射一条长度为laserLength的激光
        RaycastHit2D ray = Physics2D.Raycast(parent.transform.position, dir, laserLength, LayerMask.GetMask("Enemy"));
        //发射点与player中心要有一定偏离,要朝着transform.up方向移动一点
        Vector3 center = transform.position;
        center += (Vector3)dir * distance;
        //设置0号点的位置
        line.SetPosition(0, center);
        //如果没有击中任何物体
        if (ray.rigidbody == null)
        {
            line.SetPosition(1, center + (Vector3)dir * laserLength);
            laserPartical.SetActive(false);
        }
        else 
        {
            line.SetPosition(1, ray.point);
            //不能帧判定
            CalculateDamage(cardList);
            ray.rigidbody.GetComponent<OnHurt>().CalculateDamage(damage, type, initialDamage);
        }

    }


    public void CalculateDamage(Weapon_CardList cardList)
    {

        type = cardList.getType();
        damage = cardList.getDamage();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        timer.runTheClock();
        if (timer.GetTime() > 0.2f)
        {
            timer.refreshTime();
        }
    }
}
