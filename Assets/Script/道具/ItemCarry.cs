using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ItemCarry : MonoBehaviour
{
    
    public GameObject[] weapons;
    public PlayerAttribute playerAttribute;
    public PlayerController playerController;


    #region 道具相关物体
    [Header("道具相关物体")]
    //火箭子弹
    public GameObject rocket;
    //跟踪子弹
    public GameObject follow;
    //防护罩
    GameObject playerCover;
    //防御无人机
    public GameObject defendFriend;
    public GameObject attackFriend;
    //攻击无人机
    #endregion

    #region 道具相关属性
    [Header("道具相关属性")]
    public int bulletNum ;
    //这两个无人机懒得做血条了，就这样吧，无敌的
    public List<GameObject> defendFriends = new List<GameObject>();
    public List<GameObject> attackFriends = new List<GameObject>();
    #endregion

    List<int> randomNumbers ;


    private void Awake()
    {
        bulletNum = 1;
        playerController = GetComponent<PlayerController>();
        playerCover = transform.Find("Cover").gameObject;
        weapons = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            //0是shotgun，1是lasergun
            weapons[i] = transform.GetChild(i).gameObject;
            Debug.Log(weapons[i].name);
        }
        playerAttribute = GetComponentInParent<PlayerAttribute>();
    }
    #region 所有道具的函数
    //添加防护罩
    public void AddCover()
    {
        playerCover.SetActive(true);
        playerCover.GetComponent<Cover>().Initialize();
    }
    //设置武器为火箭
    public void AddRocket()
    {
        playerController.currentWeapen.SetActive(false);
        playerController.currentWeapen = playerController.weapens[0];
        playerController.currentWeapen.GetComponent<ShotGun>().bullet = rocket;
        playerController.currentWeapen.SetActive(true);
        SetBulletNum();

    }
    //设置武器为激光
    public void AddLaserGun()
    {
        playerController.currentWeapen.SetActive(false);
        playerController.currentWeapen = playerController.weapens[1];
        playerController.currentWeapen.SetActive(true);
        SetBulletNum();
    }
    //设置武器为跟踪
    public void AddFollow()
    {
        playerController.currentWeapen.SetActive(false);
        playerController.currentWeapen = playerController.weapens[0];
        playerController.currentWeapen.GetComponent<ShotGun>().bullet = follow;
        playerController.currentWeapen.SetActive(true);
        SetBulletNum();
    }
    //子弹++
    public void AddBulletNum()
    {
        bulletNum++;
        SetBulletNum();
    }
    //添加防御类队友
    public void AddDefendFriend()
    {
        GameObject temp = Instantiate(defendFriend, transform.position, Quaternion.identity);
        defendFriends.Add(temp);
    }
    //添加攻击类队友
    public void AddAttackFriend()
    {
        GameObject temp = Instantiate(attackFriend, transform.position, Quaternion.identity);
        attackFriends.Add(temp);
    }

    //设置子弹数量，道具之间会发生交互
    void SetBulletNum()
    {
        //shotGun
        if (playerController.currentWeapen == playerController.weapens[0])
        {
            weapons[0].GetComponent<ShotGun>().bulletNum = bulletNum;
        }
        //laserGun
        else
        {
            //如果是laserGun
            weapons[1].GetComponent<LaserGun>().num = bulletNum;
            weapons[1].GetComponent<LaserGun>().CreateCOPY();
        }
    }
    #endregion

    public List<string> ShowItem()
    {
        randomNumbers =  new List<int>();
        List<string> result = new List<string>();
        //选道具，返回道具描述
        while (randomNumbers.Count < 3)
        {
            int randomNum = Random.Range(0, 7);
            //选不重复的道具
            if (!randomNumbers.Contains(randomNum))
            {
                randomNumbers.Add(randomNum);
                switch (randomNum)
                {
                    case 0:
                        //AddCover();
                        result.Add("获得一个护罩\n多次获得护罩会增加护罩的最大生命值");
                        break;
                    case 1:
                        //AddRocket();
                        result.Add("修改攻击方式为火箭\n多次获得会增加基础伤害");
                        break;
                    case 2:
                        //AddLaserGun();
                        result.Add("修改攻击方式为激光\n多次获得会增加基础伤害");
                        break;
                    case 3:
                        //AddFollow();
                        result.Add("修改攻击方式为跟踪子弹\n多次获得会增加基础伤害");
                        break;
                    case 4:
                        //AddBulletNum();
                        result.Add("增加子弹数量");
                        break;
                    case 5:
                        //AddDefendFriend();
                        result.Add("召唤一个围绕玩家飞行的无人机");
                        break;
                    case 6:
                        //AddAttackFriend();
                        result.Add("召唤一个跟随玩家的无人机");
                        break;
                }
            }
        }
        return result;
    }

    public void ChooseItem(int num)
    {
        int item = randomNumbers[num];
        switch (item)
        {
            case 0:
                AddCover();
                break;
            case 1:
                AddRocket();
                break;
            case 2:
                AddLaserGun();
                break;
            case 3:
                AddFollow();    
                break;
            case 4:
                AddBulletNum();
                break;
            case 5:
                AddDefendFriend();
                break;
            case 6:
                AddAttackFriend();
                break;
        }
    }
}